using LearningApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LearningApp.Application.Services
{
    public class DemoLearningService:ILearningServices
    {

       

        private readonly List<LearningSubject> _subjects = new List<LearningSubject>()
        {
            new LearningSubject(){ Id =1, 
            CreatedAt = DateTime.Now, Name = "Shabat"},
            new LearningSubject(){ Id =2, 
            CreatedAt = DateTime.Now, Name = "Brachot"},
            
        };

        private readonly List<LearningChapter> _chapters = new List<LearningChapter>();

        private int _subjectId = 1;
        private int _chapterId = 1;

        // ---------------- Subjects ----------------

        public DemoLearningService()
        {
            _subjectId = _subjects.Any()
       ? _subjects.Max(s => s.Id) + 1
       : 1;

            _chapterId = _chapters.Any()
                ? _chapters.Max(c => c.Id) + 1
                : 1;
        }

        public Task<int> AddSubjectAsync(LearningSubject subject)
        {
            subject.Id = _subjectId++;
            subject.CreatedAt = DateTime.UtcNow;

            _subjects.Add(subject);
            return Task.FromResult(subject.Id);
        }

        public Task<IList<LearningSubject>> GetAllSubjectsAsync()
        {
            return Task.FromResult<IList<LearningSubject>>(
                _subjects.OrderBy(s => s.Id).ToList()
            );
        }

        public Task<LearningSubject> GetSubjectByIdAsync(int subjectId)
        {
            var subject = _subjects.FirstOrDefault(s => s.Id == subjectId);
            return Task.FromResult(subject);
        }

        public Task<bool> RemoveSubjectAsync(int subjectId)
        {
            var subject = _subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null)
                return Task.FromResult(false);

            _subjects.Remove(subject);

            // מחיקת פרקים של הנושא (כמו ON DELETE CASCADE)
            _chapters.RemoveAll(c => c.SubjectId == subjectId);

            return Task.FromResult(true);
        }

        public Task<bool> UpdateSubjectAsync(LearningSubject subject)
        {
            var existing = _subjects.FirstOrDefault(s => s.Id == subject.Id);
            if (existing == null)
                return Task.FromResult(false);

            existing.Name = subject.Name;
            return Task.FromResult(true);
        }

        // ---------------- Chapters ----------------

        public Task<int> AddChapterAsync(LearningChapter chapter)
        {
            var chaptersBySubject = _chapters
                .Where(c => c.SubjectId == chapter.SubjectId)
                .ToList();

            chapter.Id = _chapterId++;
            chapter.OrderIndex = chaptersBySubject.Any()
                ? chaptersBySubject.Max(c => c.OrderIndex) + 1
                : 1;

            chapter.TimesStudied = 0;

            _chapters.Add(chapter);
            return Task.FromResult(chapter.Id);
        }


        public async Task AddChaptersAsync(AddChaptersRequest req)
        {
            if (req.Mode == "single")
            {
                await AddChapterAsync(new LearningChapter
                {
                    SubjectId = req.SubjectId,
                    Name = req.From
                });
                return;
            }

            // --- range ---
            if ( HebrewIndex.TryParseNumericRange(req.From, req.To, out var numbers))
            {
                foreach (var n in numbers)
                    await AddChapterAsync(Create(req.SubjectId, n.ToString()));
                return;
            }

            if (HebrewIndex.TryParseHebrewRange(req.From, req.To, out var hebrew))
            {
                foreach (var h in hebrew)
                    await AddChapterAsync(Create(req.SubjectId, h));
                return;
            }

            throw new Exception("Unsupported range format");
        }

        /// <summary>
        /// helper
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private LearningChapter Create(int subjectId, string name) =>
    new LearningChapter
    {
        SubjectId = subjectId,
        Name = name
    };




        public Task<IList<LearningChapter>> GetChaptersBySubjectAsync(int subjectId)
        {
            var result = _chapters
                .Where(c => c.SubjectId == subjectId)
                .OrderBy(c => c.OrderIndex)
                .ToList();

            return Task.FromResult<IList<LearningChapter>>(result);
        }

        public Task IncrementChapterStudyCountAsync(int chapterId)
        {
            var chapter = _chapters.FirstOrDefault(c => c.Id == chapterId);
            if (chapter != null)
                chapter.TimesStudied++;

            return Task.CompletedTask;
        }

        public Task DecrementChapterStudyCountAsync(int chapterId)
        {
            var chapter = _chapters.FirstOrDefault(c => c.Id == chapterId);
            if (chapter != null && chapter.TimesStudied > 0)
                chapter.TimesStudied--;

            return Task.CompletedTask;
        }

        public Task<bool> UpdateChapterAsync(LearningChapter chapter)
        {
            var existing = _chapters.FirstOrDefault(c => c.Id == chapter.Id);
            if (existing == null)
                return Task.FromResult(false);

            existing.Name = chapter.Name;
            existing.Note = chapter.Note;
            existing.LearningRate = chapter.LearningRate;

            return Task.FromResult(true);
        }

        public Task<bool> RemoveChapterAsync(int chapterId)
        {
            var chapter = _chapters.FirstOrDefault(c => c.Id == chapterId);
            if (chapter == null)
                return Task.FromResult(false);

            _chapters.Remove(chapter);
            return Task.FromResult(true);
        }
    }
}
