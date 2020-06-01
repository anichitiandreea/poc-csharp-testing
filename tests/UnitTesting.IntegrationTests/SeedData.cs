using Microsoft.EntityFrameworkCore;
using System;
using UnitTesting.Data;
using UnitTesting.Domain;

namespace UnitTesting.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(DatabaseContext dbContext)
        {
            var user1 = new User
            {
                Id = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Name = "User1",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var question1 = new Question
            {
                Id = new Guid("28066e80-31c4-4fdd-b8db-236af203b76d"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "Wayne",
                Description = "Description1",
                IsDeleted = false,
                Answers = null
            };

            var question2 = new Question
            {
                Id = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                Title = "My second Question",
                Description = "Description2",
                IsDeleted = false,
                Answers = null
            };

            dbContext.Users.Add(user1);
            dbContext.Questions.Add(question1);
            dbContext.Questions.Add(question2);
            dbContext.SaveChanges();

            dbContext.Entry(question1).State = EntityState.Detached;
            dbContext.Entry(question2).State = EntityState.Detached;
            dbContext.Entry(user1).State = EntityState.Detached;

            dbContext.SaveChanges();
        }
    }
}