using BaRemake.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaRemake.Data;

public static class DbContextFactory
{
    /// <summary>
    /// Registers ApplicationDbContext with the chosen provider from configuration.
    /// Set "DatabaseProvider" in appsettings.json to "SqlServer", "PostgreSQL", or "MySQL".
    /// </summary>
    public static IServiceCollection AddBaRemakeDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var providerStr = configuration["DatabaseProvider"] ?? "SqlServer";
        var provider = Enum.Parse<DatabaseProvider>(providerStr, ignoreCase: true);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    var pgConn = configuration.GetConnectionString("PostgreSQL")
                        ?? throw new InvalidOperationException("PostgreSQL connection string is missing.");
                    options.UseNpgsql(pgConn,
                        o => o.MigrationsHistoryTable("__EFMigrationsHistory", "public"));
                    break;

                case DatabaseProvider.MySQL:
                    var myConn = configuration.GetConnectionString("MySQL")
                        ?? throw new InvalidOperationException("MySQL connection string is missing.");
                    options.UseMySql(myConn, ServerVersion.AutoDetect(myConn),
                        o => o.MigrationsHistoryTable("__EFMigrationsHistory"));
                    break;

                case DatabaseProvider.SqlServer:
                default:
                    var sqlConn = configuration.GetConnectionString("SqlServer")
                        ?? configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("SQL Server connection string is missing.");
                    options.UseSqlServer(sqlConn,
                        o => o.MigrationsHistoryTable("__EFMigrationsHistory", "dbo"));
                    break;
            }
        });

        return services;
    }
}
