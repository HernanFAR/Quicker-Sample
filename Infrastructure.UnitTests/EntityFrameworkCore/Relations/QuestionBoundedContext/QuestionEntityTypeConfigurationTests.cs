using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Validators;
using Domain.Contexts.UserBoundedContext.Constants;
using Domain.Contexts.UserBoundedContext.Core;
using FluentAssertions;
using FluentValidation.Validators;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.Relations.QuestionBoundedContext;
using Infrastructure.EntityFrameworkCore.Relations.UserBoundedContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SharedTestResources.Extensions;
using System;
using System.Linq;
using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Constants;
using Infrastructure.EntityFrameworkCore.Relations.AnswerBoundedContext;
using Xunit;

namespace Infrastructure.UnitTests.EntityFrameworkCore.Relations.QuestionBoundedContext
{
    public class QuestionEntityTypeConfigurationTests : IDisposable
    {
        public ApplicationDbContext _Context = ContextManager.CreateContext(Guid.NewGuid());

        public void Dispose()
        {
            _Context.Dispose();
        }

        [Fact]
        public void TableInfo_Success_ShouldHaveCorrectNameAndSchema()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<Question>();

            new QuestionEntityTypeConfiguration(_Context).Configure(questionBuilder);

            questionBuilder.Metadata.GetTableName()
                .Should().Be(nameof(Question));

            questionBuilder.Metadata.GetSchema()
                .Should().Be(QuestionDatabaseConstants.Schema);
        }

        [Fact]
        public void NameProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var questionBuilder = modelBuilder.Entity<Question>();

            new QuestionEntityTypeConfiguration(_Context).Configure(questionBuilder);

            var nameMetadata = questionBuilder.Metadata.FindDeclaredProperty(nameof(Question.Name));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new QuestionValidator();

            var validatorForName = validator.GetValidatorsForMember(e => e.Name)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForName.OfType<MaximumLengthValidator<Question>>()
                .First();

            var notEmptyValidator = validatorForName.OfType<NotEmptyValidator<Question, string>>()
                .First();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }
    }
}
