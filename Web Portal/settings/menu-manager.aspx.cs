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

public partial class settings_menu_manager : System.Web.UI.Page
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
                    cmd.CommandText = "UPDATE [menus] SET [isActive]='0' WHERE [guid]='" + v.ToUpper() + "'";
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

        StringBuilder cMenuTable = new StringBuilder();
        cMenuTable.AppendLine("<table>");

        cMenuTable.AppendLine("<thead>");
        cMenuTable.AppendLine("<tr>");
        cMenuTable.AppendLine("<th style=\"width: 5%;\"></th>");
        cMenuTable.AppendLine("<th style=\"width: 40%;\"> Name</th>");
        cMenuTable.AppendLine("<th style=\"width: 35%;\"> Menu ID</th>");
        cMenuTable.AppendLine("<th style=\"width: 25%;\"></th> ");
        cMenuTable.AppendLine("</tr>");
        cMenuTable.AppendLine("</thead>");

        cMenuTable.AppendLine("<tbody>");

        int num = 1;

        List<Menus> data = GetData();
        foreach (Menus m in data)
        {
            cMenuTable.AppendLine("<tr>");
            cMenuTable.AppendLine("<td>" + num + "</td>");
            cMenuTable.AppendLine("<td>" + m.name + "</td>");
            cMenuTable.AppendLine("<td>" + m.menuId.ToString() + "</td>");
            cMenuTable.AppendLine("<td><a href=\"add/add-menu.aspx?edit=" + m.guid + "\">edit</a> / <a href=\"?remove=" + m.guid + "\">remove</a></td>");
            cMenuTable.AppendLine("</tr>");

            num++;
        }

        cMenuTable.AppendLine("</tbody>");

        cMenuTable.AppendLine("</table>");

        currentMenusTable.Text = cMenuTable.ToString();
    }

    public List<Menus> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<Menus> data = new List<Menus>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [menus] WHERE [isActive]='1' ORDER BY menuId", conn);
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
                    (string)sdr["menuItem1"],
                    (string)sdr["menuItem1Link"],
                    (string)sdr["menuItem1Icon"],
                    (string)sdr["menuItem2"],
                    (string)sdr["menuItem2Link"],
                    (string)sdr["menuItem2Icon"],
                    (string)sdr["menuItem3"],
                    (string)sdr["menuItem3Link"],
                    (string)sdr["menuItem3Icon"],
                    (string)sdr["menuItem4"],
                    (string)sdr["menuItem4Link"],
                    (string)sdr["menuItem4Icon"],
                    (string)sdr["menuItem5"],
                    (string)sdr["menuItem5Link"],
                    (string)sdr["menuItem5Icon"],
                    (string)sdr["menuItem6"],
                    (string)sdr["menuItem6Link"],
                    (string)sdr["menuItem6Icon"],
                    (string)sdr["menuItem7"],
                    (string)sdr["menuItem7Link"],
                    (string)sdr["menuItem7Icon"],
                    (string)sdr["menuItem8"],
                    (string)sdr["menuItem8Link"],
                    (string)sdr["menuItem8Icon"],
                    (string)sdr["menuItem9"],
                    (string)sdr["menuItem9Link"],
                    (string)sdr["menuItem9Icon"]);
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