using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        bId = Request.QueryString["bid"];
        if(bId == null)
        {
            Response.Redirect("~/settings/youth-manager.aspx");
        }

        className.Attributes.Add("placeholder", "Class name");
        classAgeRange.Attributes.Add("placeholder", "i.e. \"Ages 3-6\"");
        classStartTime.Attributes.Add("placeholder", "i.e 5PM or 17:00");
        classEndTime.Attributes.Add("placeholder", "i.e 5PM or 17:00");

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
            SaveForm.PostBackUrl = "?bid=" + bId + "&edit=" + finalGuid + "&update=true";
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
                    if (dayString.Substring(0, 1) == "1") { classMonday.Checked = true; }
                    if (dayString.Substring(1, 1) == "1") { classTuesday.Checked = true; }
                    if (dayString.Substring(2, 1) == "1") { classWednesday.Checked = true; }
                    if (dayString.Substring(3, 1) == "1") { classThursday.Checked = true; }
                    if (dayString.Substring(4, 1) == "1") { classFriday.Checked = true; }
                    if (dayString.Substring(5, 1) == "1") { classSaturday.Checked = true; }
                    if (dayString.Substring(6, 1) == "1") { classSunday.Checked = true; }
                cTimes cTimes = JsonConvert.DeserializeObject<cTimes>((string)sdr["time"]);
                    classStartTime.Text = cTimes.cStartTime.ToString("HH:mm");
                    classEndTime.Text = cTimes.cEndTime.ToString("HH:mm");
                classLocation.Text = (string)sdr["location"];
                classCategory.Text = Convert.ToString((double)sdr["category"]);
                string sessionString = (string)sdr["sessions"];
                    if (sessionString.Substring(0, 1) == "1") { classSession1.Checked = true; }
                    if (sessionString.Substring(1, 1) == "1") { classSession2.Checked = true; }
                    if (sessionString.Substring(2, 1) == "1") { classSession3.Checked = true; }
                    if (sessionString.Substring(3, 1) == "1") { classSession4.Checked = true; }
                    if (sessionString.Substring(4, 1) == "1") { classSession5.Checked = true; }
                classDescription.Text = (string)sdr["description"];
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
        string ageRange = classAgeRange.Text;
        double ageMin = Convert.ToDouble(classAgeMin.Text);
        double ageMax = Convert.ToDouble(classAgeMax.Text);
        StringBuilder classDays = new StringBuilder();
            classDays.Append(Convert.ToInt16(classMonday.Checked).ToString());
            classDays.Append(Convert.ToInt16(classTuesday.Checked).ToString());
            classDays.Append(Convert.ToInt16(classWednesday.Checked).ToString());
            classDays.Append(Convert.ToInt16(classThursday.Checked).ToString());
            classDays.Append(Convert.ToInt16(classFriday.Checked).ToString());
            classDays.Append(Convert.ToInt16(classSaturday.Checked).ToString());
            classDays.Append(Convert.ToInt16(classSunday.Checked).ToString());
            string days = classDays.ToString();
        cTimes classTime = new cTimes();
            classTime.cStartTime = Convert.ToDateTime(classStartTime.Text.Replace(".", string.Empty));
            classTime.cEndTime = Convert.ToDateTime(classEndTime.Text.Replace(".", string.Empty));
            string time = JsonConvert.SerializeObject(classTime);
        string location = classLocation.Text;
        double category = Convert.ToDouble(classCategory.Text);
        StringBuilder classSessions = new StringBuilder();
            classSessions.Append(Convert.ToInt16(classSession1.Checked).ToString());
            classSessions.Append(Convert.ToInt16(classSession2.Checked).ToString());
            classSessions.Append(Convert.ToInt16(classSession3.Checked).ToString());
            classSessions.Append(Convert.ToInt16(classSession4.Checked).ToString());
            classSessions.Append(Convert.ToInt16(classSession5.Checked).ToString());
            string sessions = classSessions.ToString();
        string descriptionReg = classDescription.Text;
        string description = Regex.Replace(descriptionReg, @"\t|\n|\r", " ");



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
                    cmd.CommandText = "UPDATE [bcRecClasses] SET id = @id, name = @name, ageRange = @ageRange, ageMin = @ageMin, ageMax = @ageMax, days = @days, time = @time, location = @location, sessions = @sessions, description = @description, category = @category, brochureId = @brochureId WHERE [id]='" + finalGuid.ToString() + "'";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@ageRange", ageRange);
                    cmd.Parameters.AddWithValue("@ageMin", ageMin);
                    cmd.Parameters.AddWithValue("@ageMax", ageMax);
                    cmd.Parameters.AddWithValue("@days", days);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@brochureId", bId);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [bcRecClasses](id, name, ageRange, ageMin, ageMax, days, time, location, sessions, description, category, brochureId) Values (@id, @name, @ageRange, @ageMin, @ageMax, @days, @time, @location, @sessions, @description, @category, @brochureId)";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@ageRange", ageRange);
                    cmd.Parameters.AddWithValue("@ageMin", ageMin);
                    cmd.Parameters.AddWithValue("@ageMax", ageMax);
                    cmd.Parameters.AddWithValue("@days", days);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@brochureId", bId);
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