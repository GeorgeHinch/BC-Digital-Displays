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
        
    }

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        int activeBit = Convert.ToInt32(settingsMessageActive.Checked);
        int mType;
        if(settingsRadioSingle.Checked == true)
        {
            mType = 0;
        } else { mType = 1; }
        string oneLineText = settingsMessageOneline.Text;
        string multiLineText = settingsMessageMultiline.Text;
        Debug.WriteLine("Text: " + oneLineText + " |");

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
                cmd.Parameters.AddWithValue("@logo", "logoURL");
                cmd.Parameters.AddWithValue("@btype", "image");
                cmd.Parameters.AddWithValue("@back", "backgroundURL");
                cmd.Parameters.AddWithValue("@pass", "11111");
                cmd.Parameters.AddWithValue("@theme", "light");
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