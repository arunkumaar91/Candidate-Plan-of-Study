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
                Users user = new Users();
                foreach (Users u in uList)
                {
                    user = u;
                }
                Session["user"] = user;
                Session["userRole"] = user.Role;
                Session["userId"] = user.Userid;
                if(user.Role=="student")
                {
                    Response.Redirect("~\\studentHomePage.aspx");
                }
                else if(user.Role=="academic")
                {
                    Response.Redirect("~\\AcadAdvisor.aspx");
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