using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Domain;
using UnitTesting.Services;

namespace UnitTesting.Tests.Services
{
    public class AnswerServiceTest
    {
        [Test]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);

            //Act
            var result = await service.GetAllAsync();

            //Assert
            result.Should().BeOfType<List<Answer>>();
        }

        [Test]
        public async Task GivenGetByIdAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var id = new Guid("5b6cff6c-f7bf-4f6d-81af-17daeff03e48");

            //Act
            var result = await service.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task GivenCreateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var question = new Answer
            {
                Id = new Guid("8c64566e-c0bc-4833-9f2e-16acf598753c"),
                Description = "Description2",
                QuestionId = new Guid("7675205b-e341-4665-9fbe-f097f6e41100"),
                UserId = new Guid("60583184-b91c-4bf6-b692-d409d31d0f39"),
                IsDeleted = false
            };

            //Act
            await service.CreateAsync(question);

            //Assert
            context.Answers.Should().HaveCount(4);
        }

        [Test]
        public async Task GivenCreateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var answer1 = new Answer
            {
                Id = new Guid("8c634d6e-c0bc-4833-562e-16acf598753c"),
                Description = "Description2",
                QuestionId = new Guid("7444205b-e341-4665-9fbe-f097f6e41100"),
                UserId = new Guid("6055aa84-b91c-4bf6-b692-d409d31d0f39"),
                IsDeleted = false
            };

            var answer2 = new Answer
            {
                Id = new Guid("8c634d6e-c0bc-4833-9f2e-16a56818753c"),
                Description = "Description2",
                QuestionId = new Guid("7444205b-e341-4665-9fbe-f097f6e41100"),
                UserId = new Guid("6055aa84-b91c-4bf6-b692-d409d31d0f39"),
                IsDeleted = false
            };

            var bulk = new List<Answer>
            {
                answer1,
                answer2
            };

            //Act
            await service.CreateBulkAsync(bulk);

            //Assert
            context.Answers.Should().HaveCount(5);
        }

        [Test]

        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            // Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var updatedQuestion = new Answer
            {
                Id = new Guid("5b6cff6c-f7bf-4f6d-81af-17daeff03e48"),
                Description = "Description1 updated",
                QuestionId = new Guid("8271de8a-c643-484f-b5d2-07ea23b09fa1"),
                UserId = new Guid("c98fb889-cef4-4023-aaba-55a501504b43"),
                IsDeleted = false
            };

            //Act
            await service.UpdateAsync(updatedQuestion);

            //Assert
            context.Answers.LastOrDefault().Description.Should().Be("Description1 updated");
        }

        [Test]
        public async Task GivenUpdateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var updatedAnswer1 = new Answer
            {
                Id = new Guid("5b6cff6c-f7bf-4f6d-81af-17daeff03e48"),
                Description = "Description1",
                QuestionId = new Guid("8271de8a-c643-484f-b5d2-07ea23b09fa1"),
                UserId = new Guid("c98fb889-cef4-4023-aaba-55a501504b43"),
                IsDeleted = false
            };

            var updatedAnswer2 = new Answer
            {
                Id = new Guid("dba9fc6a-920f-44b9-b800-cb8c2c7bd4d8"),
                Description = "Description3",
                QuestionId = new Guid("a42aef11-858b-4b39-b0ad-7339388ada57"),
                UserId = new Guid("12d5682c-e781-4bef-9d05-cc8a1753d77f"),
                IsDeleted = false
            };

            var bulk = new List<Answer>
            {
                updatedAnswer1,
                updatedAnswer2
            };

            //Act
            await service.UpdateBulkAsync(bulk);

            //Assert
            context.Answers.FirstOrDefault().Should().BeSameAs(updatedAnswer2);
            context.Answers.LastOrDefault().Should().BeSameAs(updatedAnswer1);
        }

        [Test]
        public async Task GivenDeleteAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var answer = await context.Answers.FirstOrDefaultAsync();

            //Act
            await service.DeleteAsync(answer);

            //Assert
            context.Answers.Should().HaveCount(2);
        }

        [Test]
        public async Task GivenDeleteBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new AnswerService(context);
            var answer1 = await context.Answers.FirstOrDefaultAsync();
            var answer2 = await context.Answers.LastOrDefaultAsync();
            var bulk = new List<Answer>
            {
                answer1,
                answer2
            };

            //Act
            await service.DeleteBulkAsync(bulk);

            //Assert
            context.Answers.Should().HaveCount(1);
        }
    }
}
