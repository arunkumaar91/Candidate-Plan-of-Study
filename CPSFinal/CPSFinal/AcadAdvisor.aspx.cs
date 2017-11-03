using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    public partial class AcadAdvisor : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
            if (Session["userRole"] == null)
            {
                Session["user"] = null;
                Response.Redirect("Login.aspx");
            }
            else if (Session["userRole"].ToString() == "academic")
            {
                Users user = new Users();
                user = (Users)(Session["user"]);
            }
            AchieversDAL dal = new AchieversDAL();
            ddlDepts.DataSource = dal.GetAllDept();
            ddlDepts.DataBind();
            if(DateTime.Now.Month<4)
            {
                txt_sem.Text = "Spring";
                txt_year.Text = DateTime.Now.Year.ToString();
            }
            else if(DateTime.Now.Month>8)
            {
                txt_sem.Text = "Fall";
                txt_year.Text = DateTime.Now.Year.ToString();
            }
            else
            {
                txt_sem.Text = "Summer";
                txt_year.Text = DateTime.Now.Year.ToString();
            }
            }
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
        }

        protected void addCatalog_Click(object sender, EventArgs e)
        {
            AddCatalogPanel.Visible = true;
            generateformpanel.Visible = false;
            
        }

        protected void btn_viewStudentList_Click(object sender, EventArgs e)
        {
            AchieversBL busL = new AchieversBL();
            List<StudentGrid1> studentList =new List<StudentGrid1>();
            studentList = busL.GetAllStudentsBySemester(ddlDepts.SelectedItem.Text, txt_sem.Text,int.Parse(txt_year.Text));
            grdAllStudents.DataSource = studentList;
            grdAllStudents.DataBind();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=UHCL_EM_ACTIVE_COURSE_CATALOG_7133_"+DateTime.Now.Year+".xlsx");
            //string path =  AppDomain.CurrentDomain.BaseDirectory + @"DefaultPDF's\UHCL_EM_ACTIVE_COURSE_CATALOG_7133.xlsx";
            Response.TransmitFile(Server.MapPath("~/DefaultPDF's/UHCL_EM_ACTIVE_COURSE_CATALOG_7133.xlsx"));
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
    }
}