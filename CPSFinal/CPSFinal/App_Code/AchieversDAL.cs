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


    public class AchieversDAL
    {
        
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

        OleDbConnection MyConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + @"DefaultPDF's\UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+ConfigurationManager.AppSettings["Year"] + ".xlsx; Extended Properties='Excel 8.0;HDR=Yes'");
        
        public List<Users> GetAllUsers(string usernumber, string password)
        {
            try
            {
                conn1.Open();
                
                SqlCommand selectCommand = new SqlCommand("dbo.uspGetUser",conn1);
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
                        user.Userid= reader["userId"].ToString();
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
        
        public List<Student> GetStudent(int studentId)
        {
            List<Student> studentList =new List<Student>();
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
                        student.StudentName = reader["studentFirstName"].ToString()+reader["studentLastName"].ToString();
                        student.ProgramName=reader["programName"].ToString();
                        student.degreeType = reader["degreetype"].ToString();
                        student.semester=reader["semester"].ToString();
                        student.StartYear = int.Parse(reader["startYear"].ToString());
                        student.StudentEmail=reader["uhclEmail"].ToString();
                        student.UserName = reader["userName"].ToString();
                        student.FacultyAdvisorId =int.Parse( reader["facultyAdvisorId"].ToString());
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
                
                sql = "SELECT [Subject],[Catalog],[Long Title],[Min Units] FROM [sheet1$] where "+deptName+"='FOUN'";
                OleDbCommand myCommand = new OleDbCommand(sql,MyConnection);
                //myCommand.CommandText = sql;
                OleDbDataReader reader = myCommand.ExecuteReader();
                
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.courseRubric=reader["Subject"].ToString();
                        cClass.courseNumber =int.Parse(reader["Catalog"].ToString());
                        cClass.className = reader["Long Title"].ToString();
                        cClass.units = int.Parse(reader["Min Units"].ToString());
                        courses.Add(cClass);

                    }
                }
                MyConnection.Close();
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
            return courses; 
        }


        public List<String> GetAllDept()
        {
            List<String> depts = new List<string>();
            
            try
            {
                
                OleDbCommand myCommand = new OleDbCommand();
                string sql = null;
                
                MyConnection.Open();
                myCommand.Connection = MyConnection;
                sql = "SELECT DISTINCT(Subject) FROM [Sheet1$] ";
                myCommand.CommandText = sql;
                OleDbDataReader reader = myCommand.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        depts.Add(reader["Subject"].ToString());
                    }
                }
                MyConnection.Close();
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
            return depts;
        }

        public List<CourseClass> GetAllMandatoryCoursesByDept(string deptName)
        {
            List<CourseClass> mandatoryClasses = new List<CourseClass>();
            
            try
            {

                OleDbCommand myCommand = new OleDbCommand();
                string sql = null;

                MyConnection.Open();
                myCommand.Connection = MyConnection;
                //sql = "SELECT Subject + Catalog + Long Title AS CourseName FROM [Sheet1$] where Subject="+"'"+deptName+"'";
                sql = "SELECT [Subject],[Catalog],[Long Title],[Min Units] FROM [Sheet1$] where " + deptName + "='CORE'";

                myCommand.CommandText = sql;
                OleDbDataReader reader = myCommand.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.courseRubric = reader["Subject"].ToString();
                        cClass.courseNumber =int.Parse( reader["Catalog"].ToString());
                        cClass.className = reader["Long Title"].ToString();
                        cClass.units =int.Parse( reader["Min Units"].ToString());
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

        public List<CourseClass> GetAllElectiveCoursesByDept(string deptName)
        {
            List<CourseClass> electiveClasses = new List<CourseClass>();

            try
            {

                OleDbCommand myCommand = new OleDbCommand();
                string sql = null;

                MyConnection.Open();
                myCommand.Connection = MyConnection;
                //sql = "SELECT Subject + Catalog + Long Title AS CourseName FROM [Sheet1$] where Subject="+"'"+deptName+"'";
                sql = "SELECT [Subject],[Catalog],[Long Title],[Min Units] FROM [Sheet1$] where " + deptName + "='ELEC'";

                myCommand.CommandText = sql;
                OleDbDataReader reader = myCommand.ExecuteReader();

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

        
        

        internal Dictionary<int, string> GetAllFaculties()
        {
            Dictionary<int, string> faculties = new Dictionary<int, string>();
            try
            {
                conn1.Open();
                SqlCommand selectCommand = new SqlCommand("uspGetAllFaculties", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = selectCommand.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        faculties.Add(int.Parse(reader["facultyId"].ToString()), reader["Name"].ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(conn1.State==ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return faculties;
        }

        

        internal bool AddStudent(Student std,Users user)
        {
            bool isAdded = false;
            try
            {
                conn1.Open();
                SqlCommand insertCommand = new SqlCommand("uspAddStudent", conn1);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.AddWithValue("@ipvUserId",user.Userid);
                insertCommand.Parameters.AddWithValue("@ipvLoginUserName",user.UserName);
                insertCommand.Parameters.AddWithValue("@ipvPass",Encrypt(user.Password));
                insertCommand.Parameters.AddWithValue("@ipvRoleOfperson",user.Role);
                insertCommand.Parameters.AddWithValue("@ipvFullName",user.FullName);
                insertCommand.Parameters.AddWithValue("@ipvStudentId", std.StudentId);
                var names=std.StudentName.Split(' ');
                string firstName=names[0];
                string lastName=names[1];
                insertCommand.Parameters.AddWithValue("@ipvStudentFirstName", firstName);
                insertCommand.Parameters.AddWithValue("@ipvStudentLastName", lastName);
                insertCommand.Parameters.AddWithValue("@ipvProgramName", std.ProgramName);
                insertCommand.Parameters.AddWithValue("@ipvUhclEmail", std.StudentEmail);
                insertCommand.Parameters.AddWithValue("@ipvFacultyAdvisorId", std.FacultyAdvisorId);
                insertCommand.Parameters.AddWithValue("@ipvDegreeetype", std.degreeType);
                insertCommand.Parameters.AddWithValue("@ipvSemester", std.semester);
                insertCommand.Parameters.AddWithValue("@ipvStartYear",DateTime.Now);
                insertCommand.Parameters.AddWithValue("@ipvUserName", std.UserName);
                int count = insertCommand.ExecuteNonQuery();
                if(count==1)
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
                if(conn1.State==ConnectionState.Open)
                {
                    conn1.Close();
                }
            }
            return isAdded;
        }

              

        internal List<StudentGrid1> GetAllStudentsBySemester(string sem, int p2)
        {
            List<StudentGrid1> studentList = new List<StudentGrid1>();
            try
            {
                conn1.Open();                
                SqlCommand selectCommand = new SqlCommand("dbo.GetAllStudentsBySemnDept", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvSemester",sem);
                selectCommand.Parameters.AddWithValue("@ipvYear", p2);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         StudentGrid1 student = new StudentGrid1();
                        student.studentId =int.Parse( reader["studentId"].ToString());
                        student.studentName = reader["Student Name"].ToString();
                        student.deptName=reader["programName"].ToString();
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

        internal int GenerateInitialCPS(List<StudentCPS> cpsList)
        {
            int count = 0;
            try
            {
                conn2.Open();
                foreach(StudentCPS cps in cpsList)
                {
                    SqlCommand insertCommand = new SqlCommand("uspAddCPS", conn2);
                    insertCommand.CommandType = CommandType.StoredProcedure;
                    insertCommand.Parameters.AddWithValue("@ipvStudentId", cps.StudentId);
                    insertCommand.Parameters.AddWithValue("@ipvStudentName", cps.StudentName);
                    insertCommand.Parameters.AddWithValue("@ipvFacId", cps.facultyAdvisorId);
                    insertCommand.Parameters.AddWithValue("@ipvCourseRubric",cps.CourseRubric );
                    insertCommand.Parameters.AddWithValue("@ipvCourseNumber", cps.CourseNumber);
                    insertCommand.Parameters.AddWithValue("@ipvCourseName", cps.CourseName);
                    insertCommand.Parameters.AddWithValue("@ipvUnits",cps.Units);
                    insertCommand.Parameters.AddWithValue("@ipvGrades", cps.Grades);
                    insertCommand.Parameters.AddWithValue("@ipvSemester", cps.Semester);
                    insertCommand.Parameters.AddWithValue("@ipvYear", cps.Year);
                    insertCommand.Parameters.AddWithValue("@ipvCourseType", cps.CourseType);
                    int cnt = insertCommand.ExecuteNonQuery();
                    if(cnt==1)
                    {
                        count++;
                    }
                    
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(conn2.State==ConnectionState.Open)
                {
                    conn2.Close();
                }
            }           
            return count;
        }

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

        internal List<CourseClass> GetElectiveCourses(int stdId)
        {
            List<CourseClass> electiveCourses = new List<CourseClass>();
            try
            {
                conn2.Open();

                SqlCommand selectCommand = new SqlCommand("dbo.uspGetElectiveCourses", conn2);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvStudentId", stdId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass cClass = new CourseClass();
                        cClass.Semester=reader["Semester"].ToString();
                        cClass.courseNumber=int.Parse( reader["CourseNumber"].ToString());
                        cClass.courseRubric=reader["CourseRubric"].ToString();
                        cClass.className=reader["CourseName"].ToString();
                        cClass.Grades = reader["Grades"].ToString();
                        cClass.units = int.Parse( reader["Units"].ToString());
                        electiveCourses.Add(cClass);
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
            return electiveCourses;
        }

        internal List<CourseClass> GetFoundationCourses(int stdId)
        {
            List<CourseClass> foundationCourses = new List<CourseClass>();
            try
            {
                conn2.Open();

                SqlCommand selectCommand = new SqlCommand("dbo.uspGetFoundationCourses", conn2);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvStudentId", stdId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CourseClass fClass = new CourseClass();
                        fClass.Semester = reader["Semester"].ToString();
                        fClass.courseNumber = int.Parse(reader["CourseNumber"].ToString());
                        fClass.courseRubric = reader["CourseRubric"].ToString();
                        fClass.className = reader["CourseName"].ToString();
                        fClass.Grades = reader["Grades"].ToString();
                        fClass.units = int.Parse(reader["Units"].ToString());
                        foundationCourses.Add(fClass);
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
            return foundationCourses;
        }

        internal List<CourseClass> GetCoreCourses(int stdId)
        {
            List<CourseClass> coreCourses = new List<CourseClass>();
            try
            {
                conn2.Open();

                SqlCommand selectCommand = new SqlCommand("dbo.uspGetCoreCourses", conn2);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvStudentId", stdId);
                SqlDataReader reader = selectCommand.ExecuteReader();
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
                        coreCourses.Add(cClass);
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
            return coreCourses;
        }
    }
}