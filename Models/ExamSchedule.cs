using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Models
{
    public partial class ExamSchedule
    {
        public int CourseId { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string ExamRoom { get; set; }
        public string ExamType { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual Course Course { get; set; }
    }
}
