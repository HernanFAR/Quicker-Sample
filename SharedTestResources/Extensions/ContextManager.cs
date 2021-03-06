using Infrastructure.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedTestResources.Extensions
{
    public static class ContextManager
    {
        public static ApplicationDbContext CreateContext(Guid contextId, IEnumerable<IInterceptor>? interceptors = null)
        {
            var connection = new SqliteConnection($"Data Source={contextId.ToString().Replace("-", "")};Mode=Memory;Cache=Shared");
            connection.Open();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .ConfigureWarnings(e => e.Ignore(RelationalEventId.AmbientTransactionWarning));

            if (interceptors != null && interceptors.Any())
            {
                builder.AddInterceptors(interceptors);
            }

            var context = new ApplicationDbContext(builder.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
