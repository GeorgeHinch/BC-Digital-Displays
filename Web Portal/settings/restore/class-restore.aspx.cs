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

public partial class settings_restore_class_restore : System.Web.UI.Page
{
    public string bId;
    protected void Page_Load(object sender, EventArgs e)
    {
        bId = Request.QueryString["id"];
        if (bId == null)
        {
            Response.Redirect("~/settings/youth-manager.aspx");
        }
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
                    cmd.CommandText = "UPDATE [bcRecClasses] SET [deleted]='0' WHERE [id]='" + v.ToUpper() + "'";
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

        StringBuilder classesHtmlTable = new StringBuilder();
        classesHtmlTable.AppendLine("<table>");

        classesHtmlTable.AppendLine("<thead>");
        classesHtmlTable.AppendLine("<tr>");
        classesHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        classesHtmlTable.AppendLine("<th style=\"width: 30%;\"> Name</th>");
        classesHtmlTable.AppendLine("<th style=\"width: 20%;\"> Days</th>");
        classesHtmlTable.AppendLine("<th style=\"width: 30%;\"> Category</th>");
        classesHtmlTable.AppendLine("<th style=\"width: 15%;\"></th> ");
        classesHtmlTable.AppendLine("</tr>");
        classesHtmlTable.AppendLine("</thead>");

        classesHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List<bcRecClasses> data = GetData();
        foreach (bcRecClasses t in data)
        {
            classesHtmlTable.AppendLine("<tr>");
            classesHtmlTable.AppendLine("<td>" + num + "</td>");
            classesHtmlTable.AppendLine("<td>" + t.name + "</td>");
            classesHtmlTable.AppendLine("<td>" + DayBuilder.dayBuilder(t.days) + "</td>");
            if (t.category == 1) { classesHtmlTable.AppendLine("<td>Family Events</td>"); }
            else if (t.category == 2) { classesHtmlTable.AppendLine("<td>School Breaks</td>"); }
            else if (t.category == 3) { classesHtmlTable.AppendLine("<td>Recreation</td>"); }
            else if (t.category == 4) { classesHtmlTable.AppendLine("<td>Tennis</td>"); }
            else if (t.category == 5) { classesHtmlTable.AppendLine("<td>Swim</td>"); }
            else if (t.category == 6) { classesHtmlTable.AppendLine("<td>Basketball</td>"); }
            else { classesHtmlTable.AppendLine("<td></td>"); }
            classesHtmlTable.AppendLine("<td><a href=\"../add/add-class.aspx?bid=" + bId + "&edit=" + t.id + "\">edit</a> / <a href=\"?id=" + bId + "&restore=" + t.id + "\">restore</a></td>");
            classesHtmlTable.AppendLine("</tr>");

            num++;
        }

        classesHtmlTable.AppendLine("</tbody>");
        classesHtmlTable.AppendLine("</table>");

        classesTable.Text = classesHtmlTable.ToString();

        StringBuilder returnLink = new StringBuilder();
        returnLink.AppendLine("<a href=\"../class-manager.aspx?id=" + bId + "\" class=\"button small icon fa-angle-left\">return to classes</a>");
        returnToClasses.Text = returnLink.ToString();
    }

    public List<bcRecClasses> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcRecClasses> data = new List<bcRecClasses>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcRecClasses] WHERE ([brochureId]='" + bId + "' AND [deleted]='1') ORDER BY name", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcRecClasses obj = new bcRecClasses(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["name"],
                    (string)sdr["ageRange"],
                    (double)sdr["ageMin"],
                    (double)sdr["ageMax"],
                    (string)sdr["days"],
                    (string)sdr["time"],
                    (string)sdr["location"],
                    (string)sdr["sessions"],
                    (string)sdr["description"],
                    (double)sdr["category"],
                    (string)sdr["brochureId"]);
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