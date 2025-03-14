
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MohTask.Infrastructure;
using MohTask.Infrastructure.Users;
using MohTask.Service.Users;

namespace MohTask.Presentation
{
    /// <summary>
    /// Handles Dependency Injection configuration.
    /// </summary>
    public static class ServiceConfiguration
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register DbContext
            string connectionString = "Data Source=.;Initial Catalog=MohTaskDB;TrustServerCertificate=True;Integrated Security=SSPI";
            services.AddDbContext<MohTaskDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Repository, Services, and Handlers
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<AccountManagement>();
            services.AddScoped<InputAndOutputOperations>();

            return services.BuildServiceProvider();
        }
    }
}
