using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsAPI.Infrastructure.Data;

namespace NewsAPI.Infrastructure;

public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AppDbContext>(options =>
            options
              .UseLazyLoadingProxies()
              .EnableSensitiveDataLogging()
              .UseInMemoryDatabase(connectionString)
               //.UseSqlServer(connectionString, sqlServerOptions =>
               //sqlServerOptions.CommandTimeout(120).EnableRetryOnFailure(maxRetryCount: 10,
               //maxRetryDelay: TimeSpan.FromSeconds(30),
               //errorNumbersToAdd: null)
            //)
        ); // will be created in web project root
}
