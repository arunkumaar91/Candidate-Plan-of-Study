using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieversCPS
{
    public class Student
    {
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public string StudentName { get; set; }
        public string ProgramName { get; set; }
        public string StudentDOB { get; set; }
        public string gender { get; set; }
        public string StudentEmail { get; set; }

        public string Address { get; set; }
        public string contactNumber { get; set; }
        public string FacultyAdvisor { get; set; }
        public string facultyEmail { get; set; }
        public string degreeType { get; set; }
        public string semester { get; set; }
        public DateTime StartYear { get; set; }
        

    }
}