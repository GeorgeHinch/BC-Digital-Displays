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

public partial class settings_class_manager : System.Web.UI.Page
{
    public string bId;
    protected void Page_Load(object sender, EventArgs e)
    {
        bId = Request.QueryString["id"];
        string v = Request.QueryString["remove"];

        #region Remove a class
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
                    cmd.CommandText = "UPDATE [bcRecClasses] SET [deleted]='1' WHERE [id]='" + v.ToUpper() + "'";
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

        StringBuilder classHtmlTable = new StringBuilder();
        classHtmlTable.AppendLine("<table>");

        classHtmlTable.AppendLine("<thead>");
        classHtmlTable.AppendLine("<tr>");
        classHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        classHtmlTable.AppendLine("<th style=\"width: 40%;\"> Name</th>");
        classHtmlTable.AppendLine("<th style=\"width: 35%;\"> Category</th>");
        classHtmlTable.AppendLine("<th style=\"width: 25%;\"></th> ");
        classHtmlTable.AppendLine("</tr>");
        classHtmlTable.AppendLine("</thead>");

        classHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List <bcRecClasses> data = GetData();
        foreach (bcRecClasses t in data)
        {
            classHtmlTable.AppendLine("<tr>");
            classHtmlTable.AppendLine("<td>"+ num + "</td>");
            classHtmlTable.AppendLine("<td>" + t.name + "</td>");
            classHtmlTable.AppendLine("<td>" + t.category + "</td>");
            classHtmlTable.AppendLine("<td><a href=\"add/add-class.aspx?id=" + bId + "&edit=" + t.id + "\">edit</a> / <a href=\"?id=" + bId + "&remove=" + t.id + "\">remove</a></td>");
            classHtmlTable.AppendLine("</tr>");

            num++;
        }

        classHtmlTable.AppendLine("</tbody>");

        classHtmlTable.AppendLine("</table>");

        classTable.Text = classHtmlTable.ToString();
    }

    public List<bcRecClasses> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcRecClasses> data = new List<bcRecClasses>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcRecClasses] WHERE ([brochureId]='" + bId + "' AND [deleted]='0') ORDER BY name", conn);
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