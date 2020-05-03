using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnitTesting.Domain;
using Xunit;

namespace UnitTesting.IntegrationTests.Controllers
{
    public class QuestionControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public QuestionControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/question");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<Question>>(stringResponse);
            Assert.Contains(players, p => p.Title == "Wayne");
        }
    }
}