using System;
using System.Collections.Generic;

#nullable disable

namespace projectPRN.Models
{
    public partial class Account
    {
        public string StudentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual StudentInfo Student { get; set; }
    }
}
