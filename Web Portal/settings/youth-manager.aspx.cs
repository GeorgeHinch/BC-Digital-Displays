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

public partial class settings_youth_manager : System.Web.UI.Page
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


        StringBuilder brochureHtmlTable = new StringBuilder();
        brochureHtmlTable.AppendLine("<table>");

        brochureHtmlTable.AppendLine("<thead>");
        brochureHtmlTable.AppendLine("<tr>");
        brochureHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        brochureHtmlTable.AppendLine("<th style=\"width: 40%;\"> Name</th>");
        brochureHtmlTable.AppendLine("<th style=\"width: 25%;\"> Active</th>");
        brochureHtmlTable.AppendLine("<th style=\"width: 35%;\"></th> ");
        brochureHtmlTable.AppendLine("</tr>");
        brochureHtmlTable.AppendLine("</thead>");

        brochureHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List <bcRecBrochure> data = GetData();
        foreach (bcRecBrochure t in data)
        {
            brochureHtmlTable.AppendLine("<tr>");
            brochureHtmlTable.AppendLine("<td>"+ num + "</td>");
            brochureHtmlTable.AppendLine("<td><a href=\"class-manager.aspx?id=" + t.id + "\">" + t.name + "</a></td>");
            if (t.isActive == true)
            {
                brochureHtmlTable.AppendLine("<td>active</td>");
            }
            else { brochureHtmlTable.AppendLine("<td></td>"); }
            brochureHtmlTable.AppendLine("<td><a href=\"add/add-brochure.aspx?edit=" + t.id + "\">edit</a> / <a href=\"?active=" + t.id + "\">active</a> / <a href=\"?remove=" + t.id + "\">remove</a></td>");
            brochureHtmlTable.AppendLine("</tr>");

            num++;
        }

        brochureHtmlTable.AppendLine("</tbody>");

        brochureHtmlTable.AppendLine("</table>");

        brochureTable.Text = brochureHtmlTable.ToString();
    }

    public List<bcRecBrochure> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcRecBrochure> data = new List<bcRecBrochure>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcRecBrochure] WHERE [deleted]='0' ORDER BY updatedAt", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcRecBrochure obj = new bcRecBrochure(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["name"],
                    (string)sdr["sessions"],
                    (bool)sdr["isActive"]);
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