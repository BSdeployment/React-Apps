using LearningApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.Application.Services
{
   public interface ILearningServices
    {
        Task<IList<LearningSubject>> GetAllSubjectsAsync();
        Task<LearningSubject> GetSubjectByIdAsync(int subjectId);
        Task<int> AddSubjectAsync(LearningSubject subject);
        Task<bool> UpdateSubjectAsync(LearningSubject subject);
        Task<bool> RemoveSubjectAsync(int subjectId);

        // Chapters
        Task<IList<LearningChapter>> GetChaptersBySubjectAsync(int subjectId);
        Task<int> AddChapterAsync(LearningChapter chapter);
        Task<bool> UpdateChapterAsync(LearningChapter chapter);
        Task<bool> RemoveChapterAsync(int chapterId);

        Task AddChaptersAsync(AddChaptersRequest req);

        // Learning actions
        Task IncrementChapterStudyCountAsync(int chapterId);
        Task DecrementChapterStudyCountAsync(int chapterId);
    }
}
