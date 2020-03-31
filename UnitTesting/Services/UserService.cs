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
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;

        public UserService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await context.Users
                .Where(question => question.Id == id
                    && question.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User question)
        {
            context.Users.Add(question);
            await context.SaveChangesAsync();
        }

        public async Task CreateBulkAsync(IList<User> questions)
        {
            await context.Users.AddRangeAsync(questions);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User question)
        {
            context.Users.Update(question);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBulkAsync(IList<User> questions)
        {
            context.Users.UpdateRange(questions);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User question)
        {
            context.Users.Remove(question);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBulkAsync(IList<User> questions)
        {
            context.Users.RemoveRange(questions);
            await context.SaveChangesAsync();
        }
    }
}
