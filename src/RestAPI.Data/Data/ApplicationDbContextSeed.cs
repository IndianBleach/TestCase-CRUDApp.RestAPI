using RestAPI.Data.Entities.ProviderEntity;


namespace RestAPI.Data.Data
{
    public static class ApplicationDbContextSeed
    {
        public async static Task SeedDatabaseAsync(ApplicationDbContext ctx)
        {
            if (ctx.Providers.Count() == 0)
            {
                Provider[] providers = new Provider[]
                {
                    new Provider("Михаил"),
                    new Provider("Евгения"),
                    new Provider("Анастасия"),
                    new Provider("Владимир"),
                };

                await ctx.Providers.AddRangeAsync(providers);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
