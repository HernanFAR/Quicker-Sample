using Domain.Contexts.UserBoundedContext.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Infrastructure.EntityFrameworkCore.RoleRelated
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, Guid>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
        {
        }
    }
}
