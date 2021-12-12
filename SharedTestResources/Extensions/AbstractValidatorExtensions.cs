using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SharedTestResources.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static IEnumerable<(IPropertyValidator Validator, IRuleComponent Options)> GetValidatorsForMember<T, TProperty>(
            this IValidator<T> validator, Expression<Func<T, TProperty>> expression)
        {
            var descriptor = validator.CreateDescriptor();
            var expressionMemberName = expression.GetMember()?.Name;

            return descriptor.GetValidatorsForMember(expressionMemberName).ToArray();
        }
    }
}
