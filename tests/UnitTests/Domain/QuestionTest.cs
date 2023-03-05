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
            //Arrange
            IDataFaker dataFaker = new DataFaker.DataFaker();

            //Act
            var questions = dataFaker.FakeQuestion.Generate(count: 99);

            //Assert
            questions.ForEach(question =>
            {
                Assert.That(question.Id, Is.Not.Null);
                Assert.That(question.Title, Is.Not.Null);
                Assert.That(question.Title, Is.Not.Empty);
                Assert.That(question.Title.Length, Is.InRange(1, 50));
                Assert.That(question.Description, Is.Not.Null);
                Assert.That(question.Description, Is.Not.Empty);
                Assert.That(question.Description.Length, Is.InRange(1, 100));
                Assert.That(question.UserId, Is.Not.Null);
                Assert.That(question.Answers, Is.Not.Null);
                Assert.That(question.Answers, Is.Not.Empty);
                Assert.That(question.Answers, Is.InstanceOf<IEnumerable<Answer>>());
                Assert.That(question.IsDeleted, Is.Not.Null);
            });
        }
    }
}
