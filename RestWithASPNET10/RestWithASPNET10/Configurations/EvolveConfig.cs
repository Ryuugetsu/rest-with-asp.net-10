using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;

namespace RestWithASPNET10.Configurations
{
    public static class EvolveConfig
    {
        public static IServiceCollection AddEvolveMigration(this IServiceCollection services,
                                                                 IConfiguration configuration,
                                                                 IWebHostEnvironment environment)
        {

            if (environment.IsDevelopment())
            {
                if (!environment.IsDevelopment()) { return services; }

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                }

                try
                {
                    ExecuteMigration(connectionString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Evolve migration failed: {ex.Message}");
                    throw;
                }
            }


            return services;
        }

        public static void ExecuteMigration(string connectionString)
        {
            using var evolveConnection = new SqlConnection(connectionString);

            string migrationsPath = Path.Combine(AppContext.BaseDirectory, "DB", "Migrations");
            string datasetPath = Path.Combine(AppContext.BaseDirectory, "DB", "Dataset");

            Evolve evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
            {
                Locations = new[] { migrationsPath, datasetPath },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
    }
}
