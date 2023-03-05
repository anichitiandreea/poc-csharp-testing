using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTesting.Data;
using UnitTesting.Domain;
using UnitTesting.Services.Interfaces;

namespace UnitTesting.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly DatabaseContext context;

        public QuestionService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IList<Question>> GetAllAsync()
        {
            return await context.Questions.ToListAsync();
        }

        public async Task<Question> GetByIdAsync(Guid id)
        {
            return await context.Questions
                .AsNoTracking()
                .Where(question =>
                    question.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Question question)
        {
            await context.Questions.AddAsync(question);

            await context.SaveChangesAsync();
        }

        public async Task CreateBulkAsync(IList<Question> questions)
        {
            await context.Questions.AddRangeAsync(questions);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question question)
        {
            context.Questions.Update(question);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBulkAsync(IList<Question> questions)
        {
            context.Questions.UpdateRange(questions);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Question question)
        {
            question.IsDeleted = true;
            context.Attach(question);
            context.Entry(question).Property("IsDeleted").IsModified = true;

            await context.SaveChangesAsync();
        }

        public async Task DeleteBulkAsync(IList<Question> questions)
        {
            foreach (var question in questions)
            {
                question.IsDeleted = true;
                context.Attach(question);
                context.Entry(question).Property("IsDeleted").IsModified = true;
            }

            await context.SaveChangesAsync();
        }
    }
}
