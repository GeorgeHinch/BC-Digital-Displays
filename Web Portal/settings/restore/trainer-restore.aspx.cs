﻿using System;
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

public partial class settings_restore_trainer_restore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string v = Request.QueryString["restore"];
        if (v != null)
        {
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
                    cmd.CommandText = "UPDATE [bcTrainers] SET [deleted]='0' WHERE [id]='" + v.ToUpper() + "'";
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
                }
            }
        }

        StringBuilder trainerHtmlTable = new StringBuilder();
        trainerHtmlTable.AppendLine("<table>");

        trainerHtmlTable.AppendLine("<thead>");
        trainerHtmlTable.AppendLine("<tr>");
        trainerHtmlTable.AppendLine("<th style=\"width: 5%;\"></th>");
        trainerHtmlTable.AppendLine("<th style=\"width: 40%;\"> Name</th>");
        trainerHtmlTable.AppendLine("<th style=\"width: 35%;\"> First Year</th>");
        trainerHtmlTable.AppendLine("<th style=\"width: 25%;\"></th> ");
        trainerHtmlTable.AppendLine("</tr>");
        trainerHtmlTable.AppendLine("</thead>");

        trainerHtmlTable.AppendLine("<tbody>");

        int num = 1;

        List<bcTrainers> data = GetData();
        foreach (bcTrainers t in data)
        {
            trainerHtmlTable.AppendLine("<tr>");
            trainerHtmlTable.AppendLine("<td>" + num + "</td>");
            trainerHtmlTable.AppendLine("<td>" + t.name.Trim() + "</td>");
            trainerHtmlTable.AppendLine("<td>" + t.yearsBC + "</td>");
            trainerHtmlTable.AppendLine("<td><a href=\"../add/add-trainer.aspx?edit=" + t.id + "\">edit</a> / <a href=\"?restore=" + t.id + "\">restore</a></td>");
            trainerHtmlTable.AppendLine("</tr>");

            num++;
        }

        trainerHtmlTable.AppendLine("</tbody>");

        trainerHtmlTable.AppendLine("</table>");

        trainerTable.Text = trainerHtmlTable.ToString();
    }

    public List<bcTrainers> GetData()
    {
        string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
        SqlConnection conn = null;

        try
        {
            List<bcTrainers> data = new List<bcTrainers>();
            conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT * FROM [bcTrainers] WHERE [deleted]='1' ORDER BY name", conn);
            conn.Open();
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                //Debug.WriteLine("Guid: " + (Guid)sdr["guid"] + " |");
                bcTrainers obj = new bcTrainers(
                    (string)sdr["id"],
                    (DateTimeOffset)sdr["createdAt"],
                    (DateTimeOffset)sdr["updatedAt"],
                    (bool)sdr["deleted"],
                    (string)sdr["name"],
                    (string)sdr["degree"],
                    (double)sdr["years"],
                    (double)sdr["yearsBC"],
                    (string)sdr["expertise"],
                    (string)sdr["reward"],
                    (string)sdr["expectation"],
                    (string)sdr["accomplishment"],
                    (string)sdr["photo"],
                    (string)sdr["reflections"]);
                data.Add(obj);
            }

            conn.Close();
            return data;
        }
        catch (Exception ex)
        {
            //log error 
            //display friendly error to user
            Debug.WriteLine("----");
            Debug.WriteLine("Source: " + ex.Source + " |");
            Debug.WriteLine("Message: " + ex.Message + " |");
            Debug.WriteLine("Stacktrace: " + ex.StackTrace + " |");
            Debug.WriteLine("Inner Exception: " + ex.InnerException + " |");
            Debug.WriteLine("----");
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
}