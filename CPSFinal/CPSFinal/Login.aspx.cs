using AchieversCPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    public partial class Login : System.Web.UI.Page
    {
        AchieversBL aBL = new AchieversBL();
        AchieversDAL aDAL = new AchieversDAL();
        List<Users> uList = new List<Users>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                
        protected void login_btn1_Click(object sender, EventArgs e)
        {
            uList = aBL.GetAllUsers(UserName.Text, Password.Text);
            if (uList.Count == 0)
            {
                Response.Write("InvalidUser!!");
            }
            else if (uList.Count == 1)
            {
                
                Session["user"] = uList[0];
                Session["userRole"] = uList[0].Role;
                Session["userId"] = uList[0].Userid;
                if(uList[0].Role=="Student")
                {
                    Response.Redirect("~\\studentHomePage.aspx");
                }
                else if(uList[0].Role=="Academic")
                {
                    Response.Redirect("~\\AcadAdvisor.aspx");
                }
                else if(uList[0].Role=="Faculty")
                {
                    Response.Redirect("~\\FacultyAdvisor.aspx");
                }
                else if (uList[0].Role == "Secratery")
                {
                    Response.Redirect("~\\FacultyAdvisor.aspx");
                }
            }
        }

        protected void login_btn2_Click(object sender, EventArgs e)
        {
            UserName.Text = string.Empty;
            Password.Text = string.Empty;
        }
    }
}