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
    public class AnswerService : IAnswerService
    {
        private readonly DatabaseContext context;

        public AnswerService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IList<Answer>> GetAllAsync()
        {
            return await context.Answers.ToListAsync();
        }

        public async Task<Answer> GetByIdAsync(Guid id)
        {
            return await context.Answers
                .Where(question => question.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Answer question)
        {
            context.Answers.Add(question);
            await context.SaveChangesAsync();
        }

        public async Task CreateBulkAsync(IList<Answer> questions)
        {
            await context.Answers.AddRangeAsync(questions);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Answer question)
        {
            context.Answers.Update(question);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBulkAsync(IList<Answer> questions)
        {
            context.Answers.UpdateRange(questions);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Answer question)
        {
            question.IsDeleted = true;
            context.Attach(question);
            context.Entry(question).Property("IsDeleted").IsModified = true;

            await context.SaveChangesAsync();
        }

        public async Task DeleteBulkAsync(IList<Answer> questions)
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
