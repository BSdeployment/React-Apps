using Dapper;
using LearningApp.Core.Models;
using LearningApp.Data.DB;
using LearningApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.Data.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly SQLiteConnectionFactory _connectionFactory;

        public ChapterRepository(SQLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddAsync(LearningChapter chapter)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
INSERT INTO LearningChapters
(SubjectId, Name, OrderIndex, TimesStudied, LearningRate, Note)
VALUES
(@SubjectId, @Name, @OrderIndex, @TimesStudied, @LearningRate, @Note);

SELECT last_insert_rowid();
";

                return await connection.ExecuteScalarAsync<int>(sql, chapter);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM LearningChapters WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task DeleteBySubjectIdAsync(int subjectId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM LearningChapters WHERE SubjectId = @SubjectId";
                await connection.ExecuteAsync(sql, new { SubjectId = subjectId });
            }
        }

        public async Task<LearningChapter> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "SELECT * FROM LearningChapters WHERE Id = @Id";

                return await connection.QuerySingleOrDefaultAsync<LearningChapter>(
                    sql,
                    new { Id = id });
            }
        }

        public async Task<IList<LearningChapter>> GetBySubjectIdAsync(int subjectId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
SELECT 
    Id,
    SubjectId,
    Name,
    OrderIndex,
    TimesStudied,
    LearningRate,
    Note
FROM LearningChapters
WHERE SubjectId = @SubjectId
ORDER BY OrderIndex;
";

                var result = await connection.QueryAsync<LearningChapter>(
                    sql,
                    new { SubjectId = subjectId });

                return result.AsList();
            }
        }

        public async Task UpdateAsync(LearningChapter chapter)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
UPDATE LearningChapters
SET
    Name = @Name,
    OrderIndex = @OrderIndex,
    TimesStudied = @TimesStudied,
    LearningRate = @LearningRate,
    Note = @Note
WHERE Id = @Id;
";

                await connection.ExecuteAsync(sql, chapter);
            }
        }
    }
}
