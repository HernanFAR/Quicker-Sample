using System;
using Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace SharedTestResources
{
    public static class ApplicationDbContextExtensions
    {
        public static ApplicationDbContext CreateContext(Guid contextId, IEnumerable<IInterceptor>? interceptors = null)
        {
            var connection = new SqliteConnection($"Data Source={contextId.ToString().Replace("-", "")};Mode=Memory;Cache=Shared");
            connection.Open();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection);

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
