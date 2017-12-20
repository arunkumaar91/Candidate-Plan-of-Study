using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AchieversCPS;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data.OleDb;

namespace AchieversCPS
{

    /// <summary>
    /// Author: Team Achievers
    /// ProjectName: CPS Final 
    /// FileName: Data Access Layer with all database logic
    /// </summary>
    public class AchieversDAL
    {
        /// <summary>
        /// this method is used for encryption of the password
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns> returns encrypted password</returns>
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// this method is used for decryption of the password
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns> returns decrypted password</returns>
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        List<Users> allUsers = new List<Users>();
        SqlConnection conn1 = new SqlConnection(@"Data Source=dcm.uhcl.edu;Initial Catalog=c432016sp01hemania;User ID=hemania;Password=1456068");
        SqlConnection conn2 = new SqlConnection(@"Data Source=dcm.uhcl.edu;Initial Catalog=capf17gswen2;Persist Security Info=True;User ID=capf17gswen2;Password=3827039");

        SqlConnection MyConnection = new SqlConnection(@"Data Source=dcm.uhcl.edu;Initial Catalog=capf17gswen22;Persist Security Info=True;User ID=capf17gswen22;Password=6679687");

        /// <summary> 
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns>A list of Users</returns>
        public List<Users> GetAllUsers(string usernumber, string password)
        {
            try
            {
                conn1.Open();

                SqlCommand selectCommand = new SqlCommand("dbo.uspGetUser", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                string encryptedPassword = Encrypt(password);
                selectCommand.Parameters.AddWithValue("@ipvUserId", usernumber);
                selectCommand.Parameters.AddWithValue("@ipvPass", encryptedPassword);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.Userid = reader["userId"].ToString();
                        user.FullName = reader["Full Name"].ToString();
                        user.Password = reader["pass"].ToString();
                        user.Role = reader["roleOfperson"].ToString();
                        allUsers.Add(user);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return allUsers;
        }

        /// <summary>
        /// This function checks for a particular student list given his studentId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> returns student List with one student in it</returns>
        public List<Student> GetStudent(int studentId)
        {
            List<Student> studentList = new List<Student>();
            try
            {
                conn1.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.uspGetStudent", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvUserId", studentId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.StudentId = (int)reader["studentId"];
                        student.StudentName = reader["studentFirstName"].ToString() + reader["studentLastName"].ToString();
                        student.ProgramName = reader["programName"].ToString();
                        student.degreeType = reader["degreetype"].ToString();
                        student.semester = reader["semester"].ToString();
                        student.StartYear = int.Parse(reader["startYear"].ToString());
                        student.StudentEmail = reader["uhclEmail"].ToString();
                        student.UserName = reader["userName"].ToString();
                        student.FacultyAdvisorId = int.Parse(reader["facultyAdvisorId"].ToString());
                        studentList.Add(student);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return studentList;
        }


        public List<CourseClass> GetAllCourses(string deptName)
        {
            List<CourseClass> courses = new List<CourseClass>();
            try
            {
                MyConnection.Open();

                string sql = null;

                sql = "SELECT rubric as [Subject],coursenumber as [Catalog],coursename as [Long Title],units as [Min Units] FROM courseCatalog where " + deptName + "='FOUN'";
                SqlCommand myCommand = new SqlCommand(sql, MyConnection);
                //myCommand.CommandText = sql;
                SqlDataReader reader = myCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.courseRubric = reader["Subject"].ToString();
                        cClass.courseNumber = int.Parse(reader["Catalog"].ToString());
                        cClass.className = reader["Long Title"].ToString();
                        cClass.units = int.Parse(reader["Min Units"].ToString());
                        courses.Add(cClass);

                    }
                }
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return courses;
        }

        /// <summary>
        /// gets all departments in a school course catalog
        /// </summary>
        /// <returns> List of Department is returned</returns>
        public List<String> GetAllDept()
        {
            List<String> depts = new List<string>();

            try
            {

                SqlCommand myCommand = new SqlCommand();
                string sql = null;

                MyConnection.Open();
                myCommand.Connection = MyConnection;
                sql = "SELECT DISTINCT(Subject) FROM courseCatalog ";
                myCommand.CommandText = sql;
                SqlDataReader reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        depts.Add(reader["Subject"].ToString());
                    }
                }
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return depts;
        }

        /// <summary>
        /// Get core courses using studentid and deptname
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="dept"></param>
        /// <returns>returns list of courseclasses</returns>
        public List<CourseClass> GetAllMandatoryCoursesByDept(string deptName)
        {
            List<CourseClass> mandatoryClasses = new List<CourseClass>();

            try
            {

                SqlCommand myCommand = new SqlCommand();
                string sql = null;

                MyConnection.Open();
                myCommand.Connection = MyConnection;
                //sql = "SELECT Subject + Catalog + Long Title AS CourseName FROM [Sheet1$] where Subject="+"'"+deptName+"'";
                sql = "SELECT rubric as [Subject],coursenumber as [Catalog],coursename as [Long Title],units as [Min Units] FROM courseCatalog where " + deptName + "='CORE'";

                myCommand.CommandText = sql;
                SqlDataReader reader = myCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.courseRubric = reader["Subject"].ToString();
                        cClass.courseNumber = int.Parse(reader["Catalog"].ToString());
                        cClass.className = reader["Long Title"].ToString();
                        cClass.units = int.Parse(reader["Min Units"].ToString());
                        mandatoryClasses.Add(cClass);

                    }
                }
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return mandatoryClasses;
        }

        /// <summary>
        /// Gets a list of all elective courses given deptName as input
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns>returns a list of all electives of a department</returns>
        public List<CourseClass> GetAllElectiveCoursesByDept(string deptName)
        {
            List<CourseClass> electiveClasses = new List<CourseClass>();

            try
            {

                SqlCommand myCommand = new SqlCommand();
                string sql = null;

                MyConnection.Open();
                myCommand.Connection = MyConnection;
                //sql = "SELECT Subject + Catalog + Long Title AS CourseName FROM [Sheet1$] where Subject="+"'"+deptName+"'";
                sql = "SELECT rubric as [Subject],coursenumber as [Catalog],coursename as [Long Title],units as [Min Units] FROM courseCatalog where " + deptName + "='ELEC'";

                myCommand.CommandText = sql;
                SqlDataReader reader = myCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.courseRubric = reader["Subject"].ToString();
                        cClass.courseNumber = int.Parse(reader["Catalog"].ToString());
                        cClass.className = reader["Long Title"].ToString();
                        cClass.units = int.Parse(reader["Min Units"].ToString());
                        electiveClasses.Add(cClass);

                    }
                }
                MyConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return electiveClasses;
        }

        /// <summary>
        /// Get all the faculties in a school
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns>returns list of faculty id and name</returns>
        internal Dictionary<int, string> GetAllFaculties()
        {
            Dictionary<int, string> faculties = new Dictionary<int, string>();
            try
            {
                conn1.Open();
                SqlCommand selectCommand = new SqlCommand("uspGetAllFaculties", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        faculties.Add(int.Parse(reader["facultyId"].ToString()), reader["Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return faculties;
        }

        /// <summary>        
        /// </summary>
        /// <param name="std"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        internal bool AddStudent(Student std, Users user)
        {
            bool isAdded = false;
            try
            {
                conn1.Open();
                SqlCommand insertCommand = new SqlCommand("uspAddStudent", conn1);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.AddWithValue("@ipvUserId", user.Userid);
                insertCommand.Parameters.AddWithValue("@ipvLoginUserName", user.UserName);
                insertCommand.Parameters.AddWithValue("@ipvPass", Encrypt(user.Password));
                insertCommand.Parameters.AddWithValue("@ipvRoleOfperson", user.Role);
                insertCommand.Parameters.AddWithValue("@ipvFullName", user.FullName);
                insertCommand.Parameters.AddWithValue("@ipvStudentId", std.StudentId);
                var names = std.StudentName.Split(' ');
                string firstName = names[0];
                string lastName = names[1];
                insertCommand.Parameters.AddWithValue("@ipvStudentFirstName", firstName);
                insertCommand.Parameters.AddWithValue("@ipvStudentLastName", lastName);
                insertCommand.Parameters.AddWithValue("@ipvProgramName", std.ProgramName);
                insertCommand.Parameters.AddWithValue("@ipvUhclEmail", std.StudentEmail);
                insertCommand.Parameters.AddWithValue("@ipvFacultyAdvisorId", std.FacultyAdvisorId);
                insertCommand.Parameters.AddWithValue("@ipvDegreeetype", std.degreeType);
                insertCommand.Parameters.AddWithValue("@ipvSemester", std.semester);
                insertCommand.Parameters.AddWithValue("@ipvStartYear", DateTime.Now);
                insertCommand.Parameters.AddWithValue("@ipvUserName", std.UserName);
                int count = insertCommand.ExecuteNonQuery();
                if (count == 1)
                {
                    isAdded = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return isAdded;
        }

        /// <summary>
        /// gets all students by semester and year they are enrolled in
        /// </summary>
        /// <param name="p1">semester </param>
        /// <param name="p2">year of admission</param>
        /// <returns> Returns a list of students that were admitted in the semester p1 and year p2</returns>
        internal List<StudentGrid1> GetAllStudentsBySemester(string sem, int p2)
        {
            List<StudentGrid1> studentList = new List<StudentGrid1>();
            try
            {
                conn1.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.GetAllStudentsBySemnDept", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvSemester", sem);
                selectCommand.Parameters.AddWithValue("@ipvYear", p2);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StudentGrid1 student = new StudentGrid1();
                        student.studentId = int.Parse(reader["studentId"].ToString());
                        student.studentName = reader["Student Name"].ToString();
                        student.deptName = reader["programName"].ToString();
                        student.degreeType = reader["degreetype"].ToString();
                        studentList.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open || conn2.State == ConnectionState.Open)
                {
                    conn1.Close();

                }
            }
            return studentList;
        }

        /// <summary>
        /// This method generates initial CPS for students and stores it in Initial CPS table
        /// </summary>
        /// <param name="cpsList"></param>
        /// <returns>count of cps columns</returns>
        internal int GenerateInitialCPS(List<StudentCPS> cpsList)
        {
            int count = 0;
            try
            {
                conn2.Open();
                foreach (StudentCPS cps in cpsList)
                {
                    SqlCommand insertCommand = new SqlCommand("uspAddCPS", conn2);
                    insertCommand.CommandType = CommandType.StoredProcedure;
                    insertCommand.Parameters.AddWithValue("@ipvStudentId", cps.StudentId);
                    insertCommand.Parameters.AddWithValue("@ipvStudentName", cps.StudentName);
                    insertCommand.Parameters.AddWithValue("@ipvFacId", cps.facultyAdvisorId);
                    insertCommand.Parameters.AddWithValue("@ipvCourseRubric", cps.CourseRubric);
                    insertCommand.Parameters.AddWithValue("@ipvCourseNumber", cps.CourseNumber);
                    insertCommand.Parameters.AddWithValue("@ipvCourseName", cps.CourseName);
                    insertCommand.Parameters.AddWithValue("@ipvUnits", cps.Units);
                    insertCommand.Parameters.AddWithValue("@ipvGrades", cps.Grades);
                    insertCommand.Parameters.AddWithValue("@ipvSemester", cps.Semester);
                    insertCommand.Parameters.AddWithValue("@ipvYear", cps.Year);
                    insertCommand.Parameters.AddWithValue("@ipvCourseType", cps.CourseType);
                    int cnt = insertCommand.ExecuteNonQuery();
                    if (cnt == 1)
                    {
                        count++;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                }
            }
            return count;
        }

        /// <summary>
        /// This module gets user by id and returns validated user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns user by using userId</returns>
        public List<Users> GetUserById(string userId)
        {
            List<Users> users = new List<Users>();
            try
            {
                conn1.Open();

                SqlCommand selectCommand = new SqlCommand("dbo.uspGetUserById", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvUserId", int.Parse(userId));
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.UserName = reader["userName"].ToString();
                        user.Password = Decrypt(reader["pass"].ToString());
                        user.Role = reader["roleOfperson"].ToString();
                        users.Add(user);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return users;
        }

        /// <summary>
        /// this module returns a list of elective courses taking studentId and deptName as input
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="deptName"></param>
        /// <returns>returns a list of elective courses added for student</returns>
        internal List<CourseClass> GetElectiveCourses(int stdId, string deptName)
        {
            List<CourseClass> electiveCourses = new List<CourseClass>();
            List<CourseClass> electiveCourses1 = new List<CourseClass>();

            try
            {

                conn2.Open();
                MyConnection.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.uspGetElectiveCourses", conn2);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvStudentId", stdId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                //DataTable table1 = new DataTable();
                //table1.Load(reader);

                SqlCommand selectCommand2 = new SqlCommand("uspGetAllElectives", MyConnection);
                selectCommand2.CommandType = CommandType.StoredProcedure;
                selectCommand2.Parameters.AddWithValue("@ipvDeptName", deptName);
                SqlDataReader reader2 = selectCommand2.ExecuteReader();
                //DataTable table2 = new DataTable();
                List<string> ccourseName = new List<string>();
                //table2.Load(reader2);
                //table1.Merge(table2);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.Semester = reader["Semester"].ToString();
                        cClass.courseNumber = int.Parse(reader["CourseNumber"].ToString());
                        cClass.courseRubric = reader["CourseRubric"].ToString();
                        cClass.className = reader["CourseName"].ToString();
                        cClass.Grades = reader["Grades"].ToString();
                        cClass.units = int.Parse(reader["Units"].ToString());
                        electiveCourses.Add(cClass);
                        ccourseName.Add(cClass.courseRubric + cClass.courseNumber);
                    }
                }

                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        bool isMatched = false;
                        CourseClass cClass = new CourseClass();
                        cClass.Semester = reader2["Semester"].ToString();
                        cClass.courseNumber = int.Parse(reader2["CourseNumber"].ToString());
                        cClass.courseRubric = reader2["CourseRubric"].ToString();
                        cClass.className = reader2["CourseName"].ToString();
                        cClass.Grades = reader2["Grades"].ToString();
                        cClass.units = int.Parse(reader2["Units"].ToString());
                        foreach (string str in ccourseName)
                        {
                            if (str == (cClass.courseRubric + cClass.courseNumber))
                            {
                                isMatched = true;
                                break;
                            }
                        }
                        if (!isMatched)
                        {
                            electiveCourses.Add(cClass);
                        }

                    }
                }
                //foreach (DataRow dr in table1.Rows)
                //{
                //    CourseClass cClass = new CourseClass();
                //    cClass.Semester = dr["Semester"].ToString();
                //    cClass.courseNumber = int.Parse(dr["CourseNumber"].ToString());
                //    cClass.courseRubric = dr["CourseRubric"].ToString();
                //    cClass.className = dr["CourseName"].ToString();
                //    cClass.Grades = dr["Grades"].ToString();
                //    cClass.units = int.Parse(dr["Units"].ToString());
                //    electiveCourses.Add(cClass);
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                }
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return electiveCourses;
        }

        /// <summary>
        /// this module returns a list of foundation courses taking studentId and deptName as input
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="deptName"></param>
        /// <returns>returns a list of foundation courses added for student</returns>
        internal List<CourseClass> GetFoundationCourses(int stdId, string deptName)
        {
            List<CourseClass> foundationCourses = new List<CourseClass>();
            try
            {

                conn2.Open();
                MyConnection.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.uspGetFoundationCourses", conn2);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvStudentId", stdId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                DataTable table1 = new DataTable();
                table1.Load(reader);



                foreach (DataRow dr in table1.Rows)
                {
                    CourseClass cClass = new CourseClass();
                    cClass.Semester = dr["Semester"].ToString();
                    cClass.courseNumber = int.Parse(dr["CourseNumber"].ToString());
                    cClass.courseRubric = dr["CourseRubric"].ToString();
                    cClass.className = dr["CourseName"].ToString();
                    cClass.Grades = dr["Grades"].ToString();
                    cClass.units = int.Parse(dr["Units"].ToString());
                    foundationCourses.Add(cClass);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();

                }
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return foundationCourses;
        }

        /// <summary>
        /// This module returns core courses
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="deptName"></param>
        /// <returns> retruns list of core classes</returns>
        public List<CourseClass> GetCoreCourses(int stdId, string deptName)
        {
            List<CourseClass> coreCourses = new List<CourseClass>();
            try
            {

                conn2.Open();
                MyConnection.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.uspGetCoreCourses", conn2);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvStudentId", stdId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                DataTable table1 = new DataTable();
                table1.Load(reader);
                foreach (DataRow dr in table1.Rows)
                {
                    CourseClass cClass = new CourseClass();
                    cClass.Semester = dr["Semester"].ToString();
                    cClass.courseNumber = int.Parse(dr["CourseNumber"].ToString());
                    cClass.courseRubric = dr["CourseRubric"].ToString();
                    cClass.className = dr["CourseName"].ToString();
                    cClass.Grades = dr["Grades"].ToString();
                    cClass.units = int.Parse(dr["Units"].ToString());
                    coreCourses.Add(cClass);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                }
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return coreCourses;
        }

        /// <summary>
        /// this module accepts faculty Id as input and returns a list of students under the professor
        /// </summary>
        /// <param name="p"></param>
        /// <returns>returns a list of students under a particular professor</returns>
        internal List<StudentDetail> GetAllStudentsByFaculty(int p)
        {
            List<StudentDetail> studentList = new List<StudentDetail>();
            try
            {
                conn1.Open();
                SqlCommand selectCommand = new SqlCommand("uspGetAllStudentsByFacultyId", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvFacultyId", p);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StudentDetail student = new StudentDetail();
                        student.StudentName = reader["StudentName"].ToString();
                        student.ProgramName = reader["programName"].ToString();
                        student.StudentId = int.Parse(reader["studentId"].ToString());
                        studentList.Add(student);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return studentList;
        }

        internal bool AddNewCourse(AddCourse course)
        {
            bool isAdded = false;
            MyConnection.Open();
            try
            {
                SqlCommand insertCommand = new SqlCommand("uspAddNewCourse",MyConnection);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.AddWithValue("@ipvRubric", course.courseRubric);
                insertCommand.Parameters.AddWithValue("@ipvCoursenumber", course.courseNumber);
                insertCommand.Parameters.AddWithValue("@ipvCoursename", course.className);
                insertCommand.Parameters.AddWithValue("@ipvUnits", course.units);
                insertCommand.Parameters.AddWithValue("@ipvCourseType", course.CourseType);
                insertCommand.Parameters.AddWithValue("@ipvDeptName", course.DeptName);
                insertCommand.Parameters.AddWithValue("@ipvYear", course.Year);
                
                int count= insertCommand.ExecuteNonQuery();
                if(count>0)
                {
                    isAdded = true;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return isAdded;
        }

        internal CourseClass GetCourseByName(string Name)
        {
            CourseClass cclass = new CourseClass();
            try
            {
                MyConnection.Open();
                SqlCommand selectCommand = new SqlCommand("uspGetCourseByName",MyConnection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvCourseName", Name);
                SqlDataReader reader= selectCommand.ExecuteReader();
                List<CourseClass> courseList = new List<CourseClass>();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass ccclass=new CourseClass();
                        ccclass.className = reader["coursename"].ToString();
                        ccclass.courseNumber = int.Parse( reader["coursenumber"].ToString());
                        ccclass.courseRubric = reader["rubric"].ToString();
                        ccclass.Grades = "Na";
                        ccclass.Semester = "Na";
                        ccclass.units = int.Parse(reader["units"].ToString());
                        ccclass.Year = 1975;
                        courseList.Add(ccclass);
                    }
                    if(courseList.Count==1)
                    {
                        cclass = courseList[0];
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(MyConnection.State==ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }

            return cclass;
        }

        internal CourseClass GetCourseByNumber(int number,string rubric)
        {
            CourseClass cclass = new CourseClass();
            try
            {
                MyConnection.Open();
                SqlCommand selectCommand = new SqlCommand("uspGetCourseByNumber", MyConnection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvCourseNumber", number);
                selectCommand.Parameters.AddWithValue("@ipvRubric", rubric);
                SqlDataReader reader = selectCommand.ExecuteReader();
                List<CourseClass> courseList = new List<CourseClass>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass ccclass = new CourseClass();
                        ccclass.className = reader["coursename"].ToString();
                        ccclass.courseNumber = int.Parse(reader["coursenumber"].ToString());
                        ccclass.courseRubric = reader["rubric"].ToString();
                        ccclass.Grades = "Na";
                        ccclass.Semester = "Na";
                        ccclass.units = int.Parse(reader["units"].ToString());
                        ccclass.Year = 1975;
                        courseList.Add(ccclass);
                    }
                    if (courseList.Count == 1)
                    {
                        cclass = courseList[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }

            return cclass;           
        }

        internal List<CourseClass> GetClassNumberByRubric(string Rubric)
        {
            List<CourseClass> cclassList = new List<CourseClass>();
            try
            {
                MyConnection.Open();
                SqlCommand selectCommand = new SqlCommand("uspGetCourseByRubric", MyConnection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                
                selectCommand.Parameters.AddWithValue("@ipvRubric", Rubric);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass ccclass = new CourseClass();
                        ccclass.className = reader["coursename"].ToString();
                        ccclass.courseNumber = int.Parse(reader["coursenumber"].ToString());
                        ccclass.courseRubric = reader["rubric"].ToString();
                        ccclass.Grades = "Na";
                        ccclass.Semester = "Na";
                        ccclass.units = int.Parse(reader["units"].ToString());
                        ccclass.Year = 1975;
                        cclassList.Add(ccclass);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyConnection.State == ConnectionState.Open)
                {
                    MyConnection.Close();
                }
            }
            return cclassList;           
        }

        

        internal bool AddclassestoDraftCPS(List<StudentCPS> cpsClasses)
        {
            bool isAdded = false;
            try
            {
                conn2.Open();
                foreach (StudentCPS cps in cpsClasses)
                {
                    SqlCommand insertCommand = new SqlCommand("uspAddDraftCPS", conn2);
                    insertCommand.CommandType = CommandType.StoredProcedure;
                    insertCommand.Parameters.AddWithValue("@ipvStudentId", cps.StudentId);
                    insertCommand.Parameters.AddWithValue("@ipvStudentName", cps.StudentName);
                    insertCommand.Parameters.AddWithValue("@ipvFacId", cps.facultyAdvisorId);
                    insertCommand.Parameters.AddWithValue("@ipvCourseRubric", cps.CourseRubric);
                    insertCommand.Parameters.AddWithValue("@ipvCourseNumber", cps.CourseNumber);
                    insertCommand.Parameters.AddWithValue("@ipvCourseName", cps.CourseName);
                    insertCommand.Parameters.AddWithValue("@ipvUnits", cps.Units);
                    insertCommand.Parameters.AddWithValue("@ipvGrades", cps.Grades);
                    insertCommand.Parameters.AddWithValue("@ipvSemester", cps.Semester);
                    insertCommand.Parameters.AddWithValue("@ipvYear", cps.Year);
                    insertCommand.Parameters.AddWithValue("@ipvCourseType", cps.CourseType);
                    int cnt = insertCommand.ExecuteNonQuery();
                    if (cnt == 1)
                    {
                        isAdded = true;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                }
            }
            return isAdded;
        }

        internal int AddInitialCPS(List<StudentCPS> cpsClasses)
        {
            int count = 0;
            try
            {
                conn2.Open();
                foreach (StudentCPS cps in cpsClasses)
                {
                    SqlCommand insertCommand = new SqlCommand("uspAddCPS", conn2);
                    insertCommand.CommandType = CommandType.StoredProcedure;
                    insertCommand.Parameters.AddWithValue("@ipvStudentId", cps.StudentId);
                    insertCommand.Parameters.AddWithValue("@ipvStudentName", cps.StudentName);
                    insertCommand.Parameters.AddWithValue("@ipvFacId", cps.facultyAdvisorId);
                    insertCommand.Parameters.AddWithValue("@ipvCourseRubric", cps.CourseRubric);
                    insertCommand.Parameters.AddWithValue("@ipvCourseNumber", cps.CourseNumber);
                    insertCommand.Parameters.AddWithValue("@ipvCourseName", cps.CourseName);
                    insertCommand.Parameters.AddWithValue("@ipvUnits", cps.Units);
                    insertCommand.Parameters.AddWithValue("@ipvGrades", cps.Grades);
                    insertCommand.Parameters.AddWithValue("@ipvSemester", cps.Semester);
                    insertCommand.Parameters.AddWithValue("@ipvYear", cps.Year);
                    insertCommand.Parameters.AddWithValue("@ipvCourseType", cps.CourseType);
                    int cnt = insertCommand.ExecuteNonQuery();
                    if (cnt == 1)
                    {
                        count++;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                }
            }
            return count;
        }
    }
}