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

public partial class settings_add_add_trainer : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        trainerName.Attributes.Add("placeholder", "Trainer name");
        trainerDegree.Attributes.Add("placeholder", "Trainer degree (i.e. BS, BA, etc.)");
        trainerYears.Attributes.Add("placeholder", "Year trainer started working");
        trainerYearsBC.Attributes.Add("placeholder", "Year trainer started working at BC");
        trainerExpertise.Attributes.Add("placeholder", "Expertise");
        trainerReward.Attributes.Add("placeholder", "What do you find most rewarding");
        trainerSession.Attributes.Add("placeholder", "What to expect from a session");
        trainerAccomplishment.Attributes.Add("placeholder", "Trainer accomplishment");
        trainerPhoto.Attributes.Add("placeholder", "URL path to trainer photo");
        trainerReflections.Attributes.Add("placeholder", "URL path to Reflections article");

        StringBuilder trainerH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            trainerH1.AppendLine("<h1 class=\"major\">Update Trainer</h1>");
            pageH1.Text = trainerH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadTrainerInfo(v);
            }
        }
        else
        {
            trainerH1.AppendLine("<h1 class=\"major\">Add Trainer</h1>");
            pageH1.Text = trainerH1.ToString();
            finalGuid = Guid.NewGuid();
        }
    }

    protected void LoadTrainerInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [bcTrainers] WHERE [id]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                string tName = (string)sdr["name"];
                trainerName.Text = tName.Trim();
                trainerDegree.Text = (string)sdr["degree"];
                double yS = (double)sdr["years"];
                trainerYears.Text = yS.ToString();
                double BCS = (double)sdr["yearsBC"];
                trainerYearsBC.Text = BCS.ToString();
                trainerExpertise.Text = (string)sdr["expertise"];
                trainerReward.Text = (string)sdr["reward"];
                trainerSession.Text = (string)sdr["expectation"];
                trainerAccomplishment.Text = (string)sdr["accomplishment"];
                trainerPhoto.Text = (string)sdr["photo"];
                trainerReflections.Text = (string)sdr["reflections"];
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
        string name = trainerName.Text;
        string degree = trainerDegree.Text;
        double years = Convert.ToDouble(trainerYears.Text);
        double yearsBC = Convert.ToDouble(trainerYearsBC.Text); 
        string expertise = trainerExpertise.Text;
        string reward = trainerReward.Text;
        string expectation = trainerSession.Text;
        string accomplishment = trainerAccomplishment.Text;
        string photo = trainerPhoto.Text;
        string reflections = trainerReflections.Text;


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
                if(isUpdate == true)
                {
                    cmd.CommandText = "UPDATE [bcTrainers] SET id = @id, name = @name, degree = @degree, years = @years, yearsBC = @yearsBC, expertise = @expertise, reward = @reward, expectation = @expectation, accomplishment = @accomp, photo = @photo, reflections = @reflections WHERE [id]='" + finalGuid.ToString() + "'";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@degree", degree);
                    cmd.Parameters.AddWithValue("@years", years);
                    cmd.Parameters.AddWithValue("@yearsBC", yearsBC);
                    cmd.Parameters.AddWithValue("@expertise", expertise);
                    cmd.Parameters.AddWithValue("@reward", reward);
                    cmd.Parameters.AddWithValue("@expectation", expectation);
                    cmd.Parameters.AddWithValue("@accomp", accomplishment);
                    cmd.Parameters.AddWithValue("@photo", photo);
                    cmd.Parameters.AddWithValue("@reflections", reflections);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [bcTrainers](id, name, degree, years, yearsBC, expertise, reward, expectation, accomplishment, photo, reflections) Values (@id, @name, @degree, @years, @yearsBC, @expertise, @reward, @expectation, @accomp, @photo, @reflections)";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@degree", degree);
                    cmd.Parameters.AddWithValue("@years", years);
                    cmd.Parameters.AddWithValue("@yearsBC", yearsBC);
                    cmd.Parameters.AddWithValue("@expertise", expertise);
                    cmd.Parameters.AddWithValue("@reward", reward);
                    cmd.Parameters.AddWithValue("@expectation", expectation);
                    cmd.Parameters.AddWithValue("@accomp", accomplishment);
                    cmd.Parameters.AddWithValue("@photo", photo);
                    cmd.Parameters.AddWithValue("@reflections", reflections);
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
            throw ex;
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
                    Response.Redirect("~/settings/trainer-manager.aspx");
                }
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