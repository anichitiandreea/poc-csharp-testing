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
    [Property("NUnit", "Controller | Answer")]
    public class AnswerControllerTest
    {
        private Mock<IAnswerService> mockAnswerService;
        private AnswerController AnswerController;

        [SetUp]
        public void Setup()
        {
            mockAnswerService = new Mock<IAnswerService>();
            AnswerController = new AnswerController(
                mockAnswerService.Object
            );
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<Answer>())
                .Verifiable();

            //Act
            var result = await AnswerController.GetAllAsync();

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(It.IsAny<List<Answer>>())
                .Verifiable();

            //Act
            var result = await AnswerController.GetAllAsync();

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetAllAsync())
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.GetAllAsync();

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Answer())
                .Verifiable();

            //Act
            var result = await AnswerController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<Answer>())
                .Verifiable();

            //Act
            var result = await AnswerController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.CreateAsync(It.IsAny<Answer>()))
                .Verifiable();

            //Act
            var result = await AnswerController.CreateAsync(It.IsAny<Answer>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.CreateAsync(It.IsAny<Answer>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.CreateAsync(It.IsAny<Answer>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.CreateBulkAsync(It.IsAny<IList<Answer>>()))
                .Verifiable();

            //Act
            var result = await AnswerController.CreateBulkAsync(It.IsAny<IList<Answer>>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.CreateBulkAsync(It.IsAny<IList<Answer>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.CreateBulkAsync(It.IsAny<IList<Answer>>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.UpdateAsync(It.IsAny<Answer>()))
                .Verifiable();

            //Act
            var result = await AnswerController.UpdateAsync(It.IsAny<Answer>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.UpdateAsync(It.IsAny<Answer>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.UpdateAsync(It.IsAny<Answer>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.UpdateBulkAsync(It.IsAny<IList<Answer>>()))
                .Verifiable();

            //Act
            var result = await AnswerController.UpdateBulkAsync(It.IsAny<IList<Answer>>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.UpdateBulkAsync(It.IsAny<IList<Answer>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.UpdateBulkAsync(It.IsAny<IList<Answer>>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Answer())
                .Verifiable();
            mockAnswerService
                .Setup(_ => _.DeleteAsync(It.IsAny<Answer>()))
                .Verifiable();

            //Act
            var result = await AnswerController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<Answer>())
                .Verifiable();

            //Act
            var result = await AnswerController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.DeleteBulkAsync(It.IsAny<IList<Answer>>()))
                .Verifiable();

            //Act
            var result = await AnswerController.DeleteBulkAsync(It.IsAny<IList<Answer>>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockAnswerService
                .Setup(_ => _.DeleteBulkAsync(It.IsAny<IList<Answer>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await AnswerController.DeleteBulkAsync(It.IsAny<IList<Answer>>());

            //Assert
            mockAnswerService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
