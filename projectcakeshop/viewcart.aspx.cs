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
    public partial class viewcart : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grindbind();
            }
            string csum = "select sum(Total_Price) from cart_tab where User_id=" + Session["uid"] + "";
            int su = Convert.ToInt32(obj.Fn_Scalar(csum));
            Label5.Text = su.ToString();
        }
        public void grindbind()
        {
            String sel = "select product_tab.Name,cart_tab.Quantity,cart_tab.Total_Price,product_tab.Image,cart_tab.Product_id from product_tab join cart_tab on product_tab.Product_id=cart_tab.Product_id and User_id="+Session["uid"]+"";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            grindbind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            grindbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string sel = "select Price from product_tab where Product_id=" + getid + "";
            string pp = obj.Fn_Scalar(sel);
            Session["Price"] = pp;
            TextBox txtqua = (TextBox)GridView1.Rows[i].Cells[2].FindControl("Textbox2");
            decimal j = Convert.ToDecimal(Session["Price"]) * Convert.ToDecimal(txtqua.Text);
            string up="update cart_tab set Quantity=" + txtqua.Text + ",Total_Price='" + j +"' where Product_id="+ getid+ "";
            obj.Fn_Nonquery(up);
            GridView1.EditIndex = -1;
            grindbind();


        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string del = "delete from cart_tab where Product_id=" + getid + "";
            obj.Fn_Nonquery(del);
            grindbind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select * from cart_tab where User_id=" + Session["uid"] + "";
            List<int> lis = new List<int>();
            SqlDataReader dr = obj.Fn_Reader(sel);
            while(dr.Read())
            {
                lis.Add(Convert.ToInt32(dr["Cart_Id"]));
            }
            foreach (int i in lis)
            {
                string sel1 = "select * from cart_tab where(cart_id=" + i +" AND User_id=" + Session["uid"] + ")";
                SqlDataReader dr1 = obj.Fn_Reader(sel1);
                int pid = 0;
                decimal qty = 0;
                decimal tp = 0;
                while (dr1.Read())
                {
                    pid = Convert.ToInt32(dr1["Product_id"]);
                    qty = Convert.ToInt32(dr1["Quantity"]);
                    tp = Convert.ToDecimal(dr1["Total_Price"]);
                }
                string ins = "insert into order_tab values(" + pid + "," + Session["uid"] + "," + qty + "," + tp + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                int k = obj.Fn_Nonquery(ins);
                string del = "delete from cart_tab where Product_id=" + pid + " and user_id=" + Session["uid"] + "";
                int d = obj.Fn_Nonquery(del);
            }
            decimal gt = Convert.ToDecimal(Label5.Text);
            string ins1 = "insert into bill_tab values(" + Session["uid"] + "," + gt + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','Ordered')";
            obj.Fn_Nonquery(ins1);
            Response.Redirect("viewbill.aspx");
        }
    }
}