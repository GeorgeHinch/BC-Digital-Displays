using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class settings_add_add_club : System.Web.UI.Page
{
    public Guid finalGuid;
    public bool isUpdate = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        clubName.Attributes.Add("placeholder", "Club name");
        clubSpecialRequests.Attributes.Add("placeholder", "If a club has special visitation restrictions");
        clubInfo.Attributes.Add("placeholder", "Brief club history and features");

        StringBuilder eventH1 = new StringBuilder();

        string v = Request.QueryString["edit"];
        string u = Request.QueryString["update"];
        if (v != null)
        {
            finalGuid = Guid.Parse(v);
            isUpdate = true;
            eventH1.AppendLine("<h1 class=\"major\">Update Club</h1>");
            pageH1.Text = eventH1.ToString();
            SaveForm.Text = "Update";
            SaveForm.PostBackUrl = "?edit=" + finalGuid + "&update=true";
            if (u != "true")
            {
                LoadClubInfo(v);
            }
        }
        else
        {
            eventH1.AppendLine("<h1 class=\"major\">Add Club</h1>");
            pageH1.Text = eventH1.ToString();
            finalGuid = Guid.NewGuid();
        }
    }

    protected void LoadClubInfo(string v)
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("Select * FROM [bcReciprocalClubs] WHERE [id]='" + v.ToUpper() + "'", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                clubName.Text = (string)sdr["clubName"];
                clubAddress.Text = (string)sdr["address"];
                clubPhone.Text = (string)sdr["phone"];
                clubFax.Text = (string)sdr["fax"];
                clubEmail.Text = (string)sdr["email"];
                clubWebsite.Text = (string)sdr["website"];
                clubSpecialRequests.Text = (string)sdr["specialRequests"];
                clubInfo.Text = (string)sdr["clubInfo"];
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
        var latLong = GeoBuilder.GetGeoCodedResults(clubAddress.Text);
        
        string countryCode = "";
        string city = "";
        string state = "";
        string country = "";

        foreach(GeoBuilder.AddressComponent aComp in latLong.results[0].address_components)
        {
            if (aComp.types[0] == "locality")
            {
                city = aComp.long_name;
            }

            if (aComp.types[0] == "administrative_area_level_1")
            {
                state = aComp.long_name;
            }

            if (aComp.types[0] == "country")
            {
                country = aComp.long_name;
                countryCode = aComp.short_name;
            }
        }

        if (city == "")
        {
            foreach (GeoBuilder.AddressComponent aComp in latLong.results[0].address_components)
            {
                if (aComp.types[0] == "neighborhood")
                {
                    city = aComp.long_name;
                }
            }
        }

        if (city == "")
        {
            if (state != "")
            {
                city = state;
            }
            else { city = country; }
        }

        string phone = PhoneBuilder.buildPhoneNumber(clubPhone.Text, countryCode);
        string fax = PhoneBuilder.buildPhoneNumber(clubFax.Text, countryCode);

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
                    cmd.CommandText = "UPDATE [bcReciprocalClubs] SET id = @id, clubName = @clubName, sortCity = @sortCity, sortState = @sortState, sortCountry = @sortCountry, address = @address, phone = @phone, fax = @fax, email = @email, website = @website, specialRequests = @specialRequests, clubInfo = @clubInfo, addressLat = @addressLat, addressLong = @addressLong WHERE [id]='" + finalGuid.ToString() + "'";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@clubName", clubName.Text);
                    cmd.Parameters.AddWithValue("@sortCity", city);
                    cmd.Parameters.AddWithValue("@sortState", state);
                    cmd.Parameters.AddWithValue("@sortCountry", country);
                    cmd.Parameters.AddWithValue("@address", latLong.results[0].formatted_address);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@fax", fax);
                    cmd.Parameters.AddWithValue("@email", clubEmail.Text);
                    cmd.Parameters.AddWithValue("@website", clubWebsite.Text);
                    cmd.Parameters.AddWithValue("@specialRequests", clubSpecialRequests.Text);
                    cmd.Parameters.AddWithValue("@clubInfo", clubInfo.Text);
                    cmd.Parameters.AddWithValue("@addressLat", latLong.results[0].geometry.location.lat);
                    cmd.Parameters.AddWithValue("@addressLong", latLong.results[0].geometry.location.lng);
                }
                else
                {
                    cmd.CommandText = "INSERT INTO [bcReciprocalClubs] (id, clubName, sortCity, sortState, sortCountry, address, phone, fax, email, website, specialRequests, clubInfo, addressLat, addressLong) VALUES (@id, @clubName, @sortCity, @sortState, @sortCountry, @address, @phone, @fax, @email, @website, @specialRequests, @clubInfo, @addressLat, @addressLong)";
                    cmd.Parameters.AddWithValue("@id", finalGuid.ToString());
                    cmd.Parameters.AddWithValue("@clubName", clubName.Text);
                    cmd.Parameters.AddWithValue("@sortCity", city);
                    cmd.Parameters.AddWithValue("@sortState", state);
                    cmd.Parameters.AddWithValue("@sortCountry", country);
                    cmd.Parameters.AddWithValue("@address", latLong.results[0].formatted_address);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@fax", fax);
                    cmd.Parameters.AddWithValue("@email", clubEmail.Text);
                    cmd.Parameters.AddWithValue("@website", clubWebsite.Text);
                    cmd.Parameters.AddWithValue("@specialRequests", clubSpecialRequests.Text);
                    cmd.Parameters.AddWithValue("@clubInfo", clubInfo.Text);
                    cmd.Parameters.AddWithValue("@addressLat", latLong.results[0].geometry.location.lat);
                    cmd.Parameters.AddWithValue("@addressLong", latLong.results[0].geometry.location.lng);
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
                    Response.Redirect("~/settings/reciprocal-manager.aspx");
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