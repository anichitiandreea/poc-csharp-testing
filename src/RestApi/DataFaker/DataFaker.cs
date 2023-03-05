using Bogus;
using RestApi.Domain;

namespace RestApi.DataFaker
{
    public sealed class DataFaker : IDataFaker
    {
        private const int COLLECTION_COUNT = 3;

        public Faker<Question> FakeQuestion =>
            new Faker<Question>(locale: "en")
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Title, f => f.Random.String(minLength: 1, maxLength: 50))
            .RuleFor(p => p.Description, f => f.Random.String(minLength: 1, maxLength: 100))
            .RuleFor(p => p.UserId, f => f.Random.Guid())
            .RuleFor(p => p.Answers, f => FakeAnswer.Generate(COLLECTION_COUNT))
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool(weight: 0.1F))
            .StrictMode(ensureRulesForAllProperties: true);

        public Faker<User> FakeUser =>
            new Faker<User>(locale: "en")
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Name, f => f.Random.String(minLength: 1, maxLength: 50))
            .RuleFor(p => p.Questions, f => FakeQuestion.Generate(COLLECTION_COUNT))
            .RuleFor(p => p.Answers, f => FakeAnswer.Generate(COLLECTION_COUNT))
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool(weight: 0.1F))
            .StrictMode(ensureRulesForAllProperties: true);

        public Faker<Answer> FakeAnswer =>
            new Faker<Answer>(locale: "en")
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Description, f => f.Random.String(minLength: 1, maxLength: 100))
            .RuleFor(p => p.UserId, f => f.Random.Guid())
            .RuleFor(p => p.QuestionId, f => f.Random.Guid())
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool(weight: 0.1F))
            .StrictMode(ensureRulesForAllProperties: true);
    }
}
