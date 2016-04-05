using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class oops : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //http://www.asp.net/web-forms/overview/older-versions-getting-started/deploying-web-site-projects/displaying-a-custom-error-page-cs
        Exception err = Server.GetLastError();
        Debug.WriteLine("-----");
        Debug.WriteLine("Exception: " + err.ToString() + " |");
        Debug.WriteLine("Exception: " + err.Message + " |");
        Debug.WriteLine("Source: " + err.Source + " |");
        Debug.WriteLine("Target Site: " + err.TargetSite + " |");
        Debug.WriteLine("StackTrace: " + err.StackTrace + " |");
        Debug.WriteLine("Inner Exception: " + err.InnerException + " |");
        Debug.WriteLine("Data: " + err.Data + " |");
        Debug.WriteLine("-----");

        StringBuilder errMsg = new StringBuilder();
        errMsg.AppendLine("<h2>" + err.Message.ToString() + "</h2>");
        errMsg.AppendLine("<hr />");
        errMsg.AppendLine("<h3>Developer Log</h3>");
        errMsg.AppendLine("<p>" + err.ToString() + "</p>");

        serverError.Text = errMsg.ToString();
    }
}