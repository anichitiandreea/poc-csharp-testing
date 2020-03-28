using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTesting.Data;
using UnitTesting.Domains;
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
                .Where(question => question.Id == id
                    && question.IsDeleted == false)
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
            context.Answers.Remove(question);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBulkAsync(IList<Answer> questions)
        {
            context.Answers.RemoveRange(questions);
            await context.SaveChangesAsync();
        }
    }
}
