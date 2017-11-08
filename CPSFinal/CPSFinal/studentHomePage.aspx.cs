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
        AchieversBL bizl = new AchieversBL();
        Student std = new Student();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userRole"] == null)
            {
                Session["user"] = null;
                Response.Redirect("Login.aspx");
            }
            else if(Session["userRole"].ToString()=="student")
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

        private void BindData(int stdId, string pgmName)
        {
            grdCoreCourses.DataSource = bizl.getMandatoryClasses(stdId, pgmName);
            //grdCourses.Columns[0].ItemStyle.
            grdCoreCourses.DataBind();
        }

        protected void initialCPS_Click1(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = true;
            CPSChangeFormPanel.Visible = false;
            viewPrintCPSPanel.Visible = false;
            
            
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
            
            BindData(std.StudentId, std.ProgramName);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    //GridView1.AllowPaging = false;
                    this.BindData(std.StudentId, std.ProgramName);
                    viewPrintCPSPanel.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
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
            //print();
            //string attachment = "attachment; filename=ApplicationForm.pdf";

            //Response.ClearContent();

            //Response.AddHeader("content-disposition", attachment);

            //Response.ContentType = "application/pdf";

            //StringWriter stw = new StringWriter();

            //HtmlTextWriter htextw = new HtmlTextWriter(stw);

            //viewPrintCPSPanel.RenderControl(htextw);

            //Document document = new Document();

            //PdfWriter.GetInstance(document, Response.OutputStream);

            //document.Open();

            //StringReader str = new StringReader(stw.ToString());

            //HTMLWorker htmlworker = new HTMLWorker(document);

            //htmlworker.Parse(str);

            //document.Close();

            //Response.Write(document);

            //Response.End();
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