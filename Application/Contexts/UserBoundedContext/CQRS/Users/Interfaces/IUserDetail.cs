using System;

namespace Application.Contexts.UserBoundedContext.CQRS.Users.Interfaces
{
    public interface IUserDetail
    {
        public Guid Id { get; }

        public string Name { get; }

        public string? SubName { get; }

        public string UserName { get; }

        public string Email { get; }

        public string? PhoneNumber { get; }
    }
}
