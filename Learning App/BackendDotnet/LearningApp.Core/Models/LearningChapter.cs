using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace LearningApp.Core.Models
{
    public class LearningChapter
    {

        [JsonPropertyName("id")]
        public int Id { get; set; } // SQLite PK
        [JsonPropertyName("subjectId")]
        public int SubjectId { get; set; } // FK ל-LearningSubject
        [JsonPropertyName("name")]
        public string Name { get; set; } // שם הפרק

        [JsonPropertyName("timesStudied")]
        public int TimesStudied { get; set; } = 0; // מספר החזרות

        [JsonPropertyName("learningRate")]
        public int LearningRate { get; set; } = 0;

        [JsonPropertyName("note")]
        public string Note {  get; set; }

        [JsonPropertyName("orderIndex")]
        public int OrderIndex { get; set; }

      
    }
}
