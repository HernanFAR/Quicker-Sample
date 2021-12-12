using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Validators;
using Domain.Contexts.UserBoundedContext.Builders;
using FluentAssertions;
using System;
using Domain.Contexts.UserBoundedContext.Validators;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;
using Domain.Contexts.UserBoundedContext.Constants;

namespace Domain.UnitTests.Contexts.UserBoundedContext.Validations
{
    public class ApplicationUserValidatorTests
    {
        [Fact]
        public void Validate_Success_Should_BeValidEntity_Detail_AllValidProperties()
        {
            const string name = "Name";
            const string userName = "UserName";
            const string email = "email.email@email.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_NameNull()
        {
            const string? name = null;
            const string userName = "UserName";
            const string email = "email@email.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name!).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El nombre es obligatorio");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_NameTooLong()
        {
            const string name = "ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ";
            const string userName = "UserName";
            const string email = "email@email.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El nombre no debe de tener más de {UserConstants.NameProperty.MaxLength} caracteres, ingresaste {name.Length}");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_UserNameNull()
        {
            const string name = "Name";
            const string? userName = null;
            const string email = "email@email.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName!)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El nombre de usuario es obligatorio");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_UserNameTooLong()
        {
            const string name = "Name";
            const string userName = "ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ";
            const string email = "email@email.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El nombre de usuario no debe de tener más de {UserConstants.UserNameProperty.MaxLength} caracteres, ingresaste {userName.Length}");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_EmailNull()
        {
            const string name = "Name";
            const string userName = "UserName";
            const string? email = null;
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email!)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El correo es obligatorio");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_EmailTooLong()
        {
            const string name = "Name";
            const string userName = "UserName";
            const string email = "email@ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El correo no debe de tener más de {UserConstants.EmailProperty.MaxLength} caracteres, ingresaste {email.Length}");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_EmailWithoutFormat()
        {
            const string name = "Name";
            const string userName = "UserName";
            const string email = "email.email";
            const string phoneNumber = "PhoneNumber";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "El correo no posee el formato adecuado");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_PhoneNumberTooLong()
        {
            const string name = "Name";
            const string userName = "UserName";
            const string email = "email@email.com";
            const string phoneNumber = "ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ";
            const string subName = "SubName";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El número de teléfono no debe de tener más de {UserConstants.PhoneNumberProperty.MaxLength} caracteres, ingresaste {phoneNumber.Length}");

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_SubNameTooLong()
        {
            const string name = "Name";
            const string userName = "UserName";
            const string email = "email@email.com";
            const string phoneNumber = "PhoneNumber";
            const string subName = "ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ";

            const int expCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            var validator = new ApplicationUserValidator();

            var result = validator.Validate(applicationUser);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should()
                .ContainSingle(e => e.ErrorMessage == $"El apellido no debe de tener más de {UserConstants.SubNameProperty.MaxLength} caracteres, ingresaste {subName.Length}");

        }
    }
}
