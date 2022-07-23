using Microsoft.Extensions.DependencyInjection;
using RestAPI.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.Extensions
{
    public static class AppicationDbSeed
    {
        public static async Task SeedDatabaseAsync(IServiceScope scope)
        {
            using (scope)
            {
                await ApplicationDbContextSeed
                    .SeedDatabaseAsync(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
            }
        }
    }
}
