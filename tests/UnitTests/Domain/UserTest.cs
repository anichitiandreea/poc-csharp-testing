using NUnit.Framework;
using System.Collections.Generic;
using RestApi.DataFaker;
using RestApi.Domain;

namespace UnitTests.Domain
{
    [Property("NUnit", "Domain | User")]
    public class UserTest
    {
        [Test]
        public void GivenUserEntityWhenGeneratedWithDataFakerThenVerifyAllProperties()
        {
            //Arrange
            IDataFaker dataFaker = new DataFaker();

            //Act
            var users = dataFaker.FakeUser.Generate(count: 99);

            //Assert
            users.ForEach(user =>
            {
                Assert.That(user.Id, Is.Not.Null);
                Assert.That(user.Name, Is.Not.Null);
                Assert.That(user.Name, Is.Not.Empty);
                Assert.That(user.Name.Length, Is.InRange(1, 50));
                Assert.That(user.Questions, Is.Not.Null);
                Assert.That(user.Questions, Is.Not.Empty);
                Assert.That(user.Questions, Is.InstanceOf<IEnumerable<Question>>());
                Assert.That(user.Answers, Is.Not.Null);
                Assert.That(user.Answers, Is.Not.Empty);
                Assert.That(user.Answers, Is.InstanceOf<IEnumerable<Answer>>());
                Assert.That(user.IsDeleted, Is.Not.Null);
            });
        }
    }
}
