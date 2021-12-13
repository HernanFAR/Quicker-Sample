using Application.Contexts.UserBoundedContext.CQRS.Users.Interfaces;
using Quicker.Infrastructure.MediatR.Request;
using System;

namespace Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterDTO : IUserDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? SubName { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpireDate { get; set; }
    }

    public class UserRegisterCommand : ICommand<UserRegisterDTO>
    {
        public string Name { get; set; } = string.Empty;

        public string SubName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
