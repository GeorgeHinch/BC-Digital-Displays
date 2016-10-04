using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class settings_calendar_manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string v = Request.QueryString["remove"];
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
                    cmd.CommandText = "UPDATE [bcEvents] SET [deleted]='1' WHERE [id]='" + v.ToUpper() + "'";
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        //Success notification
                        Debug.WriteLine("If");
                    }
                    else
                    {
                        //Error notification
                        Debug.WriteLine("Else");
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

        StringBuilder eventHtmlTable = new StringBuilder();
        eventHtmlTable.AppendLine("<table>");

        eventHtmlTable.AppendLine("<thead>");
        eventHtmlTable.AppendLine("<tr>");
        eventHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        eventHtmlTable.AppendLine("<th style=\"width: 40%;\">Event</th>");
        eventHtmlTable.AppendLine("<th style=\"width: 10%;\">Day</th>");
        eventHtmlTable.AppendLine("<th style=\"width: 25%;\">Time</th>");
        eventHtmlTable.AppendLine("<th style=\"width: 25%;\"></th> ");
        eventHtmlTable.AppendLine("</tr>");
        eventHtmlTable.AppendLine("</thead>");

        eventHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List<bcEvents> data = GetData();
        foreach (bcEvents ev in data)
        {
            string trimTime = ev.startTime;
            Debug.WriteLine("Trime Time: " + trimTime + " |");
            DateTime start = DateTime.ParseExact(trimTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);

            string TimeStart = String.Format("{0:t}", start);
            string DayStart = String.Format("{0:m}", start);

            eventHtmlTable.AppendLine("<tr>");
            eventHtmlTable.AppendLine("<td>" + num + "</td>");
            eventHtmlTable.AppendLine("<td>" + ev.name + "</td>");
            eventHtmlTable.AppendLine("<td>" + DayStart + "</td>");
            eventHtmlTable.AppendLine("<td>" + TimeStart + "</td>");
            eventHtmlTable.AppendLine("<td><a href=\"add/add-event.aspx?edit=" + ev.id + "\">edit</a> / <a href=\"?remove=" + ev.id + "\">remove</a></td>");
            eventHtmlTable.AppendLine("</tr>");

            num++;
        }

        eventHtmlTable.AppendLine("</tbody>");

        eventHtmlTable.AppendLine("</table>");

        eventTable.Text = eventHtmlTable.ToString();
    }

    public List<bcEvents> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcEvents> data = new List<bcEvents>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcEvents] WHERE [deleted]='0' ORDER BY orderTime", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcEvents obj = new bcEvents(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["name"],
                    (bool)sdr["allDay"],
                    (DateTimeOffset)sdr["orderTime"],
                    (string)sdr["startTime"],
                    (string)sdr["endTime"],
                    (string)sdr["location"],
                    (string)sdr["instructor"],
                    (string)sdr["description"],
                    (string)sdr["department"],
                    (string)sdr["flier"],
                    (string)sdr["price"],
                    (bool)sdr["isApproved"]);
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