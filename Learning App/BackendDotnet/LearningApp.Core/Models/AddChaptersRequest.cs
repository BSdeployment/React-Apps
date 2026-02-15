using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApp.Core.Models
{
    public class AddChaptersRequest
    {
        public int SubjectId { get; set; }
        public string Mode { get; set; } // "single" | "range"
        public string From { get; set; }
        public string To { get; set; }
    }
}
