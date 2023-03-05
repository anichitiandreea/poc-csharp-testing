using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestApi.Data;
using RestApi.Domain;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class QuestionControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient client;
        public QuestionControllerTest(CustomWebApplicationFactory<Program> factory)
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
            var httpResponse = await client.GetAsync("/questions");

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
            var httpResponse = await client.GetAsync("/questions/28066e80-31c4-4fdd-b8db-236af203b76d");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<Question>(stringResponse);

            question.Title.Should().Be("Wayne");
        }

        [Fact]
        public async Task CreateAsync()
        {
            var rawQuestion = new Question
            {
                UserId = new Guid("62309c30-9a5f-45de-bdc2-bdb7aeeb1b5a"),
                Title = "Created question",
                Description = "My description",
                IsDeleted = false,
                Answers = null
            };

            var myContent = JsonConvert.SerializeObject(rawQuestion);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/questions", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<Question>(stringResponse);

            question.Title.Should().Be("Created question");
        }

        [Fact]
        public async Task CreateBulkAsync()
        {
            var rawQuestion1 = new Question
            {
                UserId = new Guid("62309c30-9a5f-45de-bdc2-bdb7aeeb1b5a"),
                Title = "Created question",
                Description = "My description",
                IsDeleted = false,
                Answers = null
            };

            var rawQuestion2 = new Question
            {
                UserId = new Guid("62309c30-9a5f-45de-bdc2-bdb7aeeb1b5a"),
                Title = "Created question",
                Description = "My description",
                IsDeleted = false,
                Answers = null
            };

            var questionList = new List<Question>
            {
                rawQuestion1,
                rawQuestion2
            };

            var myContent = JsonConvert.SerializeObject(questionList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/questions/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<List<Question>>(stringResponse);

            questions.Count.Should().Equals(2);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var rawQuestion = new Question
            {
                Id = new Guid("28066e80-31c4-4fdd-b8db-236af203b76d"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "Wayne",
                Description = "Description1",
                IsDeleted = false,
                Answers = null
            };

            var myContent = JsonConvert.SerializeObject(rawQuestion);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/questions", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<Question>(stringResponse);

            question.Title.Should().Be("Wayne");
        }

        [Fact]
        public async Task UpdateBulkAsync()
        {
            var rawQuestion1 = new Question
            {
                Id = new Guid("28066e80-31c4-4fdd-b8db-236af203b76d"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "Wayne",
                Description = "Description1",
                IsDeleted = false,
                Answers = null
            };

            var rawQuestion2 = new Question
            {
                Id = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "My second Question",
                Description = "Description2",
                IsDeleted = false,
                Answers = null
            };

            var questionList = new List<Question>
            {
                rawQuestion1,
                rawQuestion2
            };

            var myContent = JsonConvert.SerializeObject(questionList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/questions/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<List<Question>>(stringResponse);

            questions.Count.Should().Equals(2);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.DeleteAsync("/questions/28066e80-31c4-4fdd-b8db-236af203b76d");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<Question>(stringResponse);

            question.IsDeleted.Should().Be(true);
        }

        [Fact]
        public async Task DeleteBulkAsync()
        {
            var rawQuestion1 = new Question
            {
                Id = new Guid("28066e80-31c4-4fdd-b8db-236af203b76d"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "Wayne",
                Description = "Description1",
                IsDeleted = false,
                Answers = null
            };

            var rawQuestion2 = new Question
            {
                Id = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "My second Question",
                Description = "Description2",
                IsDeleted = false,
                Answers = null
            };

            var questionList = new List<Question>
            {
                rawQuestion1,
                rawQuestion2
            };

            var myContent = JsonConvert.SerializeObject(questionList);
            var request = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/questions/bulk", UriKind.Relative),
                Content = new StringContent(myContent, Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(request);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<List<Question>>(stringResponse);

            questions.Should().OnlyContain(question => question.IsDeleted == true);
        }
    }
}
