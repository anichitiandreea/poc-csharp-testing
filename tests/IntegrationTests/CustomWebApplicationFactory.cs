using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApi;
using RestApi.Data;
using System.Linq;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
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
                services.AddDbContext<DatabaseContext>(options => {
                    options.UseInMemoryDatabase("InMemoryDatabase");
                });
            });
        }
    }
}
