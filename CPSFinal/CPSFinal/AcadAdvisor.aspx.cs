using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    public partial class AcadAdvisor : System.Web.UI.Page
    {
        AchieversBL bizl = new AchieversBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["userRole"] == null)
            {
                Session["user"] = null;
                Response.Redirect("Login.aspx");
            }
            else if (Session["userRole"].ToString() == "Academic")
            {
                Users user = new Users();
                user = (Users)(Session["user"]);
                sgnName.Text = user.FullName;
                //if(Page.PreviousPage.FindControl("NewStudentPage.aspx")==true)
                //{

                //}
            }
            AchieversDAL dal = new AchieversDAL();
            
        }
        protected void sgnButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }

        protected void generateForm_Click(object sender, EventArgs e)
        {
            generateformpanel.Visible = true;
            AddCatalogPanel.Visible = false;
            pnlCatalog.Visible = false;
        }

        protected void addCatalog_Click(object sender, EventArgs e)
        {
            AddCatalogPanel.Visible = false;
            generateformpanel.Visible = false;
            pnlCatalog.Visible = true;
        }

        protected void btn_viewStudentList_Click(object sender, EventArgs e)
        {
            AchieversBL busL = new AchieversBL();
            List<StudentGrid1> studentList =new List<StudentGrid1>();
            studentList = busL.GetAllStudentsBySemester( ddlSem.SelectedItem.Text,int.Parse(txt_year.Text));
            grdAllStudents.DataSource = studentList;
            grdAllStudents.DataBind();
            Session["year"]=txt_year.Text;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+DateTime.Now.Year+".xlsx");
            //string path =  AppDomain.CurrentDomain.BaseDirectory + @"DefaultPDF's\UHCL_EM_ACTIVE_COURSE_CATALOG_7133.xlsx";
            
            Response.TransmitFile(Server.MapPath("~/DefaultPDF's/UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+DateTime.Now.Year+".xlsx"));
            Response.End();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            
            if (FileUpload1.FileName.EndsWith(".xlsx")) 
            {
                FileUpload1.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "DefaultPDF's\\" + FileUpload1.FileName);
            }
            else
            {
                Response.Write("Please upload excel file");
            }            
        }

        protected void RangeValidator1_Init(object sender, EventArgs e)
        {
            ((RangeValidator)sender).MaximumValue = DateTime.Now.Year.ToString();
        }

        protected void rdoAddCatalog_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoAddCatalog.Checked==true)
            {
                AddCatalogPanel.Visible = true;
                pnlAddCourse.Visible = false;
                rdoAddCourse.Checked = false;
            }
           
        }       

        protected void rdoAddCourse_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAddCourse.Checked == true)
            {
                AddCatalogPanel.Visible = false;
                pnlAddCourse.Visible = true;
                rdoAddCatalog.Checked = false;
                
            }
            
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            AddCourse course = new AddCourse();
            course.courseRubric=ddlRubric.SelectedValue;
            course.className = txtCourseName.Text;
            course.courseNumber = int.Parse(txtNumber.Text);
            course.units = int.Parse(ddlUnits.SelectedItem.Text);
            course.CourseType = ddlCourseType.SelectedValue;
            course.Year = DateTime.Now.Year;
            course.DeptName= ddlDept.SelectedValue;
            bool isAdded = bizl.AddNewCourse(course);
            if(isAdded)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script>alert('New Course" +course.className+ "added successfully!!!')</script>");
                txtNumber.Text = "";
                txtCourseName.Text = "";
                txt_year.Text = string.Empty;
                ddlCourseType.SelectedIndex = 0;
                ddlDept.SelectedIndex = 0;
                ddlRubric.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                ddlUnits.SelectedIndex = 0;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script>alert('New Course addition unsuccessfully!!!')</script>");
            }
        }

        protected void auditCPS_Click(object sender, EventArgs e)
        {
            generateformpanel.Visible = false;
            pnlCatalog.Visible = false;
        }

        protected void modifyCPS_Click(object sender, EventArgs e)
        {
            generateformpanel.Visible = false;
            pnlCatalog.Visible = false;
        }
    }
}