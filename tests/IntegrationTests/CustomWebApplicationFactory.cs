using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApi.Data;
using System;
using System.Linq;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DatabaseContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Register the context to use in memory database
                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDatabase");
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                {
                    try
                    {
                        //appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }
    }
}
