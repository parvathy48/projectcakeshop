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
    public partial class productedit : System.Web.UI.Page
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
        public void GrindBind_Fun()
        {


            String sel = "select * from product_tab where Category_id="+DropDownList1.SelectedItem.Value+"";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
      

        protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GrindBind_Fun();

        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtname = (TextBox)GridView1.Rows[i].Cells[1].FindControl("Textbox1");
            TextBox txtprice = (TextBox)GridView1.Rows[i].Cells[2].FindControl("Textbox2");
            TextBox txtstock = (TextBox)GridView1.Rows[i].Cells[3].FindControl("Textbox3");
            FileUpload f1=(FileUpload)GridView1.Rows[i].Cells[4].FindControl("FileUpload1");
            TextBox txtdes = (TextBox)GridView1.Rows[i].Cells[5].FindControl("Textbox7");
            TextBox txtstatus = (TextBox)GridView1.Rows[i].Cells[6].FindControl("Textbox6");


            string p = "~/Productphotos" + f1.FileName;
            f1.SaveAs(MapPath(p));


    
            string strup = "update product_tab set Name='" + txtname.Text + "', Price="+txtprice.Text+",Stock='"+txtstock.Text+"',Image='" + p+ "',Description='"+txtdes.Text+"',Status='"+txtstatus.Text+"' where Product_id=" + getid + "";
            int j = obj.Fn_Nonquery(strup);
            GridView1.EditIndex = -1;
            GrindBind_Fun();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GrindBind_Fun();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GrindBind_Fun();
        }
    }
}