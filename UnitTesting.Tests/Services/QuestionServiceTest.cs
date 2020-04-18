using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    }
}
