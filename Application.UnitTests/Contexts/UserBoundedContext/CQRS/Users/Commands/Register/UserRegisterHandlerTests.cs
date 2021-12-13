using Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register;
using Domain.Contexts.UserBoundedContext.Configurations;
using Domain.Contexts.UserBoundedContext.Core;
using FluentAssertions;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.RoleRelated;
using Infrastructure.EntityFrameworkCore.UserRelated;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedTestResources.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterHandlerTests : IDisposable
    {
        private readonly IServiceProvider _ServiceProvider;

        public UserRegisterHandlerTests()
        {
            _ServiceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped(_ => ContextManager.CreateContext(Guid.NewGuid()))
                .AddEntityFrameworkSqlite()
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders()
                .Services
                .Configure(IdentityConfiguration.ApplyIdentityOptionsConfiguration)
                .BuildServiceProvider();
        }

        public void Dispose()
        {
            _ServiceProvider.GetRequiredService<ApplicationDbContext>().Dispose();
        }

        [Fact]
        public async Task Handle_Success_Should_CreateUserWithInformation()
        {
            const int expCount = 2;

            const string name = "Name";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "H5sdb.1234";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var jwtConfiguration = new JWTConfiguration("XDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
                "XDDDDDDDDDDDDD", "XDDDDDDDDDDDDD", 2.5);

            var handler = new UserRegisterHandler(_ServiceProvider.GetRequiredService<ApplicationSignInManager>(), jwtConfiguration);

            var dto = await handler.Handle(command, default);

            dto.Id.Should().NotBe(default(Guid));
            dto.Name.Should().Be(name);
            dto.UserName.Should().Be(userName);
            dto.Email.Should().Be(email);
            dto.PhoneNumber.Should().Be(phoneNumber);
            dto.SubName.Should().Be(subName);

            dto.Token.Should().NotBeNullOrEmpty();
            dto.ExpireDate.Should().BeCloseTo(DateTime.Now.AddDays(jwtConfiguration.Duration), TimeSpan.FromHours(6));

            var context = _ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Users.Count().Should().Be(expCount);
            context.Users.Any(e => e.Id == dto.Id).Should().BeTrue();
        }
    }
}
