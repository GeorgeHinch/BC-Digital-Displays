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
    public string pageGuid = null;
    public Guid guid;
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
        

        StringBuilder trainerH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        if (v != null)
        {
            pageGuid = v;
            isUpdate = true;
            trainerH1.AppendLine("<h1 class=\"major\">Update Trainer</h1>");
            pageH1.Text = trainerH1.ToString();
            SaveForm.Text = "Update";
            LoadTrainerInfo(v);
        }
        else
        {
            trainerH1.AppendLine("<h1 class=\"major\">Add Trainer</h1>");
            pageH1.Text = trainerH1.ToString();
        }
    }

    protected void LoadTrainerInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [trainers] WHERE [guid]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                string tName = (string)sdr["name"];
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
                trainerPhoto.Text = (string)sdr["photo"];
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
        if (pageGuid == null)
        {
            guid = Guid.NewGuid();
        }
        else { guid = Guid.Parse(pageGuid); Debug.WriteLine("Parsed GUID: " + guid + " |"); }
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
            conn = new SqlConnection(connString);
            conn.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if(isUpdate == true)
                {
                    cmd.CommandText = "UPDATE [trainers] SET isActive='1', guid='" + guid + "', date='" + DateTime.UtcNow + "', name='" + name + "', degree='" + degree + "', years='" + years + "', yearsBC='" + yearsBC + "', expertise='" + expertise + "', reward='" + reward + "', session='" + session + "', accomplishment='" + accomplishment + "', photo='" + photo + "' WHERE [guid]='" + pageGuid + "'";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [trainers](isActive, guid, date, name, degree, years, yearsBC, expertise, reward, session, accomplishment, photo) Values (@isActive, @guid, @date, @name, @degree, @years, @yearsBC, @expertise, @reward, @session, @accomp, @photo)";
                    cmd.Parameters.AddWithValue("@isActive", 1);
                    cmd.Parameters.AddWithValue("@guid", guid);
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