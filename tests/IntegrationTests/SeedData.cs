using Microsoft.EntityFrameworkCore;
using System;
using RestApi.Data;
using RestApi.Domain;

namespace IntegrationTests
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
            var user2 = new User
            {
                Id = new Guid("74256f8c-fcab-448b-9add-1158ed9056fd"),
                Name = "User2",
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

            var answer1 = new Answer
            {
                Id = new Guid("e5e1d944-3296-4532-adfa-00ec05eb2cfa"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description1",
                IsDeleted = false
            };

            var answer2 = new Answer
            {
                Id = new Guid("852b5984-0dc4-459e-a3a8-0a81b0e1a2a5"),
                UserId = new Guid("9876deb5-27ed-41c1-8998-86c998275acf"),
                QuestionId = new Guid("c75f9e5e-c01b-4acd-b504-d412c14756f4"),
                Description = "Description2",
                IsDeleted = false
            };

            dbContext.Users.Add(user1);
            dbContext.Users.Add(user2);
            dbContext.Questions.Add(question1);
            dbContext.Questions.Add(question2);
            dbContext.Answers.Add(answer1);
            dbContext.Answers.Add(answer2);

            dbContext.SaveChanges();

            dbContext.Entry(question1).State = EntityState.Detached;
            dbContext.Entry(question2).State = EntityState.Detached;
            dbContext.Entry(answer1).State = EntityState.Detached;
            dbContext.Entry(answer2).State = EntityState.Detached;
            dbContext.Entry(user1).State = EntityState.Detached;
            dbContext.Entry(user2).State = EntityState.Detached;

            dbContext.SaveChanges();
        }
    }
}
