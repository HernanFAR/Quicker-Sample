using Domain.Contexts.UserBoundedContext.Constants;
using Domain.Contexts.UserBoundedContext.Core;
using FluentValidation;

namespace Domain.Contexts.UserBoundedContext.Validators
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("El nombre es obligatorio")
                .MaximumLength(UserConstants.NameProperty.MaxLength)
                    .WithMessage("El nombre no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

            RuleFor(e => e.UserName)
                .NotEmpty()
                    .WithMessage("El nombre de usuario es obligatorio")
                .MaximumLength(UserConstants.UserNameProperty.MaxLength)
                    .WithMessage("El nombre de usuario no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("El correo es obligatorio")
                .EmailAddress()
                    .WithMessage("El correo no posee el formato adecuado")
                .MaximumLength(UserConstants.EmailProperty.MaxLength)
                    .WithMessage("El correo no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

            RuleFor(e => e.PhoneNumber)
                .MaximumLength(UserConstants.PhoneNumberProperty.MaxLength)
                    .WithMessage("El número de teléfono no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");

            RuleFor(e => e.SubName)
                .MaximumLength(UserConstants.SubNameProperty.MaxLength)
                    .WithMessage("El apellido no debe de tener más de {MaxLength} caracteres, ingresaste {TotalLength}");
        }
    }
}
