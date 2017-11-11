using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AchieversCPS
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetUser_Click(object sender, EventArgs e)
        {
            AchieversBL bl = new AchieversBL();
            Users user= bl.GetUserById(txtUserId.Text);
            lblUserName.Text = user.UserName;
            lblPassword.Text = user.Password;
        }
    }
}