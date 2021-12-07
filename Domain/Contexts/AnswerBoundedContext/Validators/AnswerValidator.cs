using Domain.Contexts.AnswerBoundedContext.Constants;
using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using FluentValidation;

namespace Domain.Contexts.AnswerBoundedContext.Validators
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Debes proveer de la respuesta a publicar")
                .MaximumLength(AnswerConstants.NameProperty.MaxLength)
                    .WithMessage("La respuesta no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

        }
    }
}
