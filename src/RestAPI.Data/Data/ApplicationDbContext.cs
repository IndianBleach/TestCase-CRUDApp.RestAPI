using Microsoft.EntityFrameworkCore;
using RestAPI.Data.Entities.OrderEntity;
using RestAPI.Data.Entities.OrderItemEntity;
using RestAPI.Data.Entities.ProviderEntity;
using System.Reflection;

namespace RestAPI.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Provider> Providers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);            
        }
    }
}