using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace AchieversCPS
{
    /// <summary>
    /// Author: Team Achievers
    /// ProjectName: CPS Final 
    /// FileName: Business Layer with all business logic
    /// </summary>
    public class AchieversBL
    {

        List<Users> userList = new List<Users>();
        AchieversDAL dal = new AchieversDAL();

        /// <summary> 
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns>A list of Users</returns>
        public List<Users> GetAllUsers(string userid, string password)
        {

            userList = dal.GetAllUsers(userid, password);
            return userList;
        }

        /// <summary>
        /// This function checks for a particular student list given his studentId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> returns student List with one student in it</returns>
        public List<Student> getStudent(int userId)
        {
            List<Student> student = new List<Student>();
            student = dal.GetStudent(userId);
            return student;
        }

        /// <summary>
        /// Get core courses using studentid and deptname
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="dept"></param>
        /// <returns>returns list of courseclasses</returns>
        public List<CourseClass> getMandatoryClasses(int studentId, string dept)
        {
            List<CourseClass> mandatedClasses = new List<CourseClass>();
            mandatedClasses = dal.GetAllMandatoryCoursesByDept(dept);
            return mandatedClasses;
        }

        /// <summary>
        /// Get all the faculties in a school
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns>returns list of faculty id and name</returns>
        internal Dictionary<int, string> GetAllFaculties()
        {
            Dictionary<int, string> faculties = dal.GetAllFaculties();

            return faculties;
        }

        /// <summary>        
        /// </summary>
        /// <param name="std"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        internal bool AddStudent(Student std, Users user)
        {
            bool isAdded = dal.AddStudent(std, user);
            return isAdded;
        }

        /// <summary>
        /// Written to get all core courses by department Name
        /// </summary>
        /// <param name="p"></param>
        /// <returns> return a list of course classes</returns>
        internal List<CourseClass> GetAllMandatoryClasses(string p)
        {
            List<CourseClass> mandatedClasses = new List<CourseClass>();
            mandatedClasses = dal.GetAllMandatoryCoursesByDept(p);
            return mandatedClasses;
        }

        /// <summary>
        /// gets all students by semester and year they are enrolled in
        /// </summary>
        /// <param name="p1">semester </param>
        /// <param name="p2">year of admission</param>
        /// <returns> Returns a list of students that were admitted in the semester p1 and year p2</returns>
        internal List<StudentGrid1> GetAllStudentsBySemester(string p1, int p2)
        {
            List<StudentGrid1> studentList = dal.GetAllStudentsBySemester(p1, p2);
            return studentList;
        }

        /// <summary>
        /// This method generates initial CPS for students and stores it in Initial CPS table
        /// </summary>
        /// <param name="cpsList"></param>
        /// <returns>count of cps columns</returns>
        internal int GenerateInitialCPS(List<StudentCPS> cpsList)
        {
            int count = dal.GenerateInitialCPS(cpsList);
            return count;
        }

        /// <summary>
        /// This module gets user by id and returns validated user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns user by using userId</returns>
        public Users GetUserById(string userId)
        {
            Users user = new Users();
            List<Users> users = dal.GetUserById(userId);
            if (users.Count == 1)
            {
                user = users[0];
            }
            return user;
        }

        /// <summary>
        /// This module returns core courses
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="deptName"></param>
        /// <returns> retruns list of core classes</returns>
        internal List<CourseClass> GetCoreCourses(int stdId, string deptName)
        {
            List<CourseClass> coreCourses = dal.GetCoreCourses(stdId, deptName);
            return coreCourses;
        }

        /// <summary>
        /// this module returns a list of foundation courses taking studentId and deptName as input
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="deptName"></param>
        /// <returns>returns a list of foundation courses added for student</returns>
        internal List<CourseClass> GetFoundationCourses(int stdId, string deptName)
        {
            List<CourseClass> foundationCourses = dal.GetFoundationCourses(stdId, deptName);
            return foundationCourses;
        }

        /// <summary>
        /// this module returns a list of elective courses taking studentId and deptName as input
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="deptName"></param>
        /// <returns>returns a list of elective courses added for student</returns>
        internal List<CourseClass> GetElectiveCourses(int stdId, string deptName)
        {
            List<CourseClass> electiveCourses = dal.GetElectiveCourses(stdId, deptName);
            return electiveCourses;
        }

        /// <summary>
        /// this module accepts faculty Id as input and returns a list of students under the professor
        /// </summary>
        /// <param name="p"></param>
        /// <returns>returns a list of students under a particular professor</returns>
        internal List<StudentDetail> GetAllStudentsByFaculty(int p)
        {
            List<StudentDetail> studentList = new List<StudentDetail>();
            studentList = dal.GetAllStudentsByFaculty(p);
            return studentList;
        }

        internal bool AddNewCourse(AddCourse course)
        {
            bool isAdded = false;
            isAdded = dal.AddNewCourse(course);
            return isAdded;
        }

        internal CourseClass GetCourseByNumber(int number,string rubric)
        {
            CourseClass cclass = new CourseClass();
            cclass = dal.GetCourseByNumber(number,rubric);
            return cclass;
        }

        internal CourseClass GetCourseByName(string Name)
        {
            CourseClass cclass = new CourseClass();
            cclass = dal.GetCourseByName(Name);
            return cclass;
        }

        internal List<CourseClass> GetClassNumberByRubric(string Rubric)
        {
            List<CourseClass> cclass = new List<CourseClass>();
            cclass = dal.GetClassNumberByRubric(Rubric);
            return cclass;
        }
        
        internal bool AddclassestoDraftCPS(List<StudentCPS> cpsClasses)
        {
            bool count = false;
            count = dal.AddclassestoDraftCPS(cpsClasses);
            return count;
        }

        internal int AddInitialCPS(List<StudentCPS> cpsClasses)
        {
            int count = dal.AddInitialCPS(cpsClasses);
            return count;
        }
    }
}