using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTesting.Domain;

namespace UnitTesting.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<IList<Answer>> GetAllAsync();
        Task<Answer> GetByIdAsync(Guid id);
        Task CreateAsync(Answer question);
        Task CreateBulkAsync(IList<Answer> questions);
        Task UpdateAsync(Answer question);
        Task UpdateBulkAsync(IList<Answer> questions);
        Task DeleteAsync(Answer question);
        Task DeleteBulkAsync(IList<Answer> questions);
    }
}
