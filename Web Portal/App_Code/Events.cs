using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Events
/// </summary>
public class Events
{
    public Events(bool isActive, Guid guid, DateTime date, string subject, string location, string description, DateTime orderTime, string startTime, string endTime, string instructor, string department, string price, string flier, bool allDay)
    {
        this.isActive = isActive;
        this.guid = guid;
        this.date = date;
        this.Subject = subject;
        this.Location = location;
        this.Description = description;
        this.orderTime = orderTime;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Instructor = instructor;
        this.Department = department;
        this.Price = price;
        this.FlierJPG = flier;
        this.AllDay = allDay;
    }

    public bool isActive { get; set; }

    public Guid guid { get; set; }

    public DateTime date { get; set; }

    public string Subject { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public DateTime orderTime { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public string Instructor { get; set; }

    public string Department { get; set; }

    public string Price { get; set; }

    public string FlierJPG { get; set; }

    public bool AllDay { get; set; }
}