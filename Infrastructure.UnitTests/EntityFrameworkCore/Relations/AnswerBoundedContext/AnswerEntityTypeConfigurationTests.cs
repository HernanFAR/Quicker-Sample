using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Validators;
using FluentValidation.Validators;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.Relations.QuestionBoundedContext;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using SharedTestResources.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.AnswerBoundedContext.Validators;
using FluentAssertions;
using Infrastructure.EntityFrameworkCore.Relations.AnswerBoundedContext;
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
