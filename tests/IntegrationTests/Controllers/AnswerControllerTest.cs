using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestApi.Data;
using RestApi.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class AnswerControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient client;
        public AnswerControllerTest(CustomWebApplicationFactory<Program> factory)
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
            var httpResponse = await client.GetAsync("/answers");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<List<Answer>>(stringResponse);

            Assert.Contains(answer, p => p.Description == "Description1");
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync("/answers/852b5984-0dc4-459e-a3a8-0a81b0e1a2a5");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Answer>(stringResponse);

            answer.Description.Should().Be("Description2");
        }

        [Fact]
        public async Task CreateAsync()
        {
            var rawAnswer = new Answer
            {
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Created answer",
                IsDeleted = false
            };

            var myContent = JsonConvert.SerializeObject(rawAnswer);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/answers", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Answer>(stringResponse);

            answer.Description.Should().Be("Created answer");
        }

        [Fact]
        public async Task CreateBulkAsync()
        {
            var answer1 = new Answer
            {
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Created Description1",
                IsDeleted = false
            };

            var answer2 = new Answer
            {
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Created Description2",
                IsDeleted = false
            };

            var answerList = new List<Answer>
            {
                answer1,
                answer2
            };

            var myContent = JsonConvert.SerializeObject(answerList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/answers/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<List<Answer>>(stringResponse);

            answers.Count.Should().Equals(2);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var answer1 = new Answer
            {
                Id = new Guid("e5e1d944-3296-4532-adfa-00ec05eb2cfa"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description1",
                IsDeleted = false
            };

            var myContent = JsonConvert.SerializeObject(answer1);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/answers", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Answer>(stringResponse);

            answer.Description.Should().Be("Description1");
        }

        [Fact]
        public async Task UpdateBulkAsync()
        {
            var answer1 = new Answer
            {
                Id = new Guid("e5e1d944-3296-4532-adfa-00ec05eb2cfa"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description1",
                IsDeleted = false
            };

            var answer2 = new Answer
            {
                Id = new Guid("852b5984-0dc4-459e-a3a8-0a81b0e1a2a5"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description2",
                IsDeleted = false
            };

            var answerList = new List<Answer>
            {
                answer1,
                answer2
            };

            var myContent = JsonConvert.SerializeObject(answerList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/answers/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<List<Answer>>(stringResponse);

            answers.Count.Should().Equals(2);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.DeleteAsync("/answers/852b5984-0dc4-459e-a3a8-0a81b0e1a2a5");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Answer>(stringResponse);

            answer.IsDeleted.Should().Be(true);
        }

        [Fact]
        public async Task DeleteBulkAsync()
        {
            var answer1 = new Answer
            {
                Id = new Guid("e5e1d944-3296-4532-adfa-00ec05eb2cfa"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description1",
                IsDeleted = false
            };

            var answer2 = new Answer
            {
                Id = new Guid("852b5984-0dc4-459e-a3a8-0a81b0e1a2a5"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description2",
                IsDeleted = false
            };

            var answerList = new List<Answer>
            {
                answer1,
                answer2
            };

            var myContent = JsonConvert.SerializeObject(answerList);
            var request = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/answers/bulk", UriKind.Relative),
                Content = new StringContent(myContent, Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(request);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<List<Answer>>(stringResponse);

            answers.Should().OnlyContain(question => question.IsDeleted == true);
        }
    }
}
