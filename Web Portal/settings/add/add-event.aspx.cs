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

public partial class settings_add_add_event : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        eventSubject.Attributes.Add("placeholder", "Event name");
        eventLocation.Attributes.Add("placeholder", "Location of event");
        eventInstructor.Attributes.Add("placeholder", "Instructor name");
        eventPrice.Attributes.Add("placeholder", "Event price (without currency symbols)");
        eventDecription.Attributes.Add("placeholder", "Description of event");
        eventFlier.Attributes.Add("placeholder", "Flier URL location");

        StringBuilder eventH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            eventH1.AppendLine("<h1 class=\"major\">Update Event</h1>");
            pageH1.Text = eventH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadEventInfo(v);
            }
        }
        else
        {
            eventH1.AppendLine("<h1 class=\"major\">Add Event</h1>");
            pageH1.Text = eventH1.ToString();
            finalGuid = Guid.NewGuid();
        }
    }

    protected void LoadEventInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [bcEvents] WHERE [id]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                eventSubject.Text = (string)sdr["name"];
                bool eAllDay = (bool)sdr["allDay"];
                if (eAllDay == true)
                {
                    eventAllDay.Checked = true;
                } else { eventAllDay.Checked = false; }
                string eStartTime = (string)sdr["startTime"];
                DateTime startParse = DateTime.ParseExact(eStartTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
                eventStartDate.Text = startParse.ToString("yyyy-MM-dd");
                eventStartTime.Text = startParse.ToString("HH:mm");
                string eEndTime = (string)sdr["endTime"];
                DateTime endParse = DateTime.ParseExact(eEndTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
                eventEndDate.Text = endParse.ToString("yyyy-MM-dd");
                eventEndTime.Text = endParse.ToString("HH:mm");
                eventLocation.Text = (string)sdr["location"];
                string eDepartment = (string)sdr["department"];
                if (eDepartment == "Aquatics")
                {
                    eventDepartment.SelectedIndex = 1;
                }
                else if (eDepartment == "Fitness")
                {
                    eventDepartment.SelectedIndex = 2;
                }
                else if (eDepartment == "Food & Beverage")
                {
                    eventDepartment.SelectedIndex = 3;
                }
                else if (eDepartment == "Member Events")
                {
                    eventDepartment.SelectedIndex = 4;
                }
                else if (eDepartment == "Recreation")
                {
                    eventDepartment.SelectedIndex = 5;
                }
                else if (eDepartment == "Tennis")
                {
                    eventDepartment.SelectedIndex = 6;
                }
                eventInstructor.Text = (string)sdr["instructor"];
                eventDecription.Text = (string)sdr["description"];
                eventFlier.Text = (string)sdr["flier"];
                eventPrice.Text = (string)sdr["price"];
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
        string dtFormatStart = dtStartYear + ", " + dtStartMonth + ", " + dtStartDay + ", " + dtStartHour + ", " + dtStartMinute + ", 0";
        DateTime orderTime = DateTime.ParseExact(dtFormatStart, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
        string dtFormatEnd = dtEndYear + ", " + dtEndMonth + ", " + dtEndDay + ", " + dtEndHour + ", " + dtEndMinute + ", 0";
        string instructor = eventInstructor.Text;
        string description = eventDecription.Text;
        string price = eventPrice.Text;
        string department;
        if (eventDepartment.Text == "1")
        {
            department = "Aquatics";
        }
        else if (eventDepartment.Text == "2")
        {
            department = "Fitness";
        }
        else if (eventDepartment.Text == "3")
        {
            department = "Food & Beverage";
        }
        else if (eventDepartment.Text == "4")
        {
            department = "Member Events";
        }
        else if (eventDepartment.Text == "5")
        {
            department = "Recreation";
        }
        else if (eventDepartment.Text == "6")
        {
            department = "Tennis";
        } else { department = "none"; }
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
                if (isUpdate == true)
                {
                    cmd.CommandText = "UPDATE [bcEvents] SET id = @id, name = @name, allDay = @allDay, orderTime = @order, startTime = @start, endTime = @end, location = @location, department = @department, instructor = @instructor, price = @price, description = @description, flier = @flier WHERE [id]='" + finalGuid.ToString() + "'";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@allDay", allDay);
                    cmd.Parameters.AddWithValue("@order", orderTime);
                    cmd.Parameters.AddWithValue("@start", dtFormatStart);
                    cmd.Parameters.AddWithValue("@end", dtFormatEnd);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@instructor", instructor);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@flier", flier);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [bcEvents](id, name, allDay, orderTime, startTime, endTime, location, department, instructor, price, description, flier, isApproved) Values (@id, @name, @allDay, @order, @start, @end, @location, @department, @instructor, @price, @description, @flier, @isApproved)";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@allDay", allDay);
                    cmd.Parameters.AddWithValue("@order", orderTime);
                    cmd.Parameters.AddWithValue("@start", dtFormatStart);
                    cmd.Parameters.AddWithValue("@end", dtFormatEnd);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@instructor", instructor);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@flier", flier);
                    cmd.Parameters.AddWithValue("@isApproved", 0);
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
                    Response.Redirect("~/settings/calendar-manager.aspx");
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