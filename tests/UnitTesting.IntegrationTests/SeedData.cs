using System;
using UnitTesting.Data;
using UnitTesting.Domain;

namespace UnitTesting.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(DatabaseContext dbContext)
        {
            dbContext.Questions.Add(new Question() { Id = Guid.NewGuid(), Title = "Wayne"});
            dbContext.SaveChanges();
        }
    }
}