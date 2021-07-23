using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Modelss
{
    public partial class TermStudent
    {
        public string StudentId { get; set; }
        public int TermId { get; set; }

        public virtual StudentInfo Student { get; set; }
        public virtual Term Term { get; set; }
    }
}
