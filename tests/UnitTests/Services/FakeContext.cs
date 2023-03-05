using Microsoft.EntityFrameworkCore;
using System;
using RestApi.Data;
using RestApi.Domain;

namespace UnitTests.Services
{
    public class FakeContext
    {
        public DatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DatabaseContext(options);

            AddSampleQuestions(context);
            AddSampleUsers(context);
            AddSampleAnswers(context);

            context.SaveChanges();

            return context;
        }

        private void AddSampleQuestions(DatabaseContext context)
        {
            var question1 = new Question
            {
                Id = new Guid("3f0b1175-dbd1-44f3-a729-085c96c08dba"),
                UserId = new Guid("a0e929c2-5662-47fc-9a7d-872fe566506e"),
                Title = "Question1",
                Description = "Description1",
                IsDeleted = false,
                Answers = null
            };

            var question2 = new Question
            {
                Id = new Guid("a844dee3-8858-4c0c-9fbe-a6d21249ed13"),
                UserId = new Guid("bef9a4e9-ff7a-49c7-bda6-5f3154e8834c"),
                Title = "Question2",
                Description = "Description2",
                IsDeleted = false,
                Answers = null
            };

            var question3 = new Question
            {
                Id = new Guid("c03c7333-6636-4a3d-900d-14bd5dbcd13b"),
                UserId = new Guid("9f9e3aaf-1eca-46fe-8900-12cb56b0634d"),
                Title = "Question3",
                Description = "Description3",
                IsDeleted = false,
                Answers = null
            };

            context.Questions.Add(question1);
            context.Questions.Add(question2);
            context.Questions.Add(question3);

            context.SaveChanges();

            context.Entry(question1).State = EntityState.Detached;
            context.Entry(question2).State = EntityState.Detached;
            context.Entry(question3).State = EntityState.Detached;
        }

        private void AddSampleUsers(DatabaseContext context)
        {
            var user1 = new User
            {
                Id = new Guid("6424fb63-1912-4a5a-b890-1b7b8d5fffa8"),
                Name = "User1",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var user2 = new User
            {
                Id = new Guid("8d446640-d132-4b83-9ada-ed13f07a6ccc"),
                Name = "User2",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            var user3 = new User
            {
                Id = new Guid("1bf1603a-5aac-4502-b99f-5befdf046603"),
                Name = "User3",
                Questions = null,
                Answers = null,
                IsDeleted = false
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);

            context.SaveChanges();

            context.Entry(user1).State = EntityState.Detached;
            context.Entry(user2).State = EntityState.Detached;
            context.Entry(user3).State = EntityState.Detached;
        }

        private void AddSampleAnswers(DatabaseContext context)
        {
            var answer1 = new Answer
            {
                Id = new Guid("5b6cff6c-f7bf-4f6d-81af-17daeff03e48"),
                Description = "Description1",
                QuestionId = new Guid("8271de8a-c643-484f-b5d2-07ea23b09fa1"),
                UserId = new Guid("c98fb889-cef4-4023-aaba-55a501504b43"),
                IsDeleted = false
            };

            var answer2 = new Answer
            {
                Id = new Guid("8c634d6e-c0bc-4833-9f2e-16acf598753c"),
                Description = "Description2",
                QuestionId = new Guid("7444205b-e341-4665-9fbe-f097f6e41100"),
                UserId = new Guid("6055aa84-b91c-4bf6-b692-d409d31d0f39"),
                IsDeleted = false
            };

            var answer3 = new Answer
            {
                Id = new Guid("dba9fc6a-920f-44b9-b800-cb8c2c7bd4d8"),
                Description = "Description3",
                QuestionId = new Guid("a42aef11-858b-4b39-b0ad-7339388ada57"),
                UserId = new Guid("12d5682c-e781-4bef-9d05-cc8a1753d77f"),
                IsDeleted = false
            };

            context.Answers.Add(answer1);
            context.Answers.Add(answer2);
            context.Answers.Add(answer3);

            context.SaveChanges();

            context.Entry(answer1).State = EntityState.Detached;
            context.Entry(answer2).State = EntityState.Detached;
            context.Entry(answer3).State = EntityState.Detached;
        }
    }
}
