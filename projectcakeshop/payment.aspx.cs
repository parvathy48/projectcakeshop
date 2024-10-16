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
    public partial class payment : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sel1 = "select Grand_Total from bill_tab where User_id=" + Session["uid"] + " and Status='Ordered'";
            Session["tot"] = obj.Fn_Scalar(sel1);
            Label2.Text = Session["tot"].ToString();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.ServiceClient obj1 = new ServiceReference1.ServiceClient();
            decimal bal = obj1.acc_bal(Convert.ToInt32(TextBox1.Text));
            decimal gt = Convert.ToDecimal(Session["tot"]);
            if (bal >= gt)
            {
                string sel4 = "select max(Account_id) from account_tab where User_id=" + Session["uid"] + "";
                string maid = obj.Fn_Scalar(sel4);
                int aid = Convert.ToInt32(maid);
                decimal newbal = bal - gt;
                string up = "update account_tab set Balance_Amt=" + newbal + ",Status='Deactive' where Account_id=" + aid + "";
                int i = obj.Fn_Nonquery(up);
                if (i == 1)
                {
                    string sel = "select Order_id from order_tab where User_id=" + Session["uid"] + " and Status='Ordered'";
                    List<int> olis = new List<int>();
                    SqlDataReader dr2 = obj.Fn_Reader(sel);
                    while (dr2.Read())
                    {
                        olis.Add(Convert.ToInt32(dr2["Order_id"]));
                    }
                    foreach (int k in olis)
                    {
                        string up1 = "update order_tab set Status='Paid' where Order_id=" + k + "";
                        obj.Fn_Nonquery(up1);
                    }
                    string sel1 = "select max(Bill_id) from bill_tab where User_id=" + Session["uid"] + " ";
                    string bid = obj.Fn_Scalar(sel1);
                    string up2 = "update bill_tab set Status='Paid' where Bill_id=" + bid + "";
                    obj.Fn_Nonquery(up2);
                    string sel2 = "select Product_id from order_tab where Status='Paid' and User_id=" + Session["uid"] + "";
                    List<int> plis = new List<int>();
                    SqlDataReader dr = obj.Fn_Reader(sel2);
                    while (dr.Read())
                    {
                        plis.Add(Convert.ToInt32(dr["Product_Id"]));
                    }
                    foreach (int j in plis)
                    {
                        string sel3 = "SELECT dbo.product_tab.Stock, dbo.Order_tab.Quantity FROM dbo.product_tab INNER JOIN dbo.order_tab ON dbo.product_tab.Product_Id = dbo.order_tab.Product_id where order_tab.Product_id=" + j + " and User_id=" + Session["uid"] + "";
                        SqlDataReader dr1 = obj.Fn_Reader(sel3);
                        decimal ps = 0;
                        decimal qua = 0;
                        while (dr1.Read())
                        {
                            ps = Convert.ToDecimal(dr1["Stock"]);
                            qua = Convert.ToDecimal(dr1["Quantity"]);
                        }
                        decimal newst = ps - qua;
                        string newpst = newst.ToString();
                        string up3 = "update product_tab set Stock='" + newpst + "' where Product_id=" + j + "";
                        int k = obj.Fn_Nonquery(up3);
                        if (k == 1)
                        {
                            Label4.Text = "Successfully Paid";
                        }
                    }
                }

            }
            else
            {
                Label4.Text = "Insufficient Balance";

                string sel4 = "select max(Account_id) from account_tab where User_id=" + Session["uid"] + "";
                string maid = obj.Fn_Scalar(sel4);
                int aid = Convert.ToInt32(maid);
                decimal newbal = bal - gt;
                string up = "update account_tab set status='Deactive' where Account_id=" + aid + "";
                int i = obj.Fn_Nonquery(up);
                if (i == 1)
                {
                    string sel = "select Order_id from order_tab where User_id=" + Session["uid"] + " and Status='Ordered'";
                    List<int> olis = new List<int>();
                    SqlDataReader dr2 = obj.Fn_Reader(sel);
                    while (dr2.Read())
                    {
                        olis.Add(Convert.ToInt32(dr2["Order_id"]));
                    }
                    foreach (int k in olis)
                    {
                        string up1 = "update order_tab set Status='Cancelled' where Order_id=" + k + "";
                        obj.Fn_Nonquery(up1);
                    }
                    string sel1 = "select max(Bill_id) from bill_tab where User_id=" + Session["uid"] + " ";
                    string bid = obj.Fn_Scalar(sel1);
                    string up2 = "update bill_tab set Status='Failed' where Bill_id=" + bid + "";
                    obj.Fn_Nonquery(up2);
                }
            }
        }
    }
}

