using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace Infrastructure.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        private static readonly Type _EntityTypeGeneric = typeof(IEntityTypeConfiguration<>);

        public static ModelBuilder ApplyConfigurationsWithContext(this ModelBuilder @this, ApplicationDbContext context)
        {
            var entityTypes = typeof(Anchor).Assembly.ExportedTypes
                .Where(e => e.GetInterfaces().Any(e => e.IsGenericType && e.GetGenericTypeDefinition() == _EntityTypeGeneric))
                .Where(e => !e.IsAbstract && !e.IsInterface)
                .ToList();

            var applyEntityConfigurationMethod = typeof(ModelBuilder)
                .GetMethods()
                .Single(
                    e => e.Name == nameof(ModelBuilder.ApplyConfiguration)
                         && e.ContainsGenericParameters
                         && e.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition()
                         == typeof(IEntityTypeConfiguration<>));

            foreach (var entityType in entityTypes)
            {
                var genericArgument = entityType.GetInterfaces()
                    .Single(e => e.IsGenericType && e.GetGenericTypeDefinition() == _EntityTypeGeneric)
                    .GenericTypeArguments[0];

                var target = applyEntityConfigurationMethod.MakeGenericMethod(genericArgument);
                target.Invoke(@this, new[] { Activator.CreateInstance(entityType, context) });
            }

            return @this;
        }
    }
}
