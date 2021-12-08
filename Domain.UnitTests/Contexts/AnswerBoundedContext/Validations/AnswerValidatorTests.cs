using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.AnswerBoundedContext.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.UnitTests.Contexts.AnswerBoundedContext.Validations
{
    public class AnswerValidatorTests
    {
        [Fact]
        public void Validate_Success_Should_BeValidEntity_Detail_AllValidProperties()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var answerId = Guid.NewGuid();

            var answer = new Answer(name, questionId, answerId, createdBy);

            var validator = new AnswerValidator();

            var result = validator.Validate(answer);


            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_NameNull()
        {
            const string? name = null;
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var answerId = Guid.NewGuid();

            const int expCount = 1;

            var answer = new Answer(name!, questionId, answerId, createdBy);

            var validator = new AnswerValidator();

            var result = validator.Validate(answer);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == "Debes proveer de la respuesta a publicar");
        }

        [Fact]
        public void Validate_Success_Should_BeInvalidEntity_Detail_NameTooLong()
        {
            const string name = "ASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑASDFGHJKLÑ";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var answerId = Guid.NewGuid();

            const int expCount = 1;

            var answer = new Answer(name, questionId, answerId, createdBy);
            var validator = new AnswerValidator();

            var result = validator.Validate(answer);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().HaveCount(expCount);
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == $"La respuesta no debe de tener más de 1024 caracteres, ingresaste {name.Length}");
        }
    }
}
