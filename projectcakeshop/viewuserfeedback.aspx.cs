using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace projectcakeshop
{
    public partial class viewuserfeedback : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fn_Grindbind();
            }
        }
        public void Fn_Grindbind()
        {
            string sel = "Select * from feedback_tab where FeedStatus=0";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        //protected void Button2_Command(object sender, CommandEventArgs e)
        //{
        //    Session["getid"] = Convert.ToInt32(e.CommandArgument);
        //    Response.Redirect("Replyfeedback.aspx");
        //}

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            Session["getid"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("Replyfeedback.aspx");
        }

    }
    }
