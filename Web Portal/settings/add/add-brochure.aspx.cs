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
using Newtonsoft.Json;

public partial class settings_add_add_brochure : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        brochureName.Attributes.Add("placeholder", "Brochure name (i.e. Fall 2016)");

        StringBuilder brochureH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            brochureH1.AppendLine("<h1 class=\"major\">Update Brochure</h1>");
            pageH1.Text = brochureH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadBrochureInfo(v);
            }
        }
        else
        {
            brochureH1.AppendLine("<h1 class=\"major\">Add Brochure</h1>");
            pageH1.Text = brochureH1.ToString();
            finalGuid = Guid.NewGuid();
        }
    }

    protected void LoadBrochureInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [bcRecBrochure] WHERE [id]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                brochureName.Text = (string)sdr["name"];
                List<bcSessions> sessions = JsonConvert.DeserializeObject<List<bcSessions>>((string)sdr["sessions"]);

                foreach(bcSessions ses in sessions)
                {

                }
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
        string name = brochureName.Text;
        
        List<bcSessions> sessionsList = new List<bcSessions>();

        bcSessions session1 = new bcSessions(session1Name.Text, session1StartDate.Text, session1EndDate.Text);
        sessionsList.Add(session1);
        bcSessions session2 = new bcSessions(session2Name.Text, session2StartDate.Text, session2EndDate.Text);
        sessionsList.Add(session2);
        bcSessions session3 = new bcSessions(session3Name.Text, session3StartDate.Text, session3EndDate.Text);
        sessionsList.Add(session3);
        bcSessions session4 = new bcSessions(session4Name.Text, session4StartDate.Text, session4EndDate.Text);
        sessionsList.Add(session4);
        bcSessions session5 = new bcSessions(session5Name.Text, session5StartDate.Text, session5EndDate.Text);
        sessionsList.Add(session5);

        string sessions = JsonConvert.SerializeObject(sessionsList);


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
                    cmd.CommandText = "UPDATE [bcRecBrochure] SET  name='" + name + "', sessions='" + sessions + "' WHERE [id]='" + finalGuid.ToString() + "'";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [bcRecBrochure](id, name, sessions, isActive) Values (@id, @name, @sessions, @isActive)";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@isActive", false);
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
                    Response.Redirect("~/settings/youth-manager.aspx");
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