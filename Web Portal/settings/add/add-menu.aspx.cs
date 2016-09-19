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

public partial class settings_add_add_menu : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*trainerName.Attributes.Add("placeholder", "Trainer name");
        trainerDegree.Attributes.Add("placeholder", "Trainer degree (i.e. BS, BA, etc.)");
        trainerYears.Attributes.Add("placeholder", "Year trainer started working");
        trainerYearsBC.Attributes.Add("placeholder", "Year trainer started working at BC");
        trainerExpertise.Attributes.Add("placeholder", "Expertise");
        trainerReward.Attributes.Add("placeholder", "What do you find most rewarding");
        trainerSession.Attributes.Add("placeholder", "What to expect from a session");
        trainerAccomplishment.Attributes.Add("placeholder", "Trainer accomplishment");
        trainerPhoto.Attributes.Add("placeholder", "URL path to trainer photo");*?*/

        StringBuilder trainerH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            //menuH1.AppendLine("<h1 class=\"major\">Update Menu</h1>");
            pageH1.Text = trainerH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadMenuInfo(v);
            }
        }
        else
        {
            //menuH1.AppendLine("<h1 class=\"major\">Add Menu</h1>");
            pageH1.Text = trainerH1.ToString();
            finalGuid = Guid.NewGuid();
        }
    }

    protected void LoadMenuInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [menus] WHERE [guid]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                /*string tName = (string)sdr["name"];
                trainerName.Text = tName.Trim();
                trainerDegree.Text = (string)sdr["degree"];
                int yS = (int)sdr["years"];
                trainerYears.Text = yS.ToString();
                int BCS = (int)sdr["yearsBC"];
                trainerYearsBC.Text = BCS.ToString();
                trainerExpertise.Text = (string)sdr["expertise"];
                trainerReward.Text = (string)sdr["reward"];
                trainerSession.Text = (string)sdr["session"];
                trainerAccomplishment.Text = (string)sdr["accomplishment"];
                trainerPhoto.Text = (string)sdr["photo"];/**/
            }

            conn.Close();
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

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        /*string name = trainerName.Text;
        string degree = trainerDegree.Text;
        int years = Convert.ToInt32(trainerYears.Text);
        int yearsBC = Convert.ToInt32(trainerYearsBC.Text);
        string expertise = trainerExpertise.Text;
        string reward = trainerReward.Text;
        string session = trainerSession.Text;
        string accomplishment = trainerAccomplishment.Text;
        string photo = trainerPhoto.Text;


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
                if (isUpdate == true)
                {
                    cmd.CommandText = "UPDATE [menus] SET isActive='1', guid='" + finalGuid + "', date='" + DateTime.UtcNow + "', name='" + name + "', degree='" + degree + "', years='" + years + "', yearsBC='" + yearsBC + "', expertise='" + expertise + "', reward='" + reward + "', session='" + session + "', accomplishment='" + accomplishment + "', photo='" + photo + "' WHERE [guid]='" + finalGuid + "'";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [menus](isActive, guid, lastModified, name, menuId, mItem1, mItem2, mItem3, mItem4, mItem5, mItem6, mItem7, mItem8, mItem9) Values (@isActive, @guid, @date, @name, @menuId, @mI1, @mI2, @mI3, @mI4, @mI5, @mI6, @mI7, @mI8, @mI9)";
                    cmd.Parameters.AddWithValue("@isActive", 1);
                    cmd.Parameters.AddWithValue("@guid", finalGuid);
                    cmd.Parameters.AddWithValue("@date", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@menuId", degree);
                    cmd.Parameters.AddWithValue("@mI1", years);
                    cmd.Parameters.AddWithValue("@mI2", yearsBC);
                    cmd.Parameters.AddWithValue("@mI3", expertise);
                    cmd.Parameters.AddWithValue("@mI4", reward);
                    cmd.Parameters.AddWithValue("@mI5", session);
                    cmd.Parameters.AddWithValue("@mI6", accomplishment);
                    cmd.Parameters.AddWithValue("@mI7", photo);
                    cmd.Parameters.AddWithValue("@mI8", photo);
                    cmd.Parameters.AddWithValue("@mI9", photo);
                }
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
                if (isUpdate == true)
                {
                    Response.Redirect("~/settings/menu-manager.aspx");
                }
            }
        }/**/
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