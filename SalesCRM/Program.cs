using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Interfaces;
using Sales.DependencyInjection;

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
            
            services.RegisterServices(connectionString);
            services.AddTransient<MainCRM>();
        }
        private static async Task EnsureDatabaseUpdated(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbService = scope.ServiceProvider.GetRequiredService<IDataBaseService>();

            try
            {
                var pendingCount = await dbService.GetPendingMigrationsCountAsync();

                if (pendingCount > 0)
                {
                    var message = $"Database updates detected ({pendingCount}).\n" +
                                  "Do you want to update now?\n\n" +
                                  "Backup is recommended to avoid errors.";

                    var result = MessageBox.Show(message, "DB updating",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        await dbService.MigrateAsync();
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