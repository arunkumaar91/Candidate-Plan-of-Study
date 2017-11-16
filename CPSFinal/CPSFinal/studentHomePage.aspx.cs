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
    public partial class studentHomePage : System.Web.UI.Page
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
            else if(Session["userRole"].ToString()=="Student")
            {
                Users user = new Users();
                user = (Users)(Session["user"]);
                AchieversBL bl = new AchieversBL();
                List<Student> student = bl.getStudent(int.Parse(user.Userid));

                if (student.Count == 1)
                {
                    foreach (Student stu in student)
                    {

                        std = stu;
                    }
                    sgnName.Text = std.StudentName;
                    lblName.Text = std.StudentName;

                }

            }
        }

        private void BindData(int stdId)
        {
            grdCoreCourses.DataSource = bizl.GetCoreCourses(stdId);
            if(bizl.GetCoreCourses(stdId).Count>0)
            {
                lblCore.Text = "Core Courses:";
            }
            grdfdnCourses.DataSource = bizl.GetFoundationCourses(stdId);
            if(bizl.GetFoundationCourses(stdId).Count>0)
            {
                lblFoundation.Text="Foundation Courses";
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
        
        protected void initialCPS_Click1(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = true;
            CPSChangeFormPanel.Visible = false;
            viewPrintCPSPanel.Visible = false;
            //this.Path = HttpContext.Current.Server.MapPath("DefaultPDF's/pdf-sample.pdf");
           // this.Path = HttpContext.Current.Server.MapPath("pdf-sample.pdf");
            string filePath = "http://dcm.uhcl.edu/capf17gswen2/DefaultPDF's/" + std.ProgramName + ".pdf";
            bool exists = false;
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(filePath);
            HttpWebResponse response=(HttpWebResponse)request.GetResponse();
            exists=response.StatusCode==HttpStatusCode.OK;
           if (exists)
            {
                this.Path = "http://dcm.uhcl.edu/capf17gswen2/DefaultPDF's/" + std.ProgramName + ".pdf";
            }
            else
            {
                this.Path = "http://dcm.uhcl.edu/capf17gswen2/DefaultPDF's/pdf-sample.pdf";
            }
        }

        protected void sgnButton_Click(object sender, EventArgs e)
        {
            
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }

        
        protected void retCPS_Click(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = false;
            CPSChangeFormPanel.Visible = false;
            viewPrintCPSPanel.Visible = true;
            lblAdmitted.Text = std.StartYear.ToString();
            lblSemester.Text = std.semester;
            lblDept.Text = std.ProgramName;
            BindData(std.StudentId);
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
                    viewPrintCPSPanel.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + std.StudentName + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        

        

        protected void cpsChng_Click(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = false;
            CPSChangeFormPanel.Visible = true;
            viewPrintCPSPanel.Visible = false;
        }

    }
}