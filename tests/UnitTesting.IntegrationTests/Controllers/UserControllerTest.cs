using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using UnitTesting.Data;
using UnitTesting.Domain;
using Xunit;

namespace UnitTesting.IntegrationTests.Controllers
{
    public class UserControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public UserControllerTest(CustomWebApplicationFactory<Startup> factory)
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
            var httpResponse = await client.GetAsync("/users");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<User>>(stringResponse);

            Assert.Contains(user, p => p.Name == "User1");
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync("/users/9876deb5-27ed-41c1-8998-86c998275acf");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);

            user.Name.Should().Be("User1");
        }

        [Fact]
        public async Task CreateAsync()
        {
            var user1 = new User
            {
                Name = "User3",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var myContent = JsonConvert.SerializeObject(user1);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/users", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);

            user.Name.Should().Be("User3");
        }

        [Fact]
        public async Task CreateBulkAsync()
        {
            var user1 = new User
            {
                Name = "User5",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };
            var user2 = new User
            {
                Name = "User4",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var userList = new List<User>
            {
                user1,
                user2
            };

            var myContent = JsonConvert.SerializeObject(userList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/users/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(stringResponse);

            ((IEnumerable)users).Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var user1 = new User
            {
                Id = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Name = "User1",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var myContent = JsonConvert.SerializeObject(user1);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/users", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);

            user.Name.Should().Be("User1");
        }

        [Fact]
        public async Task UpdateBulkAsync()
        {
            var user1 = new User
            {
                Id = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Name = "User1",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var user2 = new User
            {
                Id = new Guid("74256f8c-fcab-448b-9add-1158ed9056fd"),
                Name = "User2",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var userList = new List<User>
            {
                user1,
                user2
            };

            var myContent = JsonConvert.SerializeObject(userList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/users/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(stringResponse);

            ((IEnumerable)users).Should().HaveCount(2);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await client.DeleteAsync("/users/9876deb5-27ed-41c1-8998-86c998275acf");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);

            user.IsDeleted.Should().Be(true);
        }

        [Fact]
        public async Task DeleteBulkAsync()
        {
            var user1 = new User
            {
                Id = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Name = "User1",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };
            var user2 = new User
            {
                Id = new Guid("74256f8c-fcab-448b-9add-1158ed9056fd"),
                Name = "User2",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var userList = new List<User>
            {
                user1,
                user2
            };

            var myContent = JsonConvert.SerializeObject(userList);
            var request = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/users/bulk", UriKind.Relative),
                Content = new StringContent(myContent, Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(request);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(stringResponse);

            users.Should().OnlyContain(question => question.IsDeleted == true);
        }
    }
}
