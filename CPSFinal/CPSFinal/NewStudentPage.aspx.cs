using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    public partial class NewStudentPage : System.Web.UI.Page
    {
        AchieversBL business = new AchieversBL();
        List<Student> stu = new List<Student>();
        int studentId = 0;


        private bool CheckIfFileExistsOnServer(string fileName)
        {
            var request = (HttpWebRequest)WebRequest.Create(fileName);
            //request.Method = WebRequestMethods.Ftp.GetFileSize;
            bool exists = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                exists = true;
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.NotFound)
                    exists = false;
            }
            return exists;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AchieversDAL dal = new AchieversDAL();
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["sid"] != null)
                    {
                        Users user = new Users();
                        user = (Users)(Session["user"]);
                        sgnName.Text = user.FullName;
                        studentId= int.Parse(Request.QueryString["sid"].ToString());
                        stu= business.getStudent(studentId);
                        fvStudents.DataSource = stu;
                        fvStudents.DataBind();
                        string deptName = stu[0].ProgramName;
                        int Year = stu[0].StartYear;

                        //string filePath = "http://dcm.uhcl.edu/capf17gswen2/DefaultPDF's/UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+Year+".xlsx";
                        //bool exists = false;
                        //HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(filePath);
                        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        //exists = CheckIfFileExistsOnServer(filePath);
                        
                        if(File.Exists(AppDomain.CurrentDomain.BaseDirectory+@"DefaultPDF's\UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+Year + ".xlsx"))
                        {
                            ConfigurationManager.AppSettings["Year"] = Year.ToString();
                        }
                        else
                        {
                            ConfigurationManager.AppSettings["Year"] = DateTime.Now.Year.ToString();
                        }
                        //ConfigurationManager.AppSettings["excelPath"] = Server.MapPath("http://dcm.uhcl.edu/capf17gswen2/DefaultPDF's/UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+ConfigurationManager.AppSettings["Year"]+".xlsx");
                        
                        GridView1.DataSource = dal.GetAllCourses(deptName);
                        GridView1.DataBind();
                        GridView2.DataSource = dal.GetAllMandatoryCoursesByDept(deptName);
                        GridView2.DataBind();
                        GridView3.DataSource = dal.GetAllElectiveCoursesByDept(deptName);

                        
                        
                        GridView3.DataBind();
                                        

                    }
                    else
                    {
                        Response.Redirect("Error.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script> alert('" + ex.Message + "')</script>");
            }
        }

        protected void generateForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AcadAdvisor.aspx");
        }

        protected void auditCPS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AcadAdvisor.aspx");
        }

        protected void modifyCPS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AcadAdvisor.aspx");
        }

        protected void addCatalog_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AcadAdvisor.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<StudentCPS> cpsList = new List<StudentCPS>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (chkRow.Checked)
                    {
                        StudentCPS cps = new StudentCPS();
                        cps.CourseRubric = row.Cells[2].Text;
                        cps.CourseNumber = int.Parse( row.Cells[1].Text);
                        cps.CourseName = row.Cells[3].Text;
                        studentId = int.Parse(Request.QueryString["sid"].ToString());
                        stu = business.getStudent(studentId);
                        cps.facultyAdvisorId = stu[0].FacultyAdvisorId;
                        cps.Grades = "NA";
                        cps.Semester = "NA";
                        cps.StudentId = stu[0].StudentId;
                        cps.StudentName = stu[0].StudentName;
                        cps.Units = int.Parse( row.Cells[4].Text);
                        cps.Year = int.Parse(stu[0].StartYear.ToString());
                        cps.CourseType = "FOUN";
                        cpsList.Add(cps);
                        
                    }
                }
            }

            
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox2") as CheckBox);
                    if (chkRow.Checked)
                    {
                        StudentCPS cps = new StudentCPS();
                        cps.CourseRubric = row.Cells[2].Text;
                        cps.CourseNumber = int.Parse(row.Cells[1].Text);
                        cps.CourseName = row.Cells[3].Text;
                        studentId = int.Parse(Request.QueryString["sid"].ToString());
                        stu = business.getStudent(studentId);
                        cps.facultyAdvisorId = stu[0].FacultyAdvisorId;
                        cps.Grades = "NA";
                        cps.Semester = "NA";
                        cps.StudentId = stu[0].StudentId;
                        cps.StudentName = stu[0].StudentName;
                        cps.Units = int.Parse(row.Cells[4].Text);
                        cps.Year = int.Parse(stu[0].StartYear.ToString());
                        cps.CourseType = "CORE";
                        cpsList.Add(cps);

                    }
                }
            }

            
            foreach (GridViewRow row in GridView3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("CheckBox3") as CheckBox);
                    if (chkRow.Checked)
                    {
                        StudentCPS cps = new StudentCPS();
                        cps.CourseRubric = row.Cells[2].Text;
                        cps.CourseNumber = int.Parse(row.Cells[1].Text);
                        cps.CourseName = row.Cells[3].Text;
                        studentId = int.Parse(Request.QueryString["sid"].ToString());
                        stu = business.getStudent(studentId);
                        cps.facultyAdvisorId = stu[0].FacultyAdvisorId;
                        cps.Grades = "NA";
                        cps.Semester = "NA";
                        cps.StudentId = stu[0].StudentId;
                        cps.StudentName = stu[0].StudentName;
                        cps.Units = int.Parse(row.Cells[4].Text);
                        cps.Year = int.Parse(stu[0].StartYear.ToString());
                        cps.CourseType = "ELEC";
                        cpsList.Add(cps);

                    }
                }
            }

            int count= business.GenerateInitialCPS(cpsList);
            if(count==cpsList.Count)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script> alert('Form Generated Successfully!!')</script>");
                Response.Redirect("~/AcadAdvisor.aspx");
            }
        }

        protected void sgnButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }
    }
}