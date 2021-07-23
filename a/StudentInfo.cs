using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Modelss
{
    public partial class StudentInfo
    {
        public StudentInfo()
        {
            Courses = new HashSet<Course>();
            Grades = new HashSet<Grade>();
        }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Major { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
