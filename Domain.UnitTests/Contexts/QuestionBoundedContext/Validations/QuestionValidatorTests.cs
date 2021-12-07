using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.UnitTests.Contexts.QuestionBoundedContext.Validations
{
    public class QuestionValidatorTests
    {
        [Fact]
        public void Validate_Success_Should_BeValidEntity_Detail_AllValidProperties()
        {
            const string name = "Question";
            var createdBy = Guid.NewGuid();

            var question = new Question(name, createdBy);
            var validator = new QuestionValidator();

            var result = validator.Validate(question);


            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_NameNull()
        {
            const string? name = null;
            const int expCount = 1;
            var createdBy = Guid.NewGuid();

            var question = new Question(name!, createdBy);
            var validator = new QuestionValidator();

            var result = validator.Validate(question);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Debes proveer de la pregunta a publicar");
        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_NameTooLong()
        {
            const string name = "ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ";
            const int expCount = 1;
            var createdBy = Guid.NewGuid();

            var question = new Question(name, createdBy);
            var validator = new QuestionValidator();

            var result = validator.Validate(question);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == $"La pregunta no debe de tener más de 1024 caracteres, ingresaste {name.Length}");
        }
    }
}
