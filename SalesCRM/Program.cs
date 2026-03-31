using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Data;
using Sales.Infrastructure.Repositories;
using Sales.Infrastructure.Services;

namespace SalesCRM
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                await EnsureDatabaseUpdated(serviceProvider);
                var mainForm = serviceProvider.GetRequiredService<MainCRM>();
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
            services.AddScoped<IDataBaseService, DataBaseService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddTransient<MainCRM>();
        }
        private static async Task EnsureDatabaseUpdated(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AdventureWorksContext>();

            try
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    var message = $"Database updates detected ({pendingMigrations.Count()}).\n" +
                                  "Do you want to update now?\n\n" +
                                  "Backup is recomended to avoid errors.";

                    var result = MessageBox.Show(message, "DB updating",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        await context.Database.MigrateAsync();
                        MessageBox.Show("Database is updated successfully", "Updated",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during loading database: {ex.Message}",
                                "Critical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}