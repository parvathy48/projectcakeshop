using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectcakeshop
{
    public partial class Userregister : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Reg_id) from login_tab";
            string regid = obj.Fn_Scalar(sel);
            int Reg_id = 0;
            if (regid == "")
            {
                Reg_id = 1;
            }
            else
            {
                int newregid = Convert.ToInt32(regid);
                Reg_id = newregid + 1;

            }
            string ins = "Insert into user_reg values(" + Reg_id + ",'" + TextBox1.Text + "','" + TextBox2.Text + "'," + TextBox3.Text + ",'" + TextBox4.Text + "','" + DropDownList1.SelectedItem.Text + "'," + TextBox5.Text + ",'" + TextBox6.Text + "','Active')";
            int i = obj.Fn_Nonquery(ins);
            if (i == 1)
            {
                string inslog = "insert into login_tab values('" + TextBox7.Text + "','" + TextBox8.Text + "'," + Reg_id + ",'User')";
                int j = obj.Fn_Nonquery(inslog);
            }

        }
    }
}

