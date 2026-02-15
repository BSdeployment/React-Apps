using LearningApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.Data.Interfaces
{
    public interface IChapterRepository
    {
        Task<IList<LearningChapter>> GetBySubjectIdAsync(int subjectId);
        Task<LearningChapter> GetByIdAsync(int id);

        Task<int> AddAsync(LearningChapter chapter);
        Task UpdateAsync(LearningChapter chapter);
        Task DeleteAsync(int id);
        Task DeleteBySubjectIdAsync(int subjectId);
    }
}
