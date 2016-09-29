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

public partial class settings_add_add_notification : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        notificationSubject.Attributes.Add("placeholder", "Notification subject");;
        notificationMessage.Attributes.Add("placeholder", "Notification details");

        StringBuilder eventH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            eventH1.AppendLine("<h1 class=\"major\">Update Notification</h1>");
            pageH1.Text = eventH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadNotificationInfo(v);
            }
        }
        else
        {
            eventH1.AppendLine("<h1 class=\"major\">Add Notification</h1>");
            pageH1.Text = eventH1.ToString();
            finalGuid = Guid.NewGuid();
        }
    }

    protected void LoadNotificationInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [bcNotification] WHERE [id]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                notificationSubject.Text = (string)sdr["subject"];
                DateTimeOffset eStartDay = (DateTimeOffset)sdr["startDate"];
                notificationStartDate.Text = eStartDay.ToString("yyyy-MM-dd");
                DateTimeOffset eEndDay = (DateTimeOffset)sdr["endDate"];
                notificationEndDate.Text = eEndDay.ToString("yyyy-MM-dd");
                notificationMessage.Text = (string)sdr["message"];
                notificationGlyph.SelectedValue = (string)sdr["glyph"];
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
        string glyph = notificationGlyph.SelectedValue;
        string subject = notificationSubject.Text;
        DateTime dayStart = Convert.ToDateTime(notificationStartDate.Text);
        DateTime dayEnd = Convert.ToDateTime(notificationEndDate.Text);
        string message = notificationMessage.Text;

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
                    cmd.CommandText = "UPDATE [bcNotification] SET id = @id, glyph = @glyph, subject = @subject, message = @message, startDate = @startDate, endDate = @endDate WHERE [id]='" + finalGuid.ToString() + "'";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@glyph", glyph);
                    cmd.Parameters.AddWithValue("@subject", subject);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters.AddWithValue("@startDate", dayStart);
                    cmd.Parameters.AddWithValue("@endDate", dayEnd);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [bcNotification](id, glyph, subject, message, startDate, endDate) Values (@id, @glyph, @subject, @message, @startDate, @endDate)";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@glyph", glyph);
                    cmd.Parameters.AddWithValue("@subject", subject);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters.AddWithValue("@startDate", dayStart);
                    cmd.Parameters.AddWithValue("@endDate", dayEnd);
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
                    Response.Redirect("~/settings/Notification-manager.aspx");
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