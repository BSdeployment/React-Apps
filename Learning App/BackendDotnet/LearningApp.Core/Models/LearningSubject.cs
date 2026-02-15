using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LearningApp.Core.Models
{
    public class LearningSubject
    {

        [JsonPropertyName("id")]
            public int Id { get; set; } // SQLite PK
        [JsonPropertyName("name")]
            public string Name { get; set; }
        [JsonPropertyName("date")]
            public DateTime CreatedAt { get; set; } = DateTime.Now;
        
    }
}
