using MediatR;
using System;

namespace Domain.Contexts.UserBoundedContext.ETOs
{
    public class ApplicationUserHasBeenCreated : INotification
    {
        public ApplicationUserHasBeenCreated(Guid applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }

        public Guid ApplicationUserId { get; }
    }
}
