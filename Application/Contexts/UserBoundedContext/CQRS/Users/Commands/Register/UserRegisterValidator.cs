using Domain.Contexts.UserBoundedContext.Constants;
using FluentValidation;
using Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterCommand>
    {
        private readonly ApplicationUserManager _UserManager;

        public UserRegisterValidator(ApplicationUserManager userManager)
        {
            _UserManager = userManager;

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("El nombre es obligatorio")
                .MaximumLength(UserConstants.NameProperty.MaxLength)
                    .WithMessage("El nombre no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

            RuleFor(e => e.UserName)
                .NotEmpty()
                    .WithMessage("El nombre de usuario es obligatorio")
                .MaximumLength(UserConstants.UserNameProperty.MaxLength)
                    .WithMessage("El nombre de usuario no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}")
                .MustAsync(BeANonExistingUserName)
                    .WithMessage(e => $"El nombre de usuario ({e.UserName}) ya existe en el sistema");

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("El correo es obligatorio")
                .EmailAddress()
                    .WithMessage("El correo no posee el formato adecuado")
                .MaximumLength(UserConstants.EmailProperty.MaxLength)
                    .WithMessage("El correo no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}")
                .MustAsync(BeANonExistingEmail)
                    .WithMessage(e => $"El correo ({e.Email}) ya existe en el sistema");

            RuleFor(e => e.PhoneNumber)
                .MaximumLength(UserConstants.PhoneNumberProperty.MaxLength)
                   .WithMessage("El número de teléfono no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

            RuleFor(e => e.SubName)
                .MaximumLength(UserConstants.SubNameProperty.MaxLength)
                    .WithMessage("El apellido no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

        }

        private Task<bool> BeANonExistingUserName(string userName, CancellationToken cancellationToken)
            => _UserManager.Users
                .AllAsync(e => e.UserName != userName, cancellationToken);

        private Task<bool> BeANonExistingEmail(string email, CancellationToken cancellationToken)
            => _UserManager.Users
                .AllAsync(e => e.Email != email, cancellationToken);
    }
}
