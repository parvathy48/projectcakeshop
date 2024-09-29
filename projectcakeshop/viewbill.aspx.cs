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
                string sel1 = "select sum(Grand_Total)from bill_tab where user_id=" + Session["uid"] + "";
                Label9.Text = obj.Fn_Scalar(sel1);
                string sel = "select Bill_id,Date from bill_tab where User_id=" + Session["uid"] + "";
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
            string sel = "SELECT dbo.product_tab.Name, dbo.order_tab.Quantity, dbo.order_tab.Total_Price FROM dbo.order_tab INNER JOIN dbo.product_tab ON dbo.order_tab.Product_id = dbo.product_tab.Product_id and User_id=" + Session["uid"];
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            
        }
        

       
    }
}