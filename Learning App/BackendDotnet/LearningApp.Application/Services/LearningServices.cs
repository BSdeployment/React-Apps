using LearningApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningApp.Data.Interfaces;
using System.Linq;
namespace LearningApp.Application.Services
{
    public class LearningServices : ILearningServices
    {


        private readonly ISubjectRepository _subjectRepository;
        private readonly IChapterRepository _chapterRepository;

        public LearningServices(ISubjectRepository subjectRepository,IChapterRepository chapterRepository)
        {
            this._chapterRepository = chapterRepository;
            this._subjectRepository = subjectRepository;
        }
        //subject function
        public async Task<int> AddSubjectAsync(LearningSubject subject)
        {
            subject.CreatedAt = DateTime.UtcNow;
            int result = await _subjectRepository.AddAsync(subject);

            return result;
        }


        public async Task<IList<LearningSubject>> GetAllSubjectsAsync()
        {
            var result = await _subjectRepository.GetAllAsync();

            return result;
        }


        public async Task<LearningSubject> GetSubjectByIdAsync(int subjectId)
        {
            var result = await _subjectRepository.GetByIdAsync(subjectId);

            return result;
        }


        public async Task<bool> RemoveSubjectAsync(int subjectId)
        {

            var subject = await _subjectRepository.GetByIdAsync(subjectId);

            if (subject == null)
            {
                return false;
            }

            await _subjectRepository.DeleteAsync(subjectId);

            return true;

        }

        public async Task<bool> UpdateSubjectAsync(LearningSubject subject)
        {
            var Upsubject = await _subjectRepository.GetByIdAsync(subject.Id);

            if (Upsubject == null)
                return false;
            await _subjectRepository.UpdateAsync(subject);

            return true;
        }




        // Chapters functions ----------------------------------------------------------->>>

        public async Task<int> AddChapterAsync(LearningChapter chapter)
        {
            var chapters = await _chapterRepository.GetBySubjectIdAsync(chapter.SubjectId);
            chapter.OrderIndex = chapters.Any()
                ? chapters.Max(c => c.OrderIndex) + 1
                : 1;

            chapter.TimesStudied = 0;

            return await _chapterRepository.AddAsync(chapter);
        }

       

        public async Task DecrementChapterStudyCountAsync(int chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
                return;

            if (chapter.TimesStudied > 0)
            {
                chapter.TimesStudied--;
                await _chapterRepository.UpdateAsync(chapter);
            }
        }

        

        public async Task<IList<LearningChapter>> GetChaptersBySubjectAsync(int subjectId)
        {
            var chapters = await _chapterRepository.GetBySubjectIdAsync(subjectId);

            return chapters
                .OrderBy(c => c.OrderIndex)
                .ToList();
        }

       

        public async Task IncrementChapterStudyCountAsync(int chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
                return;

            chapter.TimesStudied++;
            await _chapterRepository.UpdateAsync(chapter);
        }

        public async Task<bool> RemoveChapterAsync(int chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter == null)
                return false;

            await _chapterRepository.DeleteAsync(chapterId);
            return true;
        }



        public async Task<bool> UpdateChapterAsync(LearningChapter chapter)
        {
            var existing = await _chapterRepository.GetByIdAsync(chapter.Id);
            if (existing == null)
                return false;

            await _chapterRepository.UpdateAsync(chapter);
            return true;
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
            if (HebrewIndex.TryParseNumericRange(req.From, req.To, out var numbers))
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

       private LearningChapter Create(int subjectId, string name) =>
           new LearningChapter
           {
               SubjectId = subjectId,
               Name = name
           };

    }
}
