using Domain.Contexts.UserBoundedContext.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Infrastructure.EntityFrameworkCore.UserRelated
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null) :
            base(context, describer)
        { }
    }
}
