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
    public partial class Login : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select count(Reg_id) from login_tab where username='" + TextBox1.Text + "'and password='" + TextBox2.Text + "'";
            string cid = obj.Fn_Scalar(str);
            int cid1 = Convert.ToInt32(cid);
            if (cid1 == 1)
            {
                string str1 = "select Reg_id from login_tab where username='" + TextBox1.Text + "'and password='" + TextBox2.Text + "'";
                string id = obj.Fn_Scalar(str1);
                Session["uid"] = id;
                string str2 = "select Log_Type from login_tab where username='" + TextBox1.Text + "'and password='" + TextBox2.Text + "'";
                string log = obj.Fn_Scalar(str2);
                if (log == "Admin")
                {
                    Response.Redirect("adminview.aspx");
                }
                else if (log == "User")
                {
                    Response.Redirect("userview.aspx");
                }
            }
            else

            {
                Label4.Text = "Invalid username and password";
            }


        }
    }
}





