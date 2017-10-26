using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieversCPS
{
    public class EnrolledClasses
    {
        public int StudentId { get; set; }
        public string courseNumber { get; set; }
        public string courseName { get; set; }
        public int CreditsAwarded { get; set; }
        public string Grade { get; set; }
        public string Term { get; set; }
    }
}