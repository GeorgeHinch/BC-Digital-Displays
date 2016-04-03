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

public partial class settings_add_add_event : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        eventSubject.Attributes.Add("placeholder", "Event name");
        eventLocation.Attributes.Add("placeholder", "Location of event");
        eventInstructor.Attributes.Add("placeholder", "Instructor name");
        eventPrice.Attributes.Add("placeholder", "Event price (without currency symbols)");
        eventDecription.Attributes.Add("placeholder", "Description of event");
        eventFlier.Attributes.Add("placeholder", "Flier URL location");
    }

    protected void FormSubmit_Click(object sender, EventArgs e)
    {
        Guid guid = Guid.NewGuid();
        string name = eventSubject.Text;
        string location = eventLocation.Text;
        int allDay = Convert.ToInt32(eventAllDay.Checked);
        DateTime dayStart = Convert.ToDateTime(eventStartDate.Text);
        DateTime hourStart = Convert.ToDateTime(eventStartTime.Text);
        DateTime dayEnd = Convert.ToDateTime(eventEndDate.Text);
        DateTime hourEnd = Convert.ToDateTime(eventEndTime.Text);
        string dtStartYear = dayStart.Year.ToString();
        string dtStartMonth = dayStart.Month.ToString();
        string dtStartDay = dayStart.Day.ToString();
        string dtStartHour = hourStart.Hour.ToString();
        string dtStartMinute = hourStart.Minute.ToString();
        string dtEndYear = dayEnd.Year.ToString();
        string dtEndMonth = dayEnd.Month.ToString();
        string dtEndDay = dayEnd.Day.ToString();
        string dtEndHour = hourEnd.Hour.ToString();
        string dtEndMinute = hourEnd.Minute.ToString();
        string dtFormatStart = dtStartYear + ",  " + dtStartMonth + ",  " + dtStartDay + ",  " + dtStartHour + ",  " + dtStartMinute + ",  0";
        string dtFormatEnd = dtEndYear + ",  " + dtEndMonth + ",  " + dtEndDay + ",  " + dtEndHour + ",  " + dtEndMinute + ",  0";
        string instructor = eventInstructor.Text;
        string department = eventDepartment.Text;
        string price = eventPrice.Text;
        string description;
        if (eventDecription.Text == "1")
        {
            description = "Aquatics";
        }
        if (eventDecription.Text == "2")
        {
            description = "Fitness";
        }
        if (eventDecription.Text == "3")
        {
            description = "Food & Beverage";
        }
        if (eventDecription.Text == "4")
        {
            description = "Member Events";
        }
        if (eventDecription.Text == "5")
        {
            description = "Recreation";
        }
        if (eventDecription.Text == "6")
        {
            description = "Tennis";
        } else { description = "none"; }
        string flier = eventFlier.Text;

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
                cmd.CommandText = "INSERT INTO [events](isActive, guid, created, name, allDay, startTime, endTime, location, department, instructor, price, description, flier) Values (@isActive, @guid, @created, @name, @allDay, @start, @end, @location, @department, @instructor, @price, @description, @flier)";
                cmd.Parameters.AddWithValue("@isActive", 1);
                cmd.Parameters.AddWithValue("@guid", guid);
                cmd.Parameters.AddWithValue("@created", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@allDay", allDay);
                cmd.Parameters.AddWithValue("@start", dtFormatStart);
                cmd.Parameters.AddWithValue("@end", dtFormatEnd);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@instructor", instructor);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@flier", flier);
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

            if (c.GetType() == typeof(CheckBox))
            {
                CheckBox cb = (CheckBox)c;
                cb.Checked = false;
            }

            if (c.GetType() == typeof(DropDownList))
            {
                DropDownList d = (DropDownList)c;
                d.SelectedIndex = 0;
            }

            if (c.Controls.Count > 0)
            {
                ClearForm(c.Controls);
            }
        }
    }
}