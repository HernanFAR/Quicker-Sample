using Domain.Contexts.UserBoundedContext.Builders;
using Domain.Contexts.UserBoundedContext.ETOs;
using Domain.Contexts.UserBoundedContext.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Quicker.Domain;
using Quicker.Domain.Interfaces.Abstracts;
using Quicker.Domain.Interfaces.Abstracts.Audited.AggregateRoots.ICUDRAudited;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Contexts.UserBoundedContext.Core
{
    // Acá, hay algo que mencionar
    // Si te fijas, en Question se usaron constructores para instanciar, esto es debido a que en la mayoría de los
    // casos es el modo más rápido y apropiado.
    //
    // Sin embargo, van a haber algunos casos en donde usar constructores puede ser tedioso, y no respetar
    // apropiadamente el lenguaje ubicuo (Ubicuous Language) por tener muchísimos parametros. En estos casos,
    // puedes usar un Builder para instanciar o modificar la instancia.
    //
    public sealed class ApplicationUser : IdentityUser<Guid>, ICUDRAuditedAggregateRoot<Guid>
    {
        private ApplicationUser()
        {
            Name = string.Empty;
            SubName = string.Empty;
        }

        internal ApplicationUser(ApplicationUserCreateBuilder createBuilder) : this()
        {
            Id = createBuilder.Id;
            Name = createBuilder.Name;
            SubName = createBuilder.SubName;
            UserName = createBuilder.UserName;
            Email = createBuilder.Email;
            PhoneNumber = createBuilder.PhoneNumber;

            AddEvent(new EventInformation(new ApplicationUserHasBeenCreated(Id), true));

            Create();
        }

        internal void UpdateInformation(ApplicationUserEditBuilder builder)
        {
            Name = builder.Name ?? Name;
            UserName = builder.UserName ?? UserName;
            Email = builder.Email ?? Email;

            PhoneNumber = builder.PhoneNumber ?? PhoneNumber;
            SubName = builder.SubName ?? SubName;

            Update();
        }

        public string Name { get; private set; }

        public string? SubName { get; private set; }

        public object?[] GetKeys() => new object?[] { Id };

        public string StringifyKeys() => $"{Id}";

        public bool EntityEquals(IEntity? other)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            CreatedDate = DateTime.Now;
        }

        public DateTime? CreatedDate { get; private set; }

        public void Update()
        {
            UpdatedDate = DateTime.Now;
        }

        public DateTime? UpdatedDate { get; private set; }

        public void Deactivate()
        {
            DeactivatedDate = DateTime.Now;
            Inactive = true;
        }

        public void Reactivate()
        {
            ReactivatedDate = DateTime.Now;
            Inactive = false;
        }

        public DateTime? DeactivatedDate { get; private set; }

        public DateTime? ReactivatedDate { get; private set; }

        public bool Inactive { get; private set; }


        private readonly ICollection<EventInformation> _Events = new Collection<EventInformation>();

        public IEnumerable<EventInformation> GetEvents() => _Events;

        public void AddEvent(EventInformation eventData) => _Events.Add(eventData);

        public void ClearEvents() => _Events.Clear();

        public void Validate()
        {
            var validator = new ApplicationUserValidator();

            validator.ValidateAndThrow(this);
        }
    }
}
