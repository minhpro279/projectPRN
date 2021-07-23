using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace projectPRN.Modelss
{
    public partial class ExamSchedule
    {
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string ExamRoom { get; set; }
        public string ExamType { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentInfo Student { get; set; }
    }
}
