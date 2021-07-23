using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace projectPRN.Modelss
{
    public partial class Course
    {
        public Course()
        {
            Grades = new HashSet<Grade>();
        }

        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        public int TermId { get; set; }

        public virtual StudentInfo Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
