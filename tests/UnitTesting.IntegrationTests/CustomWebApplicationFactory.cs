using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using UnitTesting.Data;

namespace UnitTesting.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        public DatabaseContext databaseContext;
        public IDbContextTransaction transaction;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                // We need to remove DatabaseContext service descriptor 
                // because Startup has already registered the context
                var serviceDescriptor = services
                    .FirstOrDefault(serviceDescriptor => 
                        serviceDescriptor.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                
                if(serviceDescriptor != null)
                {
                    services.Remove(serviceDescriptor);
                }

                // Register the context to use in memory database
                // Uncomment this code to run QuestionController and UserController tests
                /*services.AddDbContext<DatabaseContext>(options => {
                    options.UseInMemoryDatabase("InMemoryDatabase");
                });*/

                // Register the context
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseNpgsql("Server=localhost;Port=5432;Database=UnitTesting;User Id=postgres;Password=parola;"),
                    ServiceLifetime.Singleton, ServiceLifetime.Singleton
                );
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseNpgsql("Server=localhost;Port=5432;Database=UnitTesting;User Id=postgres;Password=parola;")
                .Options;

                databaseContext = new DatabaseContext(options);
                databaseContext.Database.Migrate();
                services.AddSingleton(databaseContext);
            });
        }

    }
}
