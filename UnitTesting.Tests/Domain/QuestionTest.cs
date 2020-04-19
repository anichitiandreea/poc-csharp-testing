using NUnit.Framework;
using System.Collections.Generic;
using UnitTesting.DataFaker;
using UnitTesting.Domain;

namespace UnitTesting.Tests.Domain
{
    [Property("NUnit", "Domain | Question")]
    public class QuestionTest
    {
        [Test]
        public void GivenQuestionEntityWhenGeneratedWithDataFakerThenVerifyAllProperties()
        {
            // Arrange
            IDataFaker dataFaker = new DataFaker.DataFaker();

            // Act
            var questions = dataFaker.FakeQuestion.Generate(count: 99);

            // Assert
            questions.ForEach(channel =>
            {
                Assert.That(channel.Id, Is.Not.Null);
                Assert.That(channel.Title, Is.Not.Null);
                Assert.That(channel.Title, Is.Not.Empty);
                Assert.That(channel.Title.Length, Is.InRange(1, 50));
                Assert.That(channel.Description, Is.Not.Null);
                Assert.That(channel.Description, Is.Not.Empty);
                Assert.That(channel.Description.Length, Is.InRange(1, 100));
                Assert.That(channel.UserId, Is.Not.Null);
                Assert.That(channel.Answers, Is.Not.Null);
                Assert.That(channel.Answers, Is.Not.Empty);
                Assert.That(channel.Answers, Is.InstanceOf<IEnumerable<Answer>>());
                Assert.That(channel.IsDeleted, Is.Not.Null);
            });
        }
    }
}
