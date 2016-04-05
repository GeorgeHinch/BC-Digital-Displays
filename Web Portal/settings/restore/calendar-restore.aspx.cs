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

public partial class settings_restore_calendar_restore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string v = Request.QueryString["restore"];
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
                    cmd.CommandText = "UPDATE [events] SET [isActive]='1' WHERE [guid]='" + v.ToUpper() + "'";
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

        List<Events> data = GetData();
        foreach (Events ev in data)
        {
            string trimTime = ev.StartTime.Trim();
            Debug.WriteLine("Trime Time: " + trimTime + " |");
            DateTime start = DateTime.ParseExact(trimTime, "yyyy,  M,  d,  H,  m,  s", System.Globalization.CultureInfo.CurrentCulture);

            string TimeStart = String.Format("{0:t}", start);
            string DayStart = String.Format("{0:m}", start);

            eventHtmlTable.AppendLine("<tr>");
            eventHtmlTable.AppendLine("<td>" + num + "</td>");
            eventHtmlTable.AppendLine("<td>" + ev.Subject.Trim() + "</td>");
            eventHtmlTable.AppendLine("<td>" + DayStart + "</td>");
            eventHtmlTable.AppendLine("<td>" + TimeStart + "</td>");
            eventHtmlTable.AppendLine("<td><a href=\"add/add-event.aspx?edit=" + ev.guid + "\">edit</a> / <a href=\"?restore=" + ev.guid + "\">restore</a></td>");
            eventHtmlTable.AppendLine("</tr>");

            num++;
        }

        eventHtmlTable.AppendLine("</tbody>");

        eventHtmlTable.AppendLine("</table>");

        eventTable.Text = eventHtmlTable.ToString();
    }

    public List<Events> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<Events> data = new List<Events>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [events] WHERE [isActive]='0' ORDER BY orderTime", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Events obj = new Events(
                    (bool)sdr["isActive"],
                    (Guid)sdr["guid"],
                    (DateTime)sdr["created"],
                    (string)sdr["name"],
                    (string)sdr["location"],
                    (string)sdr["description"],
                    (DateTime)sdr["orderTime"],
                    (string)sdr["startTime"],
                    (string)sdr["endTime"],
                    (string)sdr["instructor"],
                    (string)sdr["department"],
                    (string)sdr["price"],
                    (string)sdr["flier"],
                    (bool)sdr["allDay"]);
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