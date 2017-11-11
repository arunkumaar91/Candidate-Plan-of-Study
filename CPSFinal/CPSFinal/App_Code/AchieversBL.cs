﻿using System;
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
        public List<CourseClass> getMandatoryClasses(int studentId,string dept)
        {
            List<CourseClass> mandatedClasses = new List<CourseClass>();
            mandatedClasses = dal.GetAllMandatoryCoursesByDept(dept);
            return mandatedClasses;
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

        internal List<CourseClass> GetAllMandatoryClasses(string p)
        {
            List<CourseClass> mandatedClasses = new List<CourseClass>();
            mandatedClasses = dal.GetAllMandatoryCoursesByDept(p);
            return mandatedClasses;
        }

        internal List<StudentGrid1> GetAllStudentsBySemester(string p1, int p2)
        {
            List<StudentGrid1> studentList = dal.GetAllStudentsBySemester( p1, p2);
            return studentList;
        }

        internal int GenerateInitialCPS(List<StudentCPS> cpsList)
        {
            int count=dal.GenerateInitialCPS(cpsList);
            return count;
        }

        public Users GetUserById(string userId)
        {
            Users user = new Users();
            List<Users> users = dal.GetUserById(userId);
            if(users.Count==1)
            {
                user= users[0];
            }
            return user;
        }

        internal List<CourseClass> GetCoreCourses(int stdId)
        {
            List<CourseClass> coreCourses = dal.GetCoreCourses(stdId);
            return coreCourses;
        }

        internal List<CourseClass> GetFoundationCourses(int stdId)
        {
            List<CourseClass> foundationCourses = dal.GetFoundationCourses(stdId);
            return foundationCourses;
        }

        internal List<CourseClass> GetElectiveCourses(int stdId)
        {
            List<CourseClass> electiveCourses = dal.GetElectiveCourses(stdId);
            return electiveCourses;
        }
    }
}