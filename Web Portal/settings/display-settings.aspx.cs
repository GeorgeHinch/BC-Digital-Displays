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
        SaveForm.PostBackUrl = "?save=true";

        string s = Request.QueryString["save"];
        if (s != "true")
        {
            LoadLastSettings();
        }
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
        int messageType;
        if(settingsRadioSingle.Checked == true)
        {
            messageType = 0;
        } else { messageType = 1; }
        string oneLineText;
        if (messageType == 0)
        {
            if (settingsMessageOneline.Text == "")
            {
                oneLineText = settingsMessageOneline.Attributes["placeholder"].ToString();
            }
            else { oneLineText = settingsMessageOneline.Text; }
        } else { oneLineText = ""; }
        string multiLineText;
        if (messageType == 1)
        {
            if (settingsMessageMultiline.Text == "")
            {
                multiLineText = settingsMessageMultiline.Attributes["placeholder"].ToString();
            }
            else { multiLineText = settingsMessageMultiline.Text; }
        }
        else { multiLineText = ""; }

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
                cmd.CommandText = "INSERT INTO [display-settings](date, ticks, logoURL, backgroundType, backgroundURL, password, theme, welcomeActive, messageType, messageOne, messageMulti) Values (@date, @ticks, @logo, @btype, @back, @pass, @theme, @active, @mtype, @otext, @mtext)";
                cmd.Parameters.AddWithValue("@date", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@ticks", DateTime.UtcNow.Ticks);
                cmd.Parameters.AddWithValue("@logo", logoURL);
                cmd.Parameters.AddWithValue("@btype", bType);
                cmd.Parameters.AddWithValue("@back", bgURL);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@theme", tType);
                cmd.Parameters.AddWithValue("@active", activeBit);
                cmd.Parameters.AddWithValue("@mtype", messageType);
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
                ClearForm(Page.Form.Controls);
                //LoadLastSettings();
                Response.Redirect("~/settings/display-settings.aspx");
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

            if (c.GetType() == typeof(CheckBox)){
                CheckBox cb = (CheckBox)c;
                cb.Checked = false;
            }

            if (c.GetType() == typeof(RadioButton))
            {
                RadioButton rb = (RadioButton)c;
                rb.Checked = false;
            }

            if (c.Controls.Count > 0)
            {
                ClearForm(c.Controls);
            }
        }
    }

    public void LoadLastSettings()
    {
        string logoURL;
        string backgroundType;
        string backgroundURL;
        string password;
        string theme;
        bool welcomeActive;
        bool messageType;
        string messageOne;
        string messageMulti;

        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;
        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [display-settings] WHERE DATE IN (SELECT MAX(DATE) FROM [display-settings])", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                logoURL = (string)sdr["logoURL"];
                backgroundType = (string)sdr["backgroundType"];
                backgroundURL = (string)sdr["backgroundURL"];
                password = (string)sdr["password"];
                theme = (string)sdr["theme"];
                welcomeActive = Convert.ToBoolean(sdr["welcomeActive"]);
                messageType = Convert.ToBoolean(sdr["messageType"]);
                messageOne = (string)sdr["messageOne"];
                messageMulti = (string)sdr["messageMulti"];

                settingsLogo.Attributes.Add("placeholder", logoURL);
                settingsBgUrl.Attributes.Add("placeholder", backgroundURL);
                settingsPassword.Attributes.Add("placeholder", password);
                settingsMessageOneline.Attributes.Add("placeholder", messageOne);
                settingsMessageMultiline.Attributes.Add("placeholder", messageMulti);

                if (backgroundType == "image")
                {
                    settingsRadioBgImg.Checked = true;
                }
                else { settingsRadioBgVid.Checked = true; }

                if (theme == "light")
                {
                    settingsRadioLight.Checked = true;
                }
                else { settingsRadioDark.Checked = true; }

                if (welcomeActive == false)
                {
                    settingsMessageActive.Checked = false;
                }
                else { settingsMessageActive.Checked = true; }

                if (messageType == false)
                {
                    settingsRadioSingle.Checked = true;
                }
                else { settingsRadioMulti.Checked = true; }
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