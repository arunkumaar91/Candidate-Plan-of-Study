using System;
using System.Collections.Generic;
using System.Linq;
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
                        
                        GridView1.DataSource = dal.GetAllCourses(deptName);
                        GridView2.DataSource = dal.GetAllMandatoryCoursesByDept(deptName);
                        GridView3.DataSource = dal.GetAllElectiveCoursesByDept(deptName);

                        GridView1.DataBind();
                        GridView2.DataBind();
                        GridView3.DataBind();
                                        

                    }
                    else
                    {
                        Server.Transfer("Error.aspx");
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
            Server.Transfer("~/AcadAdvisor.aspx");
        }

        protected void auditCPS_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/AcadAdvisor.aspx");
        }

        protected void modifyCPS_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/AcadAdvisor.aspx");
        }

        protected void addCatalog_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/AcadAdvisor.aspx");
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