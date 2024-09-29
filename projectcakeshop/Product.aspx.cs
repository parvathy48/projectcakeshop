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
    public partial class Product : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                string sel = "Select Category_id,Name from category_tab";
                DataSet ds = obj.Fn_Dataset(sel);
                DropDownList1.DataSource = ds;
                DropDownList1.DataValueField = "Category_id";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string p = "~/Productphotos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(p));
            string ins="insert into product_tab values("+DropDownList1.SelectedItem.Value+",'"+TextBox1.Text+"',"+TextBox2.Text+",'"+TextBox3.Text+"','"+p+"','"+TextBox4.Text+"','Available')";
            int i = obj.Fn_Nonquery(ins);
            if (i == 1)
            {
                Label2.Text = "Data Inserted Successfully";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            Label2.Visible = false;

        }
    }
   
}

