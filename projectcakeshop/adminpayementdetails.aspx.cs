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
    public partial class adminpayementdetails : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            Fn_Grindbind();

        }
        public void Fn_Grindbind()
        {
            string sel = "select * from bill_tab where Status='Paid'";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Fn_Grindbind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Fn_Grindbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtsta = (TextBox)GridView1.Rows[i].Cells[3].FindControl("TextBox1");
            string up = "update bill_tab set Status='Delivered' where Bill_id=" + getid + "";
            obj.Fn_Nonquery(up);
            GridView1.EditIndex = -1;
            Fn_Grindbind();
        }
    }
}