using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    public partial class AddMember : System.Web.UI.Page
    {
        AchieversBL bizl = new AchieversBL();
        Dictionary<int, string> faculties = new Dictionary<int, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                pnlAddFac.Visible = false;
                pnlAddStudent.Visible = false;
            }
        }

        protected void txtStudentLastName_TextChanged(object sender, EventArgs e)
        {
            Random rand=new Random(1);
            txtStudentEmail.Text = txtStudentLastName.Text + txtStudentFirstName.Text.ElementAt(0).ToString() + rand.Next().ToString().Substring(0,4);
        }

        protected void ddlMemberSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlMemberSelection.Text=="Student")
            {
                pnlAddFac.Visible = false;
                pnlAddStudent.Visible = true;
                faculties = bizl.GetAllFaculties();
                ddlAdvisor.DataSource = faculties;
                ddlAdvisor.DataValueField = "key";
                ddlAdvisor.DataTextField = "value";
                ddlAdvisor.DataBind();
            }
            else if (ddlMemberSelection.Text == "Faculty Advisor")
            {
                pnlAddFac.Visible = true;
                pnlAddStudent.Visible = false;
            }
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Student std = new Student();
            bool isAdded = false;
            std.StudentId =int.Parse(txtStudentId.Text);
            std.StudentName = txtStudentFirstName.Text + " " + txtStudentLastName.Text;
            std.StudentEmail = txtStudentEmail.Text + "@uhcl.edu";
            std.UserName = txtStudentEmail.Text;
            std.degreeType = "Masters";
            std.Address = "300 Cyberonics blvd";
            std.contactNumber = "832770420";
            std.semester = "fall";
            string[] dept=ddlDept.Text.Split('-');

            std.ProgramName = dept[dept.Length - 1];
            std.gender = ddlGender.Text;
            std.FacultyAdvisor = ddlAdvisor.SelectedValue;
            std.StudentDOB = txtCalender.Text;
            Users user = new Users();
            user.FullName = std.StudentName;
            user.Password = txtStudentPass.Text;
            user.Role = "Student";
            user.Userid = std.StudentId.ToString();
            user.UserName = std.UserName;
            isAdded = bizl.AddStudent(std,user);
            if(isAdded)
            {
                Response.Write("Added Successfully");
            }
            else
            {
                Response.Write("Adddition unsuccessful");
            }

        }
    }
}