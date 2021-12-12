using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contexts.UserBoundedContext.CQRS.Users.Commands.Register
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserRegisterDTO>
    {
        public Task<UserRegisterDTO> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
