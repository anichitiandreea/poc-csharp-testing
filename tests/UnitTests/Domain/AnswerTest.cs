using NUnit.Framework;
using System;
using RestApi.DataFaker;

namespace UnitTests.Domain
{
    [Property("NUnit", "Domain | Answer")]
    public class AnswerTest
    {
        [Test]
        public void GivenAnswerEntityWhenGeneratedWithDataFakerThenVerifyAllProperties()
        {
            //Arrange
            IDataFaker dataFaker = new DataFaker();

            //Act
            var answers = dataFaker.FakeAnswer.Generate(count: 99);

            //Assert
            answers.ForEach(answer =>
            {
                Assert.That(answer.Id, Is.Not.Null);
                Assert.That(answer.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(answer.Description, Is.Not.Null);
                Assert.That(answer.Description, Is.Not.Empty);
                Assert.That(answer.Description.Length, Is.InRange(1, 100));
                Assert.That(answer.UserId, Is.Not.Null);
                Assert.That(answer.UserId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(answer.QuestionId, Is.Not.Null);
                Assert.That(answer.QuestionId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(answer.IsDeleted, Is.Not.Null);
            });
        }
    }
}
