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
        BindGrid();
        menuOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
        string v = Request.QueryString["remove"];
        string r = Request.QueryString["restore"];
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
                    cmd.CommandText = "UPDATE [bcMenu] SET [deleted]='1', orderVal='0' WHERE [id]='" + v.ToUpper() + "'";
                    int rowsAffected = cmd.ExecuteNonQuery();
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

                    BindGrid();
                    menuOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        if (r != null)
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
                    cmd.CommandText = "UPDATE [bcMenu] SET [deleted]='0' WHERE [id]='" + r.ToUpper() + "'";
                    int rowsAffected = cmd.ExecuteNonQuery();
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

                BindGrid();
                menuOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (!Page.IsPostBack)
            {
                BindGrid();
                menuOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        StringBuilder cMenuTable = new StringBuilder();
        cMenuTable.AppendLine("<table>");

        cMenuTable.AppendLine("<thead>");
        cMenuTable.AppendLine("<tr>");
        cMenuTable.AppendLine("<th style=\"width: 5%;\"></th>");
        cMenuTable.AppendLine("<th>Menu Item</th>");
        cMenuTable.AppendLine("<th style=\"width: 10%;\"></th> ");
        cMenuTable.AppendLine("</tr>");
        cMenuTable.AppendLine("</thead>");

        cMenuTable.AppendLine("<tbody>");

        List<bcMenu> data = GetData();
        foreach (bcMenu m in data)
        {
            cMenuTable.AppendLine("<tr>");
            cMenuTable.AppendLine("<td> </td>");
            cMenuTable.AppendLine("<td>" + m.menuItem + "</td>");
            cMenuTable.AppendLine("<td><a href=\"?restore=" + m.id + "\">restore</a></td>");
            cMenuTable.AppendLine("</tr>");
        }

        cMenuTable.AppendLine("</tbody>");

        cMenuTable.AppendLine("</table>");

        currentMenusTable.Text = cMenuTable.ToString();
    }

    public void BindGrid()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connString);

        List<bcMenu> data = new List<bcMenu>();
        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcMenu] WHERE [deleted]='0' ORDER BY orderVal", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcMenu obj = new bcMenu(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["menuItem"],
                    (double)sdr["orderVal"]);
                data.Add(obj);
            }

            conn.Close();
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
            menuOrder.DataSource = data;
            menuOrder.DataBind();

            if (conn != null)
            {
                //cleanup connection i.e close 
                conn.Close();
            }
        }
    }

    public List<bcMenu> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcMenu> data = new List<bcMenu>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcMenu] WHERE [deleted]='1' ORDER BY menuItem", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                bcMenu obj = new bcMenu(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["menuItem"],
                    (double)sdr["orderVal"]);
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

    protected void saveOrder(object sender, EventArgs e)
    {
        string[] id = Request.Form.GetValues("orderID");
        int orderVal = 1;

        foreach (string menuItem in id)
        {
            this.updateOrder(menuItem, orderVal);
            orderVal += 1;
        }

        Response.Redirect(Request.Url.AbsoluteUri);
    }

    private void updateOrder(string id, int orderVal)
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

                cmd.CommandText = "UPDATE [bcMenu] SET orderVal = @orderVal WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@orderVal", orderVal);

                cmd.ExecuteNonQuery();
                
                int rowsAffected = cmd.ExecuteNonQuery();
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
}