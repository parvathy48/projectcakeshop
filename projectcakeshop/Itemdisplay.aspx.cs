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
    public partial class Itemdisplay : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string sel = "select * from product_tab where Product_id=" + Session["Product_id"] + "";
                SqlDataReader dr = obj.Fn_Reader(sel);
                while(dr.Read())
                {
                    Label1.Text = dr["Name"].ToString();
                    Label2.Text = dr["Price"].ToString();
                    Label3.Text = dr["Description"].ToString();
                    Image1.ImageUrl=dr["Image"].ToString();                   
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Cart_id)from cart_tab";
            string i = obj.Fn_Scalar(sel);
            int cartid = 0;
            if (i == "")
            {
                cartid = 1;

            }
            else
            {
                int newcartid = Convert.ToInt32(i);
                cartid = newcartid + 1;
            }
           
            decimal pr =Convert.ToDecimal(Label2.Text);
            decimal q = Convert.ToDecimal(TextBox1.Text);
            decimal price = pr * q;
           



            string ins = "insert into cart_tab values(" + cartid + ","+ Session["uid"] + "," + Session["Product_id"] + "," + q + "," + price + ",'"+ DateTime.Now.ToString("yyyy-MM-dd")+"')";
            int j = obj.Fn_Nonquery(ins);
        }

    }
}