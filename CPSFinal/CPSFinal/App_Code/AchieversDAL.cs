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
//using Excel = Microsoft.Office.Interop.Excel;
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
        SqlConnection conn2 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\alira\Documents\Visual Studio 2013\Projects\CPSFinal\CPSFinal\App_Data\CourseCatalog.mdf';Integrated Security=True");
        OleDbConnection MyConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\alira\Documents\Visual Studio 2013\Projects\CPSFinal\CPSFinal\DefaultPDF's\UHCL_EM_ACTIVE_COURSE_CATALOG_7133.xlsx;Extended Properties='Excel 8.0;HDR=Yes'");
        public List<Users> GetAllUsers(string usernumber, string password)
        {
            try
            {
                conn1.Open();
                
                SqlCommand selectCommand = new SqlCommand("dbo.uspGetUser",conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvUserId", usernumber);
                selectCommand.Parameters.AddWithValue("@ipvPass", password);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.Userid= reader["userId"].ToString();
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

        
        public List<string> GetAllCourses(string deptName) 
        { 
            List<String> courses = new List<string>();
            try
            {
                
                OleDbCommand myCommand = new OleDbCommand();
                string sql = null;
                
                MyConnection.Open();
                myCommand.Connection = MyConnection;
                //sql = "SELECT Subject + Catalog + Long Title AS CourseName FROM [Sheet1$] where Subject="+"'"+deptName+"'";
                sql = "SELECT [Subject] +' '+ [Catalog] +' ' +[Long Title] AS [Course] FROM [Sheet1$] where Subject='"+deptName+"'";

                myCommand.CommandText = sql;
                OleDbDataReader reader = myCommand.ExecuteReader();
                //SqlDataAdapter da = new SqlDataAdapter();
                //DataSet ds=new DataSet();
                //da.Fill(ds, "[Sheet1$]");
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        courses.Add(reader["Course"].ToString());
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

        public List<CPSClass> GetAllMandatoryCoursesById(int studentId,string deptName)
        {
            List<CPSClass> mandatoryClasses = new List<CPSClass>();
            try
            {
                conn1.Open();
                conn2.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.GetAllMandatoryCourses", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvDeptName", deptName);
                selectCommand.Parameters.AddWithValue("@ipvStudentId", studentId);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CPSClass mandatedClass = new CPSClass();
                        mandatedClass.Grade=reader["grade"].ToString();
                        mandatedClass.courseRubric=reader["courseRubric"].ToString();
                        mandatedClass.courseNumber=reader["courseNumber"].ToString();
                        mandatedClass.className=reader["className"].ToString();
                        mandatedClass.units=(int)reader["credits"];
                        mandatedClass.Term=reader["Term"].ToString();
                        mandatoryClasses.Add(mandatedClass);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn1.State == ConnectionState.Open || conn2.State==ConnectionState.Open)
                {
                    conn1.Close();
                    conn2.Close();
                }
            }
            return mandatoryClasses;
        }

        
        internal bool ScheduleAppointment(int Id,string Name,string appointmentDate,string facultyAdvisor)
        {
            bool isScheduled = false;
            try{
                conn1.Open();
                SqlCommand insertCommand = new SqlCommand("uspAddAppointment", conn1);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.AddWithValue("@ipvStudentId", Id);
                insertCommand.Parameters.AddWithValue("@ipvStudentName", Name);
                insertCommand.Parameters.AddWithValue("@ipvApptDate",appointmentDate);
                insertCommand.Parameters.AddWithValue("@ipvAdvisor", facultyAdvisor);
                int isAdded=insertCommand.ExecuteNonQuery();

                if(isAdded==1)
                {
                    isScheduled = true;
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
            return isScheduled;
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

       

        internal List<CPSClass> GetAllMandatoryCourses(string p)
        {
            List<CPSClass> mandatoryClasses = new List<CPSClass>();
            try
            {
                conn1.Open();
                conn2.Open();
                SqlCommand selectCommand = new SqlCommand("dbo.GetAllMandatoryCoursesByDept", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvDeptName", p);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CPSClass mandatedClass = new CPSClass();
                        mandatedClass.Grade = reader["grade"].ToString();
                        mandatedClass.courseRubric = reader["courseRubric"].ToString();
                        mandatedClass.courseNumber = reader["courseNumber"].ToString();
                        mandatedClass.className = reader["className"].ToString();
                        mandatedClass.units = (int)reader["credits"];
                        mandatedClass.Term = reader["Term"].ToString();
                        mandatoryClasses.Add(mandatedClass);
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
                    conn2.Close();
                }
            }
            return mandatoryClasses;
        }

        internal List<StudentGrid1> GetAllStudentsBySemester(string p1,string sem, int p2)
        {
            List<StudentGrid1> studentList = new List<StudentGrid1>();
            try
            {
                conn1.Open();                
                SqlCommand selectCommand = new SqlCommand("dbo.GetAllStudentsBySemnDept", conn1);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@ipvDeptName", p1);
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
    }
}