using _011Global.Shared.CustomerDbContext;
using _011Global.Shared.CustomerDbContext.Interfaces;
using _011Global.Shared.CustomerDbContext.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace TransasctionsHubUnitTesting.Hooks;

[Binding]
public class Hooks
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.test.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<Hooks>()
            .Build();
        
        var services = new ServiceCollection();
        
        // Configuration
        services.AddSingleton<IConfiguration>(config);
        
        // CustomerContext
        services.AddDbContext<CustomerContext>(options => options.UseSqlServer(config.GetConnectionString("TransactionsHubDB"),
            sqlServerOptions => sqlServerOptions.CommandTimeout(120).EnableRetryOnFailure()));
            
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        
        // Add HttpClient
        services.AddHttpClient();

        return services;
    }
}