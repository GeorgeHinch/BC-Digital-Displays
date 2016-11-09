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

public partial class settings_reciprocal_manager : System.Web.UI.Page
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
                    cmd.CommandText = "UPDATE [bcReciprocalClubs] SET [deleted]='1' WHERE [id]='" + v.ToUpper() + "'";
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

        StringBuilder clubHtmlTable = new StringBuilder();
        clubHtmlTable.AppendLine("<table>");

        clubHtmlTable.AppendLine("<thead>");
        clubHtmlTable.AppendLine("<tr>");
        clubHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        clubHtmlTable.AppendLine("<th style=\"width: 35%;\">Club</th>");
        clubHtmlTable.AppendLine("<th style=\"width: 15%;\">City</th>");
        clubHtmlTable.AppendLine("<th style=\"width: 15%;\">State</th>");
        clubHtmlTable.AppendLine("<th style=\"width: 15%;\">Country</th>");
        clubHtmlTable.AppendLine("<th style=\"width: 15%;\"></th> ");
        clubHtmlTable.AppendLine("</tr>");
        clubHtmlTable.AppendLine("</thead>");

        clubHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List<bcReciprocalClubs> data = GetData();
        foreach (bcReciprocalClubs rc in data)
        {
            clubHtmlTable.AppendLine("<tr>");
            clubHtmlTable.AppendLine("<td>" + num + "</td>");
            clubHtmlTable.AppendLine("<td>" + rc.clubName + "</td>");
            clubHtmlTable.AppendLine("<td>" + rc.sortCity + "</td>");
            clubHtmlTable.AppendLine("<td>" + rc.sortState + "</td>");
            clubHtmlTable.AppendLine("<td>" + rc.sortCountry + "</td>");
            clubHtmlTable.AppendLine("<td><a href=\"add/add-club.aspx?edit=" + rc.id + "\">edit</a> / <a href=\"?remove=" + rc.id + "\">remove</a></td>");
            clubHtmlTable.AppendLine("</tr>");

            num++;
        }

        clubHtmlTable.AppendLine("</tbody>");

        clubHtmlTable.AppendLine("</table>");

        clubTable.Text = clubHtmlTable.ToString();
    }

    public List<bcReciprocalClubs> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcReciprocalClubs> data = new List<bcReciprocalClubs>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcReciprocalClubs] WHERE [deleted]='0' ORDER BY sortCountry, sortState, sortCity", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcReciprocalClubs obj = new bcReciprocalClubs(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["clubName"],
                    (string)sdr["address"],
                    (string)sdr["phone"],
                    (string)sdr["fax"],
                    (string)sdr["email"],
                    (string)sdr["website"],
                    (string)sdr["specialRequests"],
                    (string)sdr["clubInfo"],
                    (string)sdr["addressLat"],
                    (string)sdr["addressLong"],
                    (string)sdr["sortCountry"],
                    (string)sdr["sortState"],
                    (string)sdr["sortCity"]);
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