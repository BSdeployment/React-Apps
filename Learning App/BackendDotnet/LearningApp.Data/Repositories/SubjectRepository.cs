using Dapper;
using LearningApp.Core.Models;
using LearningApp.Data.DB;
using LearningApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SQLiteConnectionFactory _connectionFactory;

        public SubjectRepository(SQLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddAsync(LearningSubject subject)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
INSERT INTO LearningSubjects (Name, CreatedAt)
VALUES (@Name, @CreatedAt);
SELECT last_insert_rowid();
";

                return await connection.ExecuteScalarAsync<int>(sql, subject);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM LearningSubjects WHERE Id = @Id";

                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IList<LearningSubject>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "SELECT Id, Name, CreatedAt FROM LearningSubjects ORDER BY CreatedAt DESC";

                var result = await connection.QueryAsync<LearningSubject>(sql);

                return result.ToList();
            }
        }

        public async Task<LearningSubject> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "SELECT Id, Name, CreatedAt FROM LearningSubjects WHERE Id = @Id";

                return await connection.QuerySingleOrDefaultAsync<LearningSubject>(
                    sql,
                    new { Id = id });
            }
        }

        public async Task UpdateAsync(LearningSubject subject)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"
UPDATE LearningSubjects
SET Name = @Name
WHERE Id = @Id;
";

                await connection.ExecuteAsync(sql, subject);
            }
        }
    }
}
