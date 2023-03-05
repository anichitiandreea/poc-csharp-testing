using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTesting.Domain;
using UnitTesting.Services;

namespace UnitTesting.Tests.Services
{
    public class UserServiceTest
    {
        [Test]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);

            //Act
            var result = await service.GetAllAsync();

            //Assert
            result.Should().BeOfType<List<User>>();
        }

        [Test]
        public async Task GivenGetByIdAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);
            var id = new Guid("8d446640-d132-4b83-9ada-ed13f07a6ccc");

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
            var service = new UserService(context);
            var question = new User
            {
                Id = new Guid("be0e95af-3901-4292-afe3-793e92248681"),
                Name = "User4",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            //Act
            await service.CreateAsync(question);

            //Assert
            context.Users.Should().HaveCount(4);
        }

        [Test]
        public async Task GivenCreateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);
            var user1 = new User
            {
                Id = new Guid("b5dbb00a-2a90-4983-849a-cff65807ff42"),
                Name = "User4",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var user2 = new User
            {
                Id = new Guid("030ee30a-feb4-483e-a1c1-98e39cab7353"),
                Name = "User5",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var bulk = new List<User>
            {
                user1,
                user2
            };

            //Act
            await service.CreateBulkAsync(bulk);

            //Assert
            context.Users.Should().HaveCount(5);
        }

        [Test]

        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            // Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);
            var updatedUser = new User
            {
                Id = new Guid("6424fb63-1912-4a5a-b890-1b7b8d5fffa8"),
                Name = "User1 updated",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            //Act
            await service.UpdateAsync(updatedUser);

            //Assert
            context.Users.FirstOrDefault(user => user.Id == updatedUser.Id)
                .Should().Be(updatedUser);
        }

        [Test]
        public async Task GivenUpdateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);
            var updatedUser1 = new User
            {
                Id = new Guid("6424fb63-1912-4a5a-b890-1b7b8d5fffa8"),
                Name = "User1 updated",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var updatedUser2 = new User
            {
                Id = new Guid("1bf1603a-5aac-4502-b99f-5befdf046603"),
                Name = "User3 updated",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var bulk = new List<User>
            {
                updatedUser1,
                updatedUser2
            };

            //Act
            await service.UpdateBulkAsync(bulk);

            //Assert
            context.Users.FirstOrDefault().Should().BeSameAs(updatedUser2);
            context.Users.LastOrDefault().Should().BeSameAs(updatedUser1);
        }

        [Test]
        public async Task GivenDeleteAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);
            var user = await context.Users.FirstOrDefaultAsync();

            //Act
            await service.DeleteAsync(user);

            //Assert
            context.Users.FirstOrDefault(a => a.Id == user.Id).IsDeleted.Should().Be(true);
        }

        [Test]
        public async Task GivenDeleteBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            var context = new FakeContext().GetContextWithData();
            var service = new UserService(context);
            var user1 = await context.Users.FirstOrDefaultAsync();
            var user2 = await context.Users.LastOrDefaultAsync();
            var bulk = new List<User>
            {
                user1,
                user2
            };

            //Act
            await service.DeleteBulkAsync(bulk);

            //Assert
            context.Users.FirstOrDefault(a => a.Id == user1.Id).IsDeleted.Should().Be(true);
            context.Users.FirstOrDefault(a => a.Id == user2.Id).IsDeleted.Should().Be(true);
        }
    }
}
