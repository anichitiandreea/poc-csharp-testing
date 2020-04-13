using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnitTesting.Controllers;
using UnitTesting.Domain;
using UnitTesting.Services.Interfaces;

namespace UnitTesting.Tests.Controllers
{
    [Property("NUnit", "Controller | User")]
    public class UserControllerTest
    {
        private Mock<IUserService> mockUserService;
        private UserController UserController;

        [SetUp]
        public void Setup()
        {
            mockUserService = new Mock<IUserService>();
            UserController = new UserController(
                mockUserService.Object
            );
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<User>())
                .Verifiable();

            //Act
            var result = await UserController.GetAllAsync();

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(It.IsAny<List<User>>())
                .Verifiable();

            //Act
            var result = await UserController.GetAllAsync();

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetAllAsync())
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.GetAllAsync();

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new User())
                .Verifiable();

            //Act
            var result = await UserController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<User>())
                .Verifiable();

            //Act
            var result = await UserController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.CreateAsync(It.IsAny<User>()))
                .Verifiable();

            //Act
            var result = await UserController.CreateAsync(It.IsAny<User>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.CreateAsync(It.IsAny<User>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.CreateAsync(It.IsAny<User>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.CreateBulkAsync(It.IsAny<IList<User>>()))
                .Verifiable();

            //Act
            var result = await UserController.CreateBulkAsync(It.IsAny<IList<User>>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.CreateBulkAsync(It.IsAny<IList<User>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.CreateBulkAsync(It.IsAny<IList<User>>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.UpdateAsync(It.IsAny<User>()))
                .Verifiable();

            //Act
            var result = await UserController.UpdateAsync(It.IsAny<User>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.UpdateAsync(It.IsAny<User>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.UpdateAsync(It.IsAny<User>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.UpdateBulkAsync(It.IsAny<IList<User>>()))
                .Verifiable();

            //Act
            var result = await UserController.UpdateBulkAsync(It.IsAny<IList<User>>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.UpdateBulkAsync(It.IsAny<IList<User>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.UpdateBulkAsync(It.IsAny<IList<User>>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new User())
                .Verifiable();
            mockUserService
                .Setup(_ => _.DeleteAsync(It.IsAny<User>()))
                .Verifiable();

            //Act
            var result = await UserController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<User>())
                .Verifiable();

            //Act
            var result = await UserController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.DeleteBulkAsync(It.IsAny<IList<User>>()))
                .Verifiable();

            //Act
            var result = await UserController.DeleteBulkAsync(It.IsAny<IList<User>>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockUserService
                .Setup(_ => _.DeleteBulkAsync(It.IsAny<IList<User>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await UserController.DeleteBulkAsync(It.IsAny<IList<User>>());

            //Assert
            mockUserService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
