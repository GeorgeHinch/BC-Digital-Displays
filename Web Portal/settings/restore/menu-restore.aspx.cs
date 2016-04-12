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

public partial class settings_restore_menu_restore : System.Web.UI.Page
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
                    cmd.CommandText = "UPDATE [menus] SET [isActive]='1' WHERE [guid]='" + v.ToUpper() + "'";
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

        StringBuilder menuHtmlTable = new StringBuilder();
        menuHtmlTable.AppendLine("<table>");

        menuHtmlTable.AppendLine("<thead>");
        menuHtmlTable.AppendLine("<tr>");
        menuHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        menuHtmlTable.AppendLine("<th style=\"width: 40%;\"> Name</th>");
        menuHtmlTable.AppendLine("<th style=\"width: 35%;\"> Menu ID</th>");
        menuHtmlTable.AppendLine("<th style=\"width: 25%;\"></th> ");
        menuHtmlTable.AppendLine("</tr>");
        menuHtmlTable.AppendLine("</thead>");

        menuHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List<Menus> data = GetData();
        foreach (Menus m in data)
        {
            menuHtmlTable.AppendLine("<tr>");
            menuHtmlTable.AppendLine("<td>" + num + "</td>");
            menuHtmlTable.AppendLine("<td>" + m.name + "</td>");
            menuHtmlTable.AppendLine("<td>" + m.menuId.ToString() + "</td>");
            menuHtmlTable.AppendLine("<td><a href=\"../add/add-menu.aspx?edit=" + m.guid + "\">edit</a> / <a href=\"?restore=" + m.guid + "\">restore</a></td>");
            menuHtmlTable.AppendLine("</tr>");

            num++;
        }

        menuHtmlTable.AppendLine("</tbody>");

        menuHtmlTable.AppendLine("</table>");

        restoreMenusTable.Text = menuHtmlTable.ToString();
    }

    public List<Menus> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<Menus> data = new List<Menus>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [menus] WHERE [isActive]='0' ORDER BY name", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Menus obj = new Menus(
                    (bool)sdr["isActive"],
                    (Guid)sdr["guid"],
                    (DateTime)sdr["lastModified"],
                    (string)sdr["name"],
                    (int)sdr["menuId"],
                    (MenuItem)sdr["mItem1"],
                    (MenuItem)sdr["mItem2"],
                    (MenuItem)sdr["mItem3"],
                    (MenuItem)sdr["mItem4"],
                    (MenuItem)sdr["mItem5"],
                    (MenuItem)sdr["mItem6"],
                    (MenuItem)sdr["mItem7"],
                    (MenuItem)sdr["mItem8"],
                    (MenuItem)sdr["mItem9"]);
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