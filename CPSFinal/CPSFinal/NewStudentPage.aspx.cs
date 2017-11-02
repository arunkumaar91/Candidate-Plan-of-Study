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
        protected void Page_Load(object sender, EventArgs e)
        {
            int studentId = 0;
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["sid"] != null)
                    {
                        studentId=int.Parse(Request.QueryString["sid"].ToString());
                        List<Student> stu= business.getStudent(studentId);
                        fvStudents.DataSource = stu;
                        fvStudents.DataBind();
                        string deptName = "CSCI";
                        AchieversDAL dal = new AchieversDAL();
                        ddl.DataSource = dal.GetAllCourses(deptName);
                        ddl.DataBind();

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
    }
}