using Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register;
using Domain.Contexts.UserBoundedContext.Constants;
using Domain.Contexts.UserBoundedContext.Core;
using FluentAssertions;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.UserRelated;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedTestResources.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterValidatorTests : IDisposable
    {
        private readonly IServiceProvider _ServiceProvider;

        public UserRegisterValidatorTests()
        {
            _ServiceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped(_ => ContextManager.CreateContext(Guid.NewGuid()))
                .AddEntityFrameworkSqlite()
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddDefaultTokenProviders()
                .Services.BuildServiceProvider();
        }

        public void Dispose()
        {
            _ServiceProvider.GetRequiredService<ApplicationDbContext>().Dispose();
        }

        [Fact]
        public async Task Validate_Success_Should_BeValidCommand()
        {
            const string name = "Name";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Validate_Failure_Should_BeNotValid_Detail_NullName(string name)
        {
            const int expCount = 1;

            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name!,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El nombre es obligatorio");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_NameTooLong()
        {
            const int expCount = 1;

            const string name = "ADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑ";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El nombre no debe de tener más de {UserConstants.NameProperty.MaxLength} caracteres, ingresaste {name.Length}");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Validate_Failure_Should_BeNotValid_Detail_NullUserName(string userName)
        {
            const int expCount = 1;

            const string name = "name";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName!,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El nombre de usuario es obligatorio");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_UserNameTooLong()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "ADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑ";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El nombre de usuario no debe de tener más de {UserConstants.UserNameProperty.MaxLength} caracteres, ingresaste {userName.Length}");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_ExistingUserName()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = UserConstants.AdminEntity.UserName;
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El nombre de usuario ({userName}) ya existe en el sistema");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_NullEmail()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = null!;
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email!,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El correo es obligatorio");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_EmailTooLong()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = "ADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑ@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El correo no debe de tener más de {UserConstants.EmailProperty.MaxLength} caracteres, ingresaste {email.Length}");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_BadEmail()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = UserConstants.AdminEntity.Email;
            const string email = "email";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == "El correo no posee el formato adecuado");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_ExistingEmail()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = UserConstants.AdminEntity.Email;
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El correo ({email}) ya existe en el sistema");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_PhoneNumberTooLong()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 1234 5678 1234 5678 1234 5678 1234 5678 1234 5678 1234 5678 1234 5678 1234 5678";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El número de teléfono no debe de tener más de {UserConstants.PhoneNumberProperty.MaxLength} caracteres, ingresaste {phoneNumber.Length}");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_BadPhoneNumber()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 asdf";
            const string subName = "subName";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El número de teléfono ingresado no tiene el siguiente formato: +569 1234 9876");
        }

        [Fact]
        public async Task Validate_Failure_Should_BeNotValid_Detail_SubNameTooLong()
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "ADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑADSFGHJKLÑ";
            const string password = "password";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El apellido no debe de tener más de {UserConstants.SubNameProperty.MaxLength} caracteres, ingresaste {subName.Length}");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Validate_Failure_Should_BeNotValid_Detail_PasswordEmpty(string password)
        {
            const int expCount = 1;

            const string name = "name";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "+569 4979 8355";
            const string subName = "subName";

            var command = new UserRegisterCommand
            {
                Name = name,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                SubName = subName,
                Password = password
            };

            var userManager = _ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var validator = new UserRegisterValidator(userManager);

            var result = await validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"La contraseña es obligatoria");
        }
    }
}
