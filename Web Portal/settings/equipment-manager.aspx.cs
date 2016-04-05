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

public partial class settings_equipment_manager : System.Web.UI.Page
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
                    cmd.CommandText = "UPDATE [equipment] SET [isActive]='0' WHERE [guid]='" + v.ToUpper() + "'";
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

        StringBuilder studio1Table = new StringBuilder();
        StringBuilder studio2Table = new StringBuilder();
        StringBuilder studio3Table = new StringBuilder();
        StringBuilder studio4Table = new StringBuilder();

        studio1Table.AppendLine("<table>");
        studio2Table.AppendLine("<table>");
        studio3Table.AppendLine("<table>");
        studio4Table.AppendLine("<table>");

        studio1Table.AppendLine("<thead>");
        studio2Table.AppendLine("<thead>");
        studio3Table.AppendLine("<thead>");
        studio4Table.AppendLine("<thead>");

        studio1Table.AppendLine("<tr>");
        studio1Table.AppendLine("<th style=\"width: 5%;\"></th>");
        studio1Table.AppendLine("<th style=\"width: 85%;\"> Name</th>");
        studio1Table.AppendLine("<th style=\"width: 10%;\"></th>");
        studio1Table.AppendLine("</tr>");
        studio2Table.AppendLine("<tr>");
        studio2Table.AppendLine("<th style=\"width: 5%;\"></th>");
        studio2Table.AppendLine("<th style=\"width: 85%;\"> Name</th>");
        studio2Table.AppendLine("<th style=\"width: 10%;\"></th>");
        studio2Table.AppendLine("</tr>");
        studio3Table.AppendLine("<tr>");
        studio3Table.AppendLine("<th style=\"width: 5%;\"></th>");
        studio3Table.AppendLine("<th style=\"width: 85%;\"> Name</th>");
        studio3Table.AppendLine("<th style=\"width: 10%;\"></th>");
        studio3Table.AppendLine("</tr>");
        studio4Table.AppendLine("<tr>");
        studio4Table.AppendLine("<th style=\"width: 5%;\"></th>");
        studio4Table.AppendLine("<th style=\"width: 85%;\"> Name</th>");
        studio4Table.AppendLine("<th style=\"width: 10%;\"></th>");
        studio4Table.AppendLine("</tr>");

        studio1Table.AppendLine("</thead>");
        studio2Table.AppendLine("</thead>");
        studio3Table.AppendLine("</thead>");
        studio4Table.AppendLine("</thead>");

        studio1Table.AppendLine("<tbody>");
        studio2Table.AppendLine("<tbody>");
        studio3Table.AppendLine("<tbody>");
        studio4Table.AppendLine("<tbody>");

        int num1 = 1;
        int num2 = 1;
        int num3 = 1;
        int num4 = 1;

        List<Equipment> data = GetData();
        foreach (Equipment t in data)
        {
            if (t.studio == "1")
            {
                studio1Table.AppendLine("<tr>");
                studio1Table.AppendLine("<td>" + num1 + "</td>");
                studio1Table.AppendLine("<td>" + t.name.Trim() + "</td>");
                studio1Table.AppendLine("<td><a href=\"?remove=" + t.guid + "\">remove</a></td>");
                studio1Table.AppendLine("</tr>");

                num1++;
            }
            else if (t.studio == "2")
            {
                studio2Table.AppendLine("<tr>");
                studio2Table.AppendLine("<td>" + num2 + "</td>");
                studio2Table.AppendLine("<td>" + t.name.Trim() + "</td>");
                studio2Table.AppendLine("<td><a href=\"?remove=" + t.guid + "\">remove</a></td>");
                studio2Table.AppendLine("</tr>");

                num2++;
            }
            else if (t.studio == "3")
            {
                studio3Table.AppendLine("<tr>");
                studio3Table.AppendLine("<td>" + num3 + "</td>");
                studio3Table.AppendLine("<td>" + t.name.Trim() + "</td>");
                studio3Table.AppendLine("<td><a href=\"?remove=" + t.guid + "\">remove</a></td>");
                studio3Table.AppendLine("</tr>");

                num3++;
            }
            else if (t.studio == "4")
            {
                studio4Table.AppendLine("<tr>");
                studio4Table.AppendLine("<td>" + num4 + "</td>");
                studio4Table.AppendLine("<td>" + t.name.Trim() + "</td>");
                studio4Table.AppendLine("<td><a href=\"?remove=" + t.guid + "\">remove</a></td>");
                studio4Table.AppendLine("</tr>");

                num4++;
            }
        }

        studio1Table.AppendLine("</tbody>");
        studio2Table.AppendLine("</tbody>");
        studio3Table.AppendLine("</tbody>");
        studio4Table.AppendLine("</tbody>");

        studio1Table.AppendLine("</table>");
        studio2Table.AppendLine("</table>");
        studio3Table.AppendLine("</table>");
        studio4Table.AppendLine("</table>");

        studio1HTMLTable.Text = studio1Table.ToString();
        studio2HTMLTable.Text = studio2Table.ToString();
        studio3HTMLTable.Text = studio3Table.ToString();
        studio4HTMLTable.Text = studio4Table.ToString();
    }

    public List<Equipment> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<Equipment> data = new List<Equipment>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [equipment] WHERE [isActive]='1' ORDER BY studio", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                Equipment obj = new Equipment(
                    (bool)sdr["isActive"],
                    (Guid)sdr["guid"],
                    (DateTime)sdr["lastModified"],
                    (string)sdr["studio"],
                    (string)sdr["name"]);
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

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        Guid finalGuid = Guid.NewGuid();
        string eStudio;
        string eName;
        Button btn = (Button)sender;
        if (btn.ID == "SaveForm1")
        {
            eStudio = "1";
            eName = studio1Tb.Text;
        }
        else if (btn.ID == "SaveForm2")
        {
            eStudio = "2";
            eName = studio2Tb.Text;
        }
        else if (btn.ID == "SaveForm3")
        {
            eStudio = "3";
            eName = studio3Tb.Text;
        }
        else if (btn.ID == "SaveForm4")
        {
            eStudio = "4";
            eName = studio4Tb.Text;
        } else { eStudio = "0"; eName = ""; }

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
                cmd.CommandText = "INSERT INTO [equipment](isActive, guid, lastModified, studio, name) Values (@isActive, @guid, @created, @studio, @name)";
                cmd.Parameters.AddWithValue("@isActive", 1);
                cmd.Parameters.AddWithValue("@guid", finalGuid);
                cmd.Parameters.AddWithValue("@created", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@studio", eStudio);
                cmd.Parameters.AddWithValue("@name", eName);

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
                ClearForm(Page.Form.Controls);
                Response.Redirect("~/settings/equipment-manager.aspx");
            }
        }
    }

    public void ClearForm(ControlCollection controls)
    {
        foreach (Control c in controls)
        {
            if (c.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)c;
                t.Text = String.Empty;
            }

            if (c.Controls.Count > 0)
            {
                ClearForm(c.Controls);
            }
        }
    }
}