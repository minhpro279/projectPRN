using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Modelss
{
    public partial class Term
    {
        public Term()
        {
            Courses = new HashSet<Course>();
        }

        public int TermId { get; set; }
        public string TermName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
