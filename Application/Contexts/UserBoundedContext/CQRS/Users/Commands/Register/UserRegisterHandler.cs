using Domain.Contexts.UserBoundedContext.Builders;
using Domain.Contexts.UserBoundedContext.Configurations;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.EntityFrameworkCore;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserRegisterDTO>
    {
        private readonly ApplicationSignInManager _SignInManager;
        private readonly JWTConfiguration _JWTConfiguration;

        public UserRegisterHandler(ApplicationSignInManager signInManager, JWTConfiguration jwtConfiguration)
        {
            _SignInManager = signInManager;
            _JWTConfiguration = jwtConfiguration;
        }

        public async Task<UserRegisterDTO> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(request.Email)
                .WithName(request.Name).WithUserName(request.UserName)
                .Continue
                .WithPhoneNumber(request.PhoneNumber).WithSubName(request.SubName)
                .GetInstance;

            var addedUserResult = await _SignInManager.UserManager.CreateAsync(applicationUser, request.Password);

            if (!addedUserResult.Succeeded)
            {
                throw new ValidationException(
                    addedUserResult.Errors.Select(e =>
                        new ValidationFailure(e.Code, e.Description)
                    )
                );
            }

            var identity = await _SignInManager.CreateUserPrincipalAsync(applicationUser);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTConfiguration.IssuerSigningKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(
                issuer: _JWTConfiguration.Issuer,
                audience: _JWTConfiguration.Audience,
                claims: identity.Claims,
                expires: DateTime.Now.AddDays(_JWTConfiguration.Duration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            scope.Complete();

            return new UserRegisterDTO
            {
                Token = tokenHandler.WriteToken(jwt),
                ExpireDate = jwt.ValidTo,
                Email = applicationUser.Email,
                Id = applicationUser.Id,
                Name = applicationUser.Name,
                PhoneNumber = applicationUser.PhoneNumber,
                SubName = applicationUser.SubName,
                UserName = applicationUser.UserName
            };
        }
    }
}
