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

public partial class settings_add_add_class : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;
    public string bId;

    protected void Page_Load(object sender, EventArgs e)
    {
        string i = Request.QueryString["bid"];
        if(i != null)
        {
            bId = i;
        }

        className.Attributes.Add("placeholder", "Class name");
        classAgeRange.Attributes.Add("placeholder", "i.e. \"Ages 3-6\"");

        StringBuilder classH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            classH1.AppendLine("<h1 class=\"major\">Update Class</h1>");
            pageH1.Text = classH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadClassInfo(v);
            }
        }
        else
        {
            classH1.AppendLine("<h1 class=\"major\">Add Class</h1>");
            pageH1.Text = classH1.ToString();
            finalGuid = Guid.NewGuid();
        }

        StringBuilder returnLink = new StringBuilder();
        returnLink.AppendLine("<a href=\"../class-manager.aspx?id=" + bId + "\" class=\"button small icon fa-angle-left\">return to classes</a>");
        returnToClasses.Text = returnLink.ToString();
    }

    protected void LoadClassInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [bcRecClasses] WHERE [id]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                className.Text = (string)sdr["name"];
                classAgeRange.Text = (string)sdr["ageRange"];
                double aMin = (double)sdr["ageMin"];
                classAgeMin.Text = aMin.ToString();
                double BCS = (double)sdr["ageMax"];
                classAgeMax.Text = BCS.ToString();
                string dayString = (string)sdr["days"];
                // Do the things with the strings
                classTime.Text = (string)sdr["time"];
                classLocation.Text = (string)sdr["location"];
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
        string name = className.Text;
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
                    cmd.CommandText = "UPDATE [bcTrainers] SET deleted='0', id='" + finalGuid.ToString() + "', name='" + name + "', degree='" + degree + "', years='" + years + "', yearsBC='" + yearsBC + "', expertise='" + expertise + "', reward='" + reward + "', expectation='" + expectation + "', accomplishment='" + accomplishment + "', photo='" + photo + "', reflections='" + reflections + "' WHERE [id]='" + finalGuid.ToString() + "'";
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
                    Response.Redirect("~/settings/class-manager.aspx?id=" + bId);
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