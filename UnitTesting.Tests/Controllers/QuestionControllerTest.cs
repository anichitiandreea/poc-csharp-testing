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
    [Property("NUnit", "Controller | Question")]
    public class QuestionControllerTest
    {
        private Mock<IQuestionService> mockQuestionService;
        private QuestionController questionController;

        [SetUp]
        public void Setup()
        {
            mockQuestionService = new Mock<IQuestionService>();
            questionController = new QuestionController(
                mockQuestionService.Object
            );
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<Question>())
                .Verifiable();

            //Act
            var result = await questionController.GetAllAsync();

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(It.IsAny<List<Question>>())
                .Verifiable();

            //Act
            var result = await questionController.GetAllAsync();

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetAllAsync())
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.GetAllAsync();

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Question())
                .Verifiable();

            //Act
            var result = await questionController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<Question>())
                .Verifiable();

            //Act
            var result = await questionController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "GET")]
        public async Task GivenGetByIdAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.GetByIdAsync(It.IsAny<Guid>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.CreateAsync(It.IsAny<Question>()))
                .Verifiable();

            //Act
            var result = await questionController.CreateAsync(It.IsAny<Question>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.CreateAsync(It.IsAny<Question>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.CreateAsync(It.IsAny<Question>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.CreateBulkAsync(It.IsAny<IList<Question>>()))
                .Verifiable();

            //Act
            var result = await questionController.CreateBulkAsync(It.IsAny<IList<Question>>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Test]
        [Property("HttpVerb", "POST")]
        public async Task GivenCreateBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.CreateBulkAsync(It.IsAny<IList<Question>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.CreateBulkAsync(It.IsAny<IList<Question>>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.UpdateAsync(It.IsAny<Question>()))
                .Verifiable();

            //Act
            var result = await questionController.UpdateAsync(It.IsAny<Question>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.UpdateAsync(It.IsAny<Question>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.UpdateAsync(It.IsAny<Question>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.UpdateBulkAsync(It.IsAny<IList<Question>>()))
                .Verifiable();

            //Act
            var result = await questionController.UpdateBulkAsync(It.IsAny<IList<Question>>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "PUT")]
        public async Task GivenUpdateBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.UpdateBulkAsync(It.IsAny<IList<Question>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.UpdateBulkAsync(It.IsAny<IList<Question>>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Question())
                .Verifiable();
            mockQuestionService
                .Setup(_ => _.DeleteAsync(It.IsAny<Question>()))
                .Verifiable();

            //Act
            var result = await questionController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenNoDataExistsThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<Question>())
                .Verifiable();

            //Act
            var result = await questionController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.DeleteAsync(It.IsAny<Guid>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteBulkAsyncWhenDataExistsThenReturnsData()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.DeleteBulkAsync(It.IsAny<IList<Question>>()))
                .Verifiable();

            //Act
            var result = await questionController.DeleteBulkAsync(It.IsAny<IList<Question>>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        [Property("HttpVerb", "DELETE")]
        public async Task GivenDeleteBulkAsyncWhenExceptionThrowmThenHandleGracefully()
        {
            //Arrange
            mockQuestionService
                .Setup(_ => _.DeleteBulkAsync(It.IsAny<IList<Question>>()))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await questionController.DeleteBulkAsync(It.IsAny<IList<Question>>());

            //Assert
            mockQuestionService.VerifyAll();
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
