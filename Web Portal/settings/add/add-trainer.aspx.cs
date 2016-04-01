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

public partial class settings_add_add_trainer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        trainerName.Attributes.Add("placeholder", "Trainer name");
        trainerDegree.Attributes.Add("placeholder", "Trainer degree (i.e. BS, BA, etc.");
        trainerYears.Attributes.Add("placeholder", "Year trainer started working");
        trainerYearsBC.Attributes.Add("placeholder", "Year trainer started working at BC");
        trainerExpertise.Attributes.Add("placeholder", "Expertise");
        trainerReward.Attributes.Add("placeholder", "Reward");
        trainerSession.Attributes.Add("placeholder", "What to expect from a session");
        trainerAccomplishment.Attributes.Add("placeholder", "Trainer accomplishment");
        trainerPhoto.Attributes.Add("placeholder", "URL path to trainer photo");
    }

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        string name = trainerName.Text;
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
            Debug.WriteLine("String: " + connString + " |");
            conn = new SqlConnection(connString);
            conn.Open();
            Debug.WriteLine("State: " + conn.State + " |");

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO [trainers](isActive, date, name, degree, years, yearsBC, expertise, reward, session, accomplishment, photo) Values (@isActive, @date, @name, @degree, @years, @yearsBC, @expertise, @reward, @session, @accomp, @photo)";
                cmd.Parameters.AddWithValue("@isActive", 1);
                cmd.Parameters.AddWithValue("@date", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@degree", degree);
                cmd.Parameters.AddWithValue("@years", years);
                cmd.Parameters.AddWithValue("@yearsBC", yearsBC);
                cmd.Parameters.AddWithValue("@expertise", expertise);
                cmd.Parameters.AddWithValue("@reward", reward);
                cmd.Parameters.AddWithValue("@session", session);
                cmd.Parameters.AddWithValue("@accomp", accomplishment);
                cmd.Parameters.AddWithValue("@photo", photo);
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

            if(c.Controls.Count > 0)
            {
                ClearForm(c.Controls);
            }
        }
    }
}