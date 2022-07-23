using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAPI.Application.Services.OrderItemService;
using RestAPI.Application.Services.OrderService;
using RestAPI.Data.Data;

namespace RestAPI.Web.Extensions
{
    public static class ApplicationServiceExtensions
    {        
        public static void AddCoreServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("MSSQLConnectionString")));

            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IOrderItemService, OrderItemService>();
        }
    }
}
