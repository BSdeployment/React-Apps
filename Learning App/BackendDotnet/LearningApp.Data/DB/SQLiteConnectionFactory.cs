using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


using Dapper;
using Microsoft.Data.Sqlite;


namespace LearningApp.Data.DB
{
    public class SQLiteConnectionFactory
    {

        private readonly string _connectionString;

        public SQLiteConnectionFactory(string connectionString)
        {

            this._connectionString = connectionString;
            
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);

            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA foreign_keys = ON;";
                command.ExecuteNonQuery();


               
                return connection;
            }
              
        }


    }
}
