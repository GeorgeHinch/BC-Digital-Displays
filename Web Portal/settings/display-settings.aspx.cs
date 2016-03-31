using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class display_settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        settingsLogo.Attributes.Add("placeholder", "Logo URL placeholder");
        settingsBgUrl.Attributes.Add("placeholder", "Logo URL placeholder");
        settingsPassword.Attributes.Add("placeholder", "Logo URL placeholder");
        settingsMessageOneline.Attributes.Add("placeholder", "Logo URL placeholder");
        settingsMessageMultiline.Attributes.Add("placeholder", "Logo URL placeholder");
    }

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        string logoURL = settingsLogo.Text;
        if (logoURL == "")
        {
            logoURL = settingsLogo.Attributes["placeholder"].ToString();
        }
        string bType;
        if (settingsRadioBgImg.Checked == true)
        {
            bType = "image";
        } else { bType = "video"; }
        string bgURL = settingsBgUrl.Text;
        if (bgURL == "")
        {
            bgURL = settingsBgUrl.Attributes["placeholder"].ToString();
        }
        string pass = settingsPassword.Text;
        if (pass == "")
        {
            pass = settingsPassword.Attributes["placeholder"].ToString();
        }
        string tType;
        if (settingsRadioLight.Checked == true)
        {
            tType = "light";
        } else { tType = "dark"; }
        int activeBit = Convert.ToInt32(settingsMessageActive.Checked);
        int mType;
        if(settingsRadioSingle.Checked == true)
        {
            mType = 0;
        } else { mType = 1; }
        string oneLineText;
        if (mType == 0)
        {
            oneLineText = settingsMessageOneline.Text;
            if (oneLineText == "")
            {
                oneLineText = settingsMessageOneline.Attributes["placeholder"].ToString();
            }
        } else { oneLineText = null; }
        string multiLineText;
        if (mType == 1)
        {
            multiLineText = settingsMessageMultiline.Text;
            if (multiLineText == "")
            {
                multiLineText = settingsMessageMultiline.Attributes["placeholder"].ToString();
            }
        }
        else { multiLineText = null; }

        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;
        try
        {
            Debug.WriteLine("String: " + connString + " |");
            conn = new SqlConnection(connString);
            conn.Open();
            Debug.WriteLine("State: " + conn.State + " |");

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO settings(logoURL, backgroundType, backgroundURL, password, theme, welcomeActive, messageType, messageOne, messageMulti) Values (@logo, @btype, @back, @pass, @theme, @active, @mtype, @otext, @mtext)";
                cmd.Parameters.AddWithValue("@logo", logoURL);
                cmd.Parameters.AddWithValue("@btype", bType);
                cmd.Parameters.AddWithValue("@back", bgURL);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@theme", tType);
                cmd.Parameters.AddWithValue("@active", activeBit);
                cmd.Parameters.AddWithValue("@mtype", mType);
                cmd.Parameters.AddWithValue("@otext", oneLineText);
                cmd.Parameters.AddWithValue("@mtext", multiLineText);
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
}