using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    
    public partial class FacultyAdvisor : System.Web.UI.Page
    {
        public string Path;
        AchieversBL bizl = new AchieversBL();
        Student std = new Student();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userRole"] == null)
            {
                Session["user"] = null;
                Response.Redirect("Login.aspx");
            }
            else if (Session["userRole"].ToString() == "Faculty")
            {
                Users user = new Users();
                user = (Users)(Session["user"]);
                AchieversBL bl = new AchieversBL();
                sgnName.Text = user.FullName;
            } 
        }
        
        protected void btn_viewStdCPS_Click(object sender, EventArgs e)
        {
            getstudentdetailspanel.Visible = false;
            vieworprintcpsfac.Visible = true;
            pnlPrintCPS.Visible = true;
            List<Student> studentList = bizl.getStudent(int.Parse(txt_view_std_id.Text));

            std = studentList[0];
            lblName.Text = std.StudentName;
            lblAdmitted.Text = std.StartYear.ToString();
            lblSemester.Text = std.semester;
            lblDept.Text = std.ProgramName;
            BindData(std.StudentId);
        }
        private void BindData(int stdId)
        {
            grdCoreCourses.DataSource = bizl.GetCoreCourses(stdId);
            if (bizl.GetCoreCourses(stdId).Count > 0)
            {
                lblCore.Text = "Core Courses:";
            }
            grdfdnCourses.DataSource = bizl.GetFoundationCourses(stdId);
            if (bizl.GetFoundationCourses(stdId).Count > 0)
            {
                lblFoundation.Text = "Foundation Courses";
            }
            grdElectiveCourses.DataSource = bizl.GetElectiveCourses(stdId);
            if (bizl.GetElectiveCourses(stdId).Count > 0)
            {
                lblElective.Text = "Elective Courses";
            }
            grdCoreCourses.DataBind();
            grdElectiveCourses.DataBind();
            grdfdnCourses.DataBind();
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

        }

        protected void getStudentDetails_Click(object sender, EventArgs e)
        {

        }

        protected void modifyDraftCPS_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    //GridView1.AllowPaging = false;
                    //this.BindData(std.StudentId);
                    pnlPrintCPS.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc,Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + lblName.Text + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        protected void viewStudentCPS_Click(object sender, EventArgs e)
        {
            getstudentdetailspanel.Visible = false;
            vieworprintcpsfac.Visible = true;
            pnlPrintCPS.Visible = false;
        }

        protected void btn_view_std_list_Click(object sender, EventArgs e)
        {
            pnlPrintCPS.Visible = true;
            List<Student> studentList = bizl.getStudent(int.Parse(txt_Std_Id.Text));
            std = studentList[0];
            lblAdmitted.Text = std.StartYear.ToString();
            lblSemester.Text = std.semester;
            lblDept.Text = std.ProgramName;
            BindData(std.StudentId);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}