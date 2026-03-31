using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Data;
using Sales.Infrastructure.Repositories;
using Sales.Infrastructure.Services;

namespace Sales.DependencyInjection
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString)
        {
            // DbContext
            services.AddDbContext<AdventureWorksContext>(options =>
                options.UseSqlServer(connectionString));

            // Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Services
            services.AddScoped<IDataBaseService, DataBaseService>();
        }
    }
}
