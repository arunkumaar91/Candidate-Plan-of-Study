using System;
using System.Collections.Generic;
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
            AddCatalogPanel.Visible = true;
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
    }
}