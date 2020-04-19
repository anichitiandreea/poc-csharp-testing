using Bogus;
using UnitTesting.Domain;

namespace UnitTesting.DataFaker
{
    public interface IDataFaker
    {
        Faker<Question> FakeQuestion { get; }
        Faker<User> FakeUser { get; }
        Faker<Answer> FakeAnswer { get; }
    }
}
