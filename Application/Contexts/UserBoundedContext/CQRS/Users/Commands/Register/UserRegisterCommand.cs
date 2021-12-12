using Quicker.Infrastructure.MediatR.Request;
using System;

namespace Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SubName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class UserRegisterCommand : ICommand<UserRegisterDTO>
    {
        public string Name { get; set; } = string.Empty;

        public string SubName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
