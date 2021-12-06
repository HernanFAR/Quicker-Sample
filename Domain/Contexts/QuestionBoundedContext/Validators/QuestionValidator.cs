using Domain.Contexts.QuestionBoundedContext.Constants;
using Domain.Contexts.QuestionBoundedContext.Core;
using FluentValidation;

namespace Domain.Contexts.QuestionBoundedContext.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Debes proveer de la pregunta a publicar")
                .MaximumLength(QuestionConstants.NameProperty.MaxLength)
                    .WithMessage("La pregunta no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");
        }
    }
}
