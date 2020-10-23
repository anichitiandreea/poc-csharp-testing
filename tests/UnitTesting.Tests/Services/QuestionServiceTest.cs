using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTesting.Data;
using UnitTesting.Domain;
using UnitTesting.Services;

namespace UnitTesting.Tests.Services
{
    public class QuestionServiceTest
    {
        [Test]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);

            //Act
            var result = await service.GetAllAsync();

            //Assert
            result.Should().BeOfType<List<Question>>();
        }

        [Test]
        public async Task GivenGetByIdAsyncWhenDataExistsThenReturnsData()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseNpgsql("Server=localhost;Port=5432;Database=UnitTesting;User Id=postgres;Password=parola;")
                .Options;
            var context = new DatabaseContext(options);
            var fixture = new Fixture();

            var transaction = context.Database.BeginTransaction();

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

            context.Users.Add(user);
            context.Questions.Add(question);
            context.Answers.Add(answer);
            context.SaveChanges();

            var service = new QuestionService(context);
            var id = question.Id;

            //Act
            var result = await service.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            transaction.Rollback();
            transaction.Dispose();
        }

        [Test]
        public async Task GivenCreateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);
            var question = new Question
            {
                Id = new Guid("ef3ee9ea-0d48-46ae-babb-13db914650a4"),
                UserId = Guid.NewGuid(),
                Title = "Question4",
                Description = "Description4",
                IsDeleted = false,
                Answers = null
            };

            //Act
            await service.CreateAsync(question);

            //Assert
            context.Questions.Should().HaveCount(4);
        }

        [Test]
        public async Task GivenCreateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);
            var question1 = new Question
            {
                Id = new Guid("3b37193c-9923-41f2-a9e4-ee7b355b932c"),
                UserId = Guid.NewGuid(),
                Title = "Question4",
                Description = "Description4",
                IsDeleted = false,
                Answers = null
            };

            var question2 = new Question
            {
                Id = new Guid("0e10258e-5ce6-4486-b9ca-724f353aa5d7"),
                UserId = Guid.NewGuid(),
                Title = "Question4",
                Description = "Description4",
                IsDeleted = false,
                Answers = null
            };

            var bulk = new List<Question>
            {
                question1,
                question2
            };

            //Act
            await service.CreateBulkAsync(bulk);

            //Assert
            context.Questions.Should().HaveCount(5);
        }

        [Test]

        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            // Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);
            var updatedQuestion = new Question
            {
                Id = new Guid("3f0b1175-dbd1-44f3-a729-085c96c08dba"),
                UserId = new Guid("a0e929c2-5662-47fc-9a7d-872fe566506e"),
                Title = "Question1 updated",
                Description = "Description1 updated",
                IsDeleted = false,
                Answers = null
            };

            //Act
            await service.UpdateAsync(updatedQuestion);

            //Assert
            context.Questions.FirstOrDefault().Should().Be(updatedQuestion);
        }

        [Test]
        public async Task GivenUpdateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);
            var updatedQuestion1 = new Question
            {
                Id = new Guid("3f0b1175-dbd1-44f3-a729-085c96c08dba"),
                UserId = new Guid("a0e929c2-5662-47fc-9a7d-872fe566506e"),
                Title = "Question1 updated",
                Description = "Description1 updated",
                IsDeleted = false,
                Answers = null
            };

            var updatedQuestion2 = new Question
            {
                Id = new Guid("c03c7333-6636-4a3d-900d-14bd5dbcd13b"),
                UserId = new Guid("9f9e3aaf-1eca-46fe-8900-12cb56b0634d"),
                Title = "Question3 updated",
                Description = "Description3 updated",
                IsDeleted = false,
                Answers = null
            };

            var bulk = new List<Question>
            {
                updatedQuestion1,
                updatedQuestion2
            };

            //Act
            await service.UpdateBulkAsync(bulk);

            //Assert
            context.Questions.FirstOrDefault().Should().BeSameAs(updatedQuestion1);
            context.Questions.LastOrDefault().Should().BeSameAs(updatedQuestion2);
        }

        [Test]
        public async Task GivenDeleteAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);
            var question = await context.Questions.FirstOrDefaultAsync();

            //Act
            await service.DeleteAsync(question);

            //Assert
            context.Questions.FirstOrDefault(a => a.Id == question.Id).IsDeleted.Should().Be(true);
        }

        [Test]
        public async Task GivenDeleteBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new QuestionService(context);
            var question1 = await context.Questions.FirstOrDefaultAsync();
            var question2 = await context.Questions.LastOrDefaultAsync();
            var bulk = new List<Question>
            {
                question1,
                question2
            };

            //Act
            await service.DeleteBulkAsync(bulk);

            //Assert
            context.Questions.FirstOrDefault(a => a.Id == question1.Id).IsDeleted.Should().Be(true);
            context.Questions.FirstOrDefault(a => a.Id == question2.Id).IsDeleted.Should().Be(true);
        }
    }
}
