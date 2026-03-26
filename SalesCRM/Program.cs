using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Data;
using Sales.Infrastructure.Repositories;

namespace SalesCRM
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            ConfigureServices(services);
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var mainForm = serviceProvider.GetRequiredService<Form1>();
                Application.Run(mainForm);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=localhost;Database=AdventureWorks2025;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AdventureWorksContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
                }));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddTransient<Form1>();
        }
    }
}