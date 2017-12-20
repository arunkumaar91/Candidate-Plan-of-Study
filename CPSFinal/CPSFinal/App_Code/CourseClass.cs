using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AchieversCPS
{
    /// <summary>
    /// Created this class for Course objects
    /// </summary>
    public class CourseClass
    {
        public int courseNumber { get; set; }
        public string courseRubric { get; set; }
        public string className { get; set; }
        public int units { get; set; }
        public string Grades { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
    }
}