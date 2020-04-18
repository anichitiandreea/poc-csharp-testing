using Microsoft.EntityFrameworkCore;
using System;
using UnitTesting.Data;
using UnitTesting.Domain;

namespace UnitTesting.Tests.Services
{
    public class FakeContext
    {
        public DatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new DatabaseContext(options);

            context.Questions.Add(new Question
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Question1",
                Description = "Description1",
                IsDeleted = false,
                Answers = null
            });

            context.Questions.Add(new Question
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Question2",
                Description = "Description2",
                IsDeleted = false,
                Answers = null
            });

            context.Questions.Add(new Question
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = "Question3",
                Description = "Description3",
                IsDeleted = false,
                Answers = null
            });

            context.SaveChanges();

            return context;
        }
    }
}
