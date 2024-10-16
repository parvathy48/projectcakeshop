using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace projectcakeshop
{
    public partial class Replyfeedback : System.Web.UI.Page
    {
        Connectionclass obj = new Connectionclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sel = "select Email from user_reg where user_id=" + Session["getid"] + "";
            string s = obj.Fn_Scalar(sel);
            TextBox1.Text = s;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string t = TextBox1.Text;
            string s = TextBox2.Text;
            string r = TextBox3.Text;
            SendEmail2("Cakeshop", "cakeshopp768@gmail.com", "lbid ydfg rmqv xwaw", "parvathy", t, s, r);
            string up = "update feedback_tab set RplyMsg='" + TextBox3.Text + "',FeedStatus=1 where User_id=" + Session["getid"] + "";
            int i = obj.Fn_Nonquery(up);
            if (i == 1)
            {
                Label6.Visible = true;
                Label6.Text = "Successfully send";
            }
        }
        public static void SendEmail2(string yourName, string yourGmailUserName, string yourGmailPassword, string toName, string toEmail, string subject, string body)

        {
            string to = toEmail; //To address    
            string from = yourGmailUserName; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(yourGmailUserName, yourGmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}