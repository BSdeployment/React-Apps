using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.Data.DB
{
    public class DatabaseInitializer
    {

        private readonly SQLiteConnectionFactory _connectionFactory;

        public DatabaseInitializer(SQLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(CreateSubjectsTableSql);
                await connection.ExecuteAsync(CreateChaptersTableSql);

            }

        }

        private const string CreateSubjectsTableSql = @"
            CREATE TABLE IF NOT EXISTS LearningSubjects (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                CreatedAt TEXT NOT NULL
            );
        ";


        private const string CreateChaptersTableSql = @"
            CREATE TABLE IF NOT EXISTS LearningChapters (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SubjectId INTEGER NOT NULL,
    Name TEXT NOT NULL,
    OrderIndex INTEGER NOT NULL,
    TimesStudied INTEGER NOT NULL DEFAULT 0,
    LearningRate INTEGER NOT NULL DEFAULT 0,
    Note TEXT,
    FOREIGN KEY (SubjectId)
        REFERENCES LearningSubjects(Id)
        ON DELETE CASCADE,
    UNIQUE (SubjectId, OrderIndex)
);

        ";
    }
}
