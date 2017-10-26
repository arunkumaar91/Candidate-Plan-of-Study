using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

        }
    }

    protected void login_btn2_Click(object sender, EventArgs e)
    {
        UserName.Text = string.Empty;
        Password.Text = string.Empty;
    }
    
}