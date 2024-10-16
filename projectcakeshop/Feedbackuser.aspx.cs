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
    public partial class Feedbackuser : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ins = "insert into feedback_tab values(" + Session["uid"] + ",'" + TextBox1.Text + "',' ',0)";
            int i = obj.Fn_Nonquery(ins);
            if(i==1)
            {
                Label4.Text = "Successfully Submitted";
            }

        }
    }
}