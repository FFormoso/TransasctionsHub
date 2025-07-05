using _011Global.Shared.CustomerDbContext;
using _011Global.Shared.CustomerDbContext.Interfaces;
using _011Global.Shared.CustomerDbContext.Repos;
using _011Global.Shared.JobsServiceDBContext;
using _011Global.Shared.JobsServiceDBContext.Interfaces;
using _011Global.Shared.JobsServiceDBContext.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _011Global.Shared
{
    public static class DBContextsService
    {
        public static IServiceCollection RegisterDbContexts(this IServiceCollection services, string? connectionString)
        {
            // JobsServiceContext
            services.AddDbContext<JobsServiceContext>(options => options.UseSqlServer(connectionString,
            sqlServerOptions => sqlServerOptions.CommandTimeout(120).EnableRetryOnFailure()));

            services.AddTransient<IJobsServiceRepository, JobsServiceRepository>();
            
            // CustomerContext
            services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connectionString,
                sqlServerOptions => sqlServerOptions.CommandTimeout(120).EnableRetryOnFailure()));
            
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
