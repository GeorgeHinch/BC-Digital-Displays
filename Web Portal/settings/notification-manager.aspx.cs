using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class settings_notification_manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string v = Request.QueryString["remove"];
        string a = Request.QueryString["active"];
        #region Remove brochure
        if (v != null)
        {
            string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [bcRecBrochure] SET [deleted]='1' WHERE [id]='" + v.ToUpper() + "'";
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        //Success notification
                        Debug.WriteLine("Updated successfully.");
                    }
                    else
                    {
                        //Error notification
                        Debug.WriteLine("Update failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                //log error 
                //display friendly error to user
                Debug.WriteLine("Ex: " + ex.Message + " |");
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    //cleanup connection i.e close 
                    conn.Close();
                }
            }
        }
        #endregion

        #region Set brochure to active
        if (a != null)
        {
            string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE [bcRecBrochure] SET isActive='0' WHERE isActive='0'"))
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE [bcRecBrochure] SET [isActive]='1' WHERE [id]='" + a.ToUpper() + "'";
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        //Success notification
                        Debug.WriteLine("Updated successfully.");
                    }
                    else
                    {
                        //Error notification
                        Debug.WriteLine("Update failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                //log error 
                //display friendly error to user
                Debug.WriteLine("Ex: " + ex.Message + " |");
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    //cleanup connection i.e close 
                    conn.Close();
                }
            }
        }
        #endregion


        StringBuilder notificationHtmlTable = new StringBuilder();
        notificationHtmlTable.AppendLine("<table>");

        notificationHtmlTable.AppendLine("<thead>");
        notificationHtmlTable.AppendLine("<tr>");
        notificationHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        notificationHtmlTable.AppendLine("<th style=\"width: 55%;\"> Subject</th>");
        notificationHtmlTable.AppendLine("<th style=\"width: 10%;\"> Start Day</th>");
        notificationHtmlTable.AppendLine("<th style=\"width: 10%;\"> End Day</th>");
        notificationHtmlTable.AppendLine("<th style=\"width: 20%;\"></th> ");
        notificationHtmlTable.AppendLine("</tr>");
        notificationHtmlTable.AppendLine("</thead>");

        notificationHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List <bcNotification> data = GetData();
        foreach (bcNotification t in data)
        {
            notificationHtmlTable.AppendLine("<tr>");
            notificationHtmlTable.AppendLine("<td>"+ num + "</td>");
            notificationHtmlTable.AppendLine("<td>" + t.subject + "</td>");
            notificationHtmlTable.AppendLine("<td>" + t.startDate.ToString("M/d/yy") + "</td>");
            notificationHtmlTable.AppendLine("<td>" + t.endDate.ToString("M/d/yy") + "</td>");
            notificationHtmlTable.AppendLine("<td><a href=\"add/add-notification.aspx?edit=" + t.id + "\">edit</a> / <a href=\"?remove=" + t.id + "\">remove</a></td>");
            notificationHtmlTable.AppendLine("</tr>");

            num++;
        }

        notificationHtmlTable.AppendLine("</tbody>");

        notificationHtmlTable.AppendLine("</table>");

        notificationTable.Text = notificationHtmlTable.ToString();
    }

    public List<bcNotification> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcNotification> data = new List<bcNotification>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcNotification] WHERE [deleted]='0' ORDER BY startDate", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcNotification obj = new bcNotification(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["glyph"],
                    (string)sdr["subject"],
                    (string)sdr["message"],
                    (DateTimeOffset)sdr["startDate"],
                    (DateTimeOffset)sdr["endDate"]);
                data.Add(obj);
            }

            conn.Close();
            return data;
        }
        catch (Exception ex)
        {
            //log error 
            //display friendly error to user
            Debug.WriteLine("----");
            Debug.WriteLine("Source: " + ex.Source + " |");
            Debug.WriteLine("Message: " + ex.Message + " |");
            Debug.WriteLine("Stacktrace: " + ex.StackTrace + " |");
            Debug.WriteLine("Inner Exception: " + ex.InnerException + " |");
            Debug.WriteLine("----");
            throw;
        }
        finally
        {
            if (conn != null)
            {
                //cleanup connection i.e close 
                conn.Close();
            }
        }
    }
}