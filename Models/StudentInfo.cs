using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Models
{
    public partial class StudentInfo
    {
        public StudentInfo()
        {
            Courses = new HashSet<Course>();
        }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Major { get; set; }
        public int? CurrentTerm { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
