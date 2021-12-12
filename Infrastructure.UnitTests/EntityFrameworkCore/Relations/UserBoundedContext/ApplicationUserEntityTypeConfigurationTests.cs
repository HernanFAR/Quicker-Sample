using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Validators;
using Domain.Contexts.UserBoundedContext.Constants;
using FluentAssertions;
using FluentValidation.Validators;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.Relations.QuestionBoundedContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SharedTestResources.Extensions;
using System;
using System.Linq;
using Domain.Contexts.UserBoundedContext.Core;
using Domain.Contexts.UserBoundedContext.Validators;
using Infrastructure.EntityFrameworkCore.Relations.UserBoundedContext;
using Xunit;

namespace Infrastructure.UnitTests.EntityFrameworkCore.Relations.UserBoundedContext
{
    public class ApplicationUserEntityTypeConfigurationTests : IDisposable
    {
        public ApplicationDbContext _Context = ContextManager.CreateContext(Guid.NewGuid());

        public void Dispose()
        {
            _Context.Dispose();
        }

        [Fact]
        public void Users_Success_Should_ReturnCreatedUsers()
        {
            var entity = _Context.Users.Single(e => e.Id == UserConstants.AdminEntity.Id);

            entity.Email.Should().Be(UserConstants.AdminEntity.Email);
            entity.Name.Should().Be(UserConstants.AdminEntity.Name);
            entity.SubName.Should().Be(UserConstants.AdminEntity.SubName);
            entity.UserName.Should().Be(UserConstants.AdminEntity.UserName);
        }

        [Fact]
        public void NameProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<ApplicationUser>();

            new ApplicationUserEntityTypeConfiguration(_Context).Configure(questionBuilder);

            var nameMetadata = questionBuilder.Metadata.FindDeclaredProperty(nameof(ApplicationUser.Name));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new ApplicationUserValidator();

            var validatorForName = validator.GetValidatorsForMember(e => e.Name)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForName.OfType<MaximumLengthValidator<ApplicationUser>>()
                .First();

            var notEmptyValidator = validatorForName.OfType<NotEmptyValidator<ApplicationUser, string>>()
                .FirstOrDefault();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }

        [Fact]
        public void UserNameProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<ApplicationUser>();

            new ApplicationUserEntityTypeConfiguration(_Context).Configure(questionBuilder);

            var nameMetadata = questionBuilder.Metadata.FindDeclaredProperty(nameof(ApplicationUser.UserName));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new ApplicationUserValidator();

            var validatorForUserName = validator.GetValidatorsForMember(e => e.UserName)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForUserName.OfType<MaximumLengthValidator<ApplicationUser>>()
                .First();

            var notEmptyValidator = validatorForUserName.OfType<NotEmptyValidator<ApplicationUser, string>>()
                .FirstOrDefault();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }

        [Fact]
        public void EmailProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<ApplicationUser>();

            new ApplicationUserEntityTypeConfiguration(_Context).Configure(questionBuilder);

            var nameMetadata = questionBuilder.Metadata.FindDeclaredProperty(nameof(ApplicationUser.Email));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new ApplicationUserValidator();

            var validatorForEmail = validator.GetValidatorsForMember(e => e.Email)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForEmail.OfType<MaximumLengthValidator<ApplicationUser>>()
                .First();

            var notEmptyValidator = validatorForEmail.OfType<NotEmptyValidator<ApplicationUser, string>>()
                .FirstOrDefault();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }

        [Fact]
        public void PhoneNumberProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<ApplicationUser>();

            new ApplicationUserEntityTypeConfiguration(_Context).Configure(questionBuilder);

            var nameMetadata = questionBuilder.Metadata.FindDeclaredProperty(nameof(ApplicationUser.PhoneNumber));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new ApplicationUserValidator();

            var validatorForPhoneNumber = validator.GetValidatorsForMember(e => e.PhoneNumber)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForPhoneNumber.OfType<MaximumLengthValidator<ApplicationUser>>()
                .First();

            var notEmptyValidator = validatorForPhoneNumber.OfType<NotEmptyValidator<ApplicationUser, string>>()
                .FirstOrDefault();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }

        [Fact]
        public void SubNameProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<ApplicationUser>();

            new ApplicationUserEntityTypeConfiguration(_Context).Configure(questionBuilder);

            var nameMetadata = questionBuilder.Metadata.FindDeclaredProperty(nameof(ApplicationUser.SubName));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new ApplicationUserValidator();

            var validatorForSubName = validator.GetValidatorsForMember(e => e.SubName)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForSubName.OfType<MaximumLengthValidator<ApplicationUser>>()
                .First();

            var notEmptyValidator = validatorForSubName.OfType<NotEmptyValidator<ApplicationUser, string>>()
                .FirstOrDefault();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }
    }
}
