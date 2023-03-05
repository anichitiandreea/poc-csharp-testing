using Bogus;
using RestApi.Domain;

namespace RestApi.DataFaker
{
    public interface IDataFaker
    {
        Faker<Question> FakeQuestion { get; }
        Faker<User> FakeUser { get; }
        Faker<Answer> FakeAnswer { get; }
    }
}
