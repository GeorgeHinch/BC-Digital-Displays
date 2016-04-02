using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        string name = emailName.Text;
        string email = emailEmail.Text;
        string message = emailMessage.Text;

        // Gmail Address from where you send the mail
        var fromAddress = "*club email*";
        // any address where the email will be sending
        var toAddress = "george@georgehinch.com";
        //Password of your gmail address
        const string fromPassword = "*email password*";
        // Passing the values and make a email formate to display
        string subject = name + "has questions about the Digital Displays";
        string body = "From: " + name + "\n";
        body += "Email: " + email + "\n";
        body += "Message: \n" + message + "\n";
        // smtp settings
        var smtp = new System.Net.Mail.SmtpClient();
        {
            smtp.Host = "*smtp address";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
            smtp.Timeout = 20000;
        }
        // Passing values to smtp object
        smtp.Send(fromAddress, toAddress, subject, body);
    }
}