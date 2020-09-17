using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Domain;
using Xunit;

namespace UnitTesting.IntegrationTests.Controllers
{
    public class AnswerControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly IDbContextTransaction transaction;

        public AnswerControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
            this.factory = factory;
            transaction = factory.databaseContext.Database.BeginTransaction();
        }

        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.Answers.Add(answer);
            factory.databaseContext.SaveChanges();

            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync("/answers");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<Answer>>(stringResponse);

            Assert.Contains(response, p => p.Description == answer.Description);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.Answers.Add(answer);
            factory.databaseContext.SaveChanges();

            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync($"/answers/{answer.Id}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Answer>(stringResponse);

            response.Description.Should().Be(answer.Description);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.SaveChanges();

            var myContent = JsonConvert.SerializeObject(answer);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/answers", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Answer>(stringResponse);

            response.Description.Should().Be(answer.Description);
        }

        [Fact]
        public async Task CreateBulkAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.SaveChanges();

            var answerList = new List<Answer>
            {
                answer
            };

            var myContent = JsonConvert.SerializeObject(answerList);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync("/answers/bulk", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<Answer>>(stringResponse);

            ((IEnumerable)response).Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.Answers.Add(answer);
            factory.databaseContext.SaveChanges();
            factory.databaseContext.Entry(answer).State = EntityState.Detached;
            factory.databaseContext.SaveChanges();

            answer.Description = "Updated description";

            var myContent = JsonConvert.SerializeObject(answer);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/answers", httpContent);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Answer>(stringResponse);

            response.Description.Should().Be(answer.Description);
        }

        [Fact]
        public async Task UpdateBulkAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.Answers.Add(answer);
            factory.databaseContext.SaveChanges();
            factory.databaseContext.Entry(answer).State = EntityState.Detached;
            factory.databaseContext.SaveChanges();

            var answerList = new List<Answer>
            {
                answer
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

            ((IEnumerable)answers).Should().HaveCount(1);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.Answers.Add(answer);
            factory.databaseContext.SaveChanges();

            // The endpoint or route of the controller action.
            var httpResponse = await client.DeleteAsync($"/answers/{answer.Id}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Answer>(stringResponse);

            response.IsDeleted.Should().Be(true);
        }

        [Fact]
        public async Task DeleteBulkAsync()
        {
            // Arrange
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .Without(p => p.Answers)
                .Without(p => p.Questions)
                .Create();
            var question = fixture.Build<Question>()
                .Without(p => p.Answers)
                .Create();
            var answer = fixture.Create<Answer>();
            answer.UserId = user.Id;
            answer.QuestionId = question.Id;
            question.UserId = user.Id;

            factory.databaseContext.Users.Add(user);
            factory.databaseContext.Questions.Add(question);
            factory.databaseContext.Answers.Add(answer);
            factory.databaseContext.SaveChanges();
            factory.databaseContext.Entry(answer).State = EntityState.Detached;
            factory.databaseContext.SaveChanges();

            var answerList = new List<Answer>
            {
                answer
            };

            var myContent = JsonConvert.SerializeObject(answerList);
            var request = new HttpRequestMessage
            {
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

        public void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
        }
    }
}
