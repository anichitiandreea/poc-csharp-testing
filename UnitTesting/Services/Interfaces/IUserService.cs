using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTesting.Domain;

namespace UnitTesting.Services.Interfaces
{
    public interface IUserService
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task CreateAsync(User question);
        Task CreateBulkAsync(IList<User> questions);
        Task UpdateAsync(User question);
        Task UpdateBulkAsync(IList<User> questions);
        Task DeleteAsync(User question);
        Task DeleteBulkAsync(IList<User> questions);
    }
}
