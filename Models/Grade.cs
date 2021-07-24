using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public string GradeType { get; set; }
        public string GradeCategory { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public decimal Percentage { get; set; }
        public decimal Value { get; set; }
        public string Comment { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentInfo Student { get; set; }
    }
}
