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
        List<int> hours = new List<int>();
        List<int> mins = new List<int>();
        protected void GetHours()
        {
            for (int i = 8; i <= 17; i++)
            {
                hours.Add(i);
            }
        }
        protected void GetMins()
        {
            for (int i = 00; i <= 60; i++)
            {
                mins.Add(i);
            }
        }
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
                List<Student> student = bl.getStudent(user.Userid);

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
            scheduleappntPanel.Visible = false;
            
        }

        protected void sgnButton_Click(object sender, EventArgs e)
        {
            
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }

        //protected void ScheduleAppoint_Click(object sender, EventArgs e)
        //{
        //    string appointmentDate=txtCalender.Text+" "+ddlhrs.Text+":"+ddlmins.Text+" "+lblampm.Text;
        //    bool isScheduled = bizl.ScheduleAppoinment(std.StudentId,std.StudentName,appointmentDate,std.FacultyAdvisorId);
        //    SendEmail();
        //    //SendNotificationEmail();
        //}
        //private bool SendEmail()
        //{
        //    System.Net.Mail.MailMessage MailObject = new System.Net.Mail.MailMessage();
        //    //System.Net.Mail.MailMessage MailObject2 = new System.Net.Mail.MailMessage();
        //    MailObject.From = new MailAddress(ConfigurationSettings.AppSettings["EmailFrom"].ToString());
        //    //MailObject.To.Add(new MailAddress("hemani.ak786@gmail.com"));
        //    MailObject.To.Add(new MailAddress(std.facultyEmail));
        //    MailObject.CC.Add(new MailAddress(std.StudentEmail));
        //    MailObject.Subject = "CPS Appointment Schedule";
        //    string ErrorHandling_Message = "Hello Professor," + " " + "<br/>" + "The following Student has scheduled an appointment on " + txtCalender.Text + "for discussing the Candidate Plan of Study";
        //    ErrorHandling_Message += "'<br><br>StudentId:'" + std.StudentId;
        //    ErrorHandling_Message += "<br><br>Student Name:" + std.StudentName;
        //    ErrorHandling_Message += "<br><br>Thank you," + "<br>" + "CPS Application Team";
        //    try
        //    {
        //        MailObject.Body = ErrorHandling_Message;
        //        MailObject.IsBodyHtml = true;
        //        MailObject.Priority = System.Net.Mail.MailPriority.High;
        //        SmtpClient smtp = new SmtpClient();

        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        //smtp.Host = "smtp.mail.yahoo.com";
        //        //smtp.Port = 465;

        //        smtp.Credentials = new NetworkCredential(ConfigurationSettings.AppSettings["EmailFrom"].ToString(), ConfigurationSettings.AppSettings["Password"].ToString());
        //        //smtp.Credentials = new NetworkCredential(ConfigurationSettings.AppSettings["EmailCC"].ToString(), ConfigurationSettings.AppSettings["Password"].ToString());

        //        smtp.EnableSsl = true;
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Send(MailObject);
        //        lblAdvisor.Visible = false;
        //        lblMessage.Visible = true;
        //        txtCalender.Visible = false;
        //        lblDateSchedule.Visible = false;
        //        btnScheduleAppt.Visible = false;
        //        lblAdvisorName.Visible = false;
        //        ddlhrs.Visible = false;
        //        ddlmins.Visible = false;
        //        lblampm.Visible = false;
        //        lblMessage.Text = "A confirmation Mail has been sent to the professor to accept or reschedule the appointment " + "<br/></br> Thank you <br/> CPS Application Team ";

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        protected void schApp_Click(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = false;
            CPSChangeFormPanel.Visible = false;
            viewPrintCPSPanel.Visible = false;
            scheduleappntPanel.Visible = true;
            lblAdvisor.Visible = true;
            lblMessage.Visible = false;
            txtCalender.Visible = true;
            lblDateSchedule.Visible = true;
            btnScheduleAppt.Visible = true;
            lblAdvisorName.Visible = true;
            lblAdvisorName.Text = std.FacultyAdvisorId.ToString();
            hours.Clear();
            GetHours();
            mins.Clear();
            GetMins();
            ddlhrs.DataSource = hours;
            ddlmins.DataSource = mins;
            ddlhrs.DataBind();
            ddlmins.DataBind();
        }

        protected void retCPS_Click(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = false;
            CPSChangeFormPanel.Visible = false;
            viewPrintCPSPanel.Visible = true;
            scheduleappntPanel.Visible = false;
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

        protected void ddlhrs_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlhrs.Text) < 12)
            {
                lblampm.Text = "AM";
            }
            else if (int.Parse(ddlhrs.Text) == 12 || int.Parse(ddlhrs.Text) < 17)
            {
                lblampm.Text = "PM";
            }
        }

        protected void ddlhrs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cpsChng_Click(object sender, EventArgs e)
        {
            initialCPSPanel.Visible = false;
            CPSChangeFormPanel.Visible = true;
            viewPrintCPSPanel.Visible = false;
            scheduleappntPanel.Visible = false;
        }

    }
}