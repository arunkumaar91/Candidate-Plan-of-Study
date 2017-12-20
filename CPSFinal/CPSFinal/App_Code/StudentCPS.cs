using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieversCPS
{
    /// <summary>
    /// created to add student CPS from academic modification page and used to insert CPS data
    /// used in Data Access Layer and Business layer
    /// </summary>
    public class StudentCPS
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int facultyAdvisorId { get; set; }
        public string CourseRubric { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public int Units { get; set; }
        public string Grades { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public string CourseType { get; set; }
    }
}