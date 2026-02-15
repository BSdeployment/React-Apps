using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningApp.Core.Models;
namespace LearningApp.Data.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IList<LearningSubject>> GetAllAsync();
        Task<LearningSubject> GetByIdAsync(int id);
        Task<int> AddAsync(LearningSubject subject);
        Task UpdateAsync(LearningSubject subject);
        Task DeleteAsync(int id);
    }
}
