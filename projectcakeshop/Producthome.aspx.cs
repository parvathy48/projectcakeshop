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
    public partial class Producthome : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
             
                string sel = "select * from product_tab where category_id=" + Session["Category_id"] + "";
                DataTable dt = obj.Fn_DataTable(sel);
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }

        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {

        }   

        protected void DataList1_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            int getid = Convert.ToInt32(e.CommandArgument);
            Session["Product_id"] = getid;
            Response.Redirect("Itemdisplay.aspx");
        }
    }
}