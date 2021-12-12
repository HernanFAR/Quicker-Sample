using Domain.Contexts.AnswerBoundedContext.Constants;
using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.AnswerBoundedContext.Validators;
using FluentAssertions;
using FluentValidation.Validators;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.Relations.AnswerBoundedContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SharedTestResources.Extensions;
using System;
using System.Linq;
using Xunit;

namespace Infrastructure.UnitTests.EntityFrameworkCore.Relations.AnswerBoundedContext
{
    public class AnswerEntityTypeConfigurationTests : IDisposable
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
            var questionBuilder = modelBuilder.Entity<Answer>();

            new AnswerEntityTypeConfiguration(_Context).Configure(questionBuilder);

            questionBuilder.Metadata.GetTableName()
                .Should().Be(nameof(Answer));

            questionBuilder.Metadata.GetSchema()
                .Should().Be(AnswerDatabaseConstants.Schema);
        }

        [Fact]
        public void NameProperty_Success_Should_FieldValidationsMustBeTheSameAsFluentValidation()
        {
            var conventionSet = ConventionSet.CreateConventionSet(_Context);

            var modelBuilder = new ModelBuilder(conventionSet);
            var answerBuilder = modelBuilder.Entity<Answer>();

            new AnswerEntityTypeConfiguration(_Context).Configure(answerBuilder);

            var nameMetadata = answerBuilder.Metadata.FindDeclaredProperty(nameof(Answer.Name));

            var maxLengthDb = nameMetadata.GetMaxLength();
            var nullableDb = nameMetadata.IsColumnNullable();

            var validator = new AnswerValidator();

            var validatorForName = validator.GetValidatorsForMember(e => e.Name)
                .Select(e => e.Validator);

            var maxLengthValidator = validatorForName.OfType<MaximumLengthValidator<Answer>>()
                .First();

            var notEmptyValidator = validatorForName.OfType<NotEmptyValidator<Answer, string>>()
                .First();

            maxLengthValidator.Max.Should().Be(maxLengthDb);

            _ = nullableDb ? notEmptyValidator.Should().BeNull() : notEmptyValidator.Should().NotBeNull();
        }
    }
}
