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
    public partial class Category : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GrindBind_Fun();
            }         

        }

        public void GrindBind_Fun()
        {


            String sel = "select * from category_tab";
            DataSet ds = obj.Fn_Dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string path = "~/Categoryphotos/" + FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(path));

            string str = "insert into category_tab values('" + TextBox1.Text + "','" + path + "','" + TextBox2.Text + "','Available')";
            int i = obj.Fn_Nonquery(str);
            if (i == 1)
            {
                Label2.Text = "Data Inserted Successfully";
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            Label2.Visible = false;

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
           
        }
     

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GrindBind_Fun();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GrindBind_Fun();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //int i = e.RowIndex;
            //string Cat_Id = GridView1.DataKeys[i].Value.ToString();
            //int Category_Id = Convert.ToInt32(Cat_Id);
            //GridViewRow row = GridView1.Rows[e.RowIndex];
            //TextBox txtname = (TextBox)row.FindControl("Textbox4");
            //string tca = txtname.Text;
            //FileUpload f1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("Fileupload2");
            //TextBox txtdesc = (TextBox)row.FindControl("Textbox3");
            //string tc = txtdesc.Text;
            //TextBox txtstatus = (TextBox)row.FindControl("Textbox5");
            //string ts = txtstatus.Text;

            int i = e.RowIndex;
            int getid = Convert.ToInt32(GridView1.DataKeys[i].Value);
            TextBox txtname = (TextBox)GridView1.Rows[i].Cells[1].FindControl("Textbox4");
            FileUpload f1 = (FileUpload)GridView1.Rows[i].Cells[2].FindControl("FileUpload2");
            string pathupdate = "~/Categoryphotos/" + f1.FileName;
            f1.SaveAs(MapPath(pathupdate));
            TextBox txtdes = (TextBox)GridView1.Rows[i].Cells[3].FindControl("Textbox3");
            TextBox txtstatus = (TextBox)GridView1.Rows[i].Cells[4].FindControl("Textbox5");

           
            string strup = "update category_tab set Name='"+txtname.Text+"',Image='" + pathupdate + "',Description='" +txtdes.Text+"',Status='"+txtstatus.Text+"' where category_id=" + getid + "";
            int j = obj.Fn_Nonquery(strup);

            if (j == 1)
            {
                Label7.Text = "Data Updated Successfully";
            }
            GridView1.EditIndex = -1;
            GrindBind_Fun();


        }

        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            int category_id = Convert.ToInt32(GridView1.DataKeys[i].Value);
            string del = "Delete from category_tab where category_id=" + category_id + "";
            int j = obj.Fn_Nonquery(del);
            if (j == 1)
            {
                GridView1.EditIndex = -1;
                GrindBind_Fun();

                Label7.Text = "Data Deleted Successfully!!";
            }

        }
    }
    }
