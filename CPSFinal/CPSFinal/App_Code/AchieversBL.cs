using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace AchieversCPS
{
    public class AchieversBL
    {
       
        List<Users> userList = new List<Users>();
        AchieversDAL dal = new AchieversDAL();
        public List<Users> GetAllUsers(string userid,string password)
        {
            
            userList = dal.GetAllUsers(userid,password);
            return userList;
        }

        public List<Student> getStudent(int userId)
        {
            List<Student> student=new List<Student>();
            student = dal.GetStudent(userId);
            return student;
        }
        public List<CPSClass> getMandatoryClasses(int studentId,string dept)
        {
            List<CPSClass> mandatedClasses = new List<CPSClass>();
            mandatedClasses = dal.GetAllMandatoryCoursesById(studentId, dept);
            return mandatedClasses;
        }

        internal bool ScheduleAppoinment(int Id,string studentName,string appointmentDate,string advisorId)
        {
            bool isScheduled = false;
            isScheduled = dal.ScheduleAppointment(Id,studentName,appointmentDate,advisorId);
            return isScheduled;
        }

        internal Dictionary<int, string> GetAllFaculties()
        {
            Dictionary<int, string> faculties = dal.GetAllFaculties();
            
            return faculties;
        }

        internal bool AddStudent(Student std,Users user)
        {
            bool isAdded = dal.AddStudent(std,user);
            return isAdded;
        }

        internal List<CPSClass> GetAllMandatoryClasses(string p)
        {
            List<CPSClass> mandatedClasses = new List<CPSClass>();
            mandatedClasses = dal.GetAllMandatoryCourses(p);
            return mandatedClasses;
        }

        internal List<StudentGrid1> GetAllStudentsBySemester(string deptName,string p1, int p2)
        {
            List<StudentGrid1> studentList = dal.GetAllStudentsBySemester(deptName, p1, p2);
            return studentList;
        }
    }
}