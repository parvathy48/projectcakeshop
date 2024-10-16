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
    public partial class viewbill : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string sel1 = "select sum(Grand_Total)from bill_tab where user_id=" + Session["uid"] + "and Status='Ordered'";
                Label9.Text = obj.Fn_Scalar(sel1);
                string sel = "select Bill_id,Date from bill_tab where User_id=" + Session["uid"] + "and Status='Ordered'";
                SqlDataReader dr = obj.Fn_Reader(sel);
                while(dr.Read())
                {
                    Label5.Text = dr["Bill_id"].ToString();
                    Label7.Text = dr["Date"].ToString();
                }
                grindbind_fn();
            }
        }
        public void grindbind_fn()
        {
            string sel = "SELECT dbo.product_tab.Name, dbo.order_tab.Quantity, dbo.order_tab.Total_Price FROM dbo.order_tab INNER JOIN dbo.product_tab ON dbo.order_tab.Product_id = dbo.product_tab.Product_id where User_id=" + Session["uid"] + " and order_tab.Status='Ordered'";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_accdet";
            cmd.Parameters.AddWithValue("@uid", Session["uid"]);
            cmd.Parameters.AddWithValue("@accno",TextBox1.Text);
            cmd.Parameters.AddWithValue("@accty", TextBox2.Text);
            cmd.Parameters.AddWithValue("@accbal", TextBox3.Text);
            cmd.Parameters.AddWithValue("@status", "Active");
            SqlParameter sp = new SqlParameter();
            sp.DbType = DbType.Int32;
            sp.ParameterName = "@sta";
            sp.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sp);
            obj.Fn_Nonquery_sp(cmd);
            int i = Convert.ToInt32(sp.Value);
            if(i==1)
            {
                Response.Redirect("payment.aspx");
            }
            else
            {
                Label16.Text = "Invalid Account Details";
                string up = "update order_tab set status='Failed'";
                obj.Fn_Nonquery(up);
                string up1 = "update bill_tab set Status='Failed'";
                obj.Fn_Nonquery(up1);

            }

        }
    }
}