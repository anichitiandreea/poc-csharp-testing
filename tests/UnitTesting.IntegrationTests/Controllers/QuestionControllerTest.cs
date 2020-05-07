using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UnitTesting.Data;
using UnitTesting.Domain;
using Xunit;

namespace UnitTesting.IntegrationTests.Controllers
{
    public class QuestionControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public QuestionControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();

            using (var scope = factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<DatabaseContext>();
                if (dbContext.Database.EnsureCreated())
                {
                    SeedData.PopulateTestData(dbContext);
                }
            }
        }
        
        [Fact]
        public async Task GetAllAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync("/question");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<List<Question>>(stringResponse);

            Assert.Contains(questions, p => p.Title == "Wayne");
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync("/question/28066e80-31c4-4fdd-b8db-236af203b76d");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<Question>(stringResponse);

            question.Title.Should().Be("Wayne");
        }
    }
}