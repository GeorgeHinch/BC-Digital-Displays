using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

[DataContract]
public class bcEvents
{
    public bcEvents(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string name, bool allDay, DateTimeOffset orderTime, string startTime, string endTime, string location, string instructor, string description, string department, string flier, string price, bool isApproved)
    {
        this.id = id;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.name = name;
        this.allDay = allDay;
        this.orderTime = orderTime;
        this.startTime = startTime;
        this.endTime = endTime;
        this.location = location;
        this.instructor = instructor;
        this.description = description;
        this.department = department;
        this.flier = flier;
        this.price = price;
        this.isApproved = isApproved;
    }

    [DataMember]
    public string id { get; set; }

    [DataMember]
    public DateTimeOffset createdAt { get; set; }

    [DataMember]
    public DateTimeOffset updatedAt { get; set; }

    [DataMember]
    public bool deleted { get; set; }

    [DataMember]
    public string name { get; set; }

    [DataMember]
    public bool allDay { get; set; }

    [DataMember]
    public DateTimeOffset orderTime { get; set; }

    [DataMember]
    public string startTime { get; set; }

    [DataMember]
    public string endTime { get; set; }

    [DataMember]
    public string location { get; set; }

    [DataMember]
    public string instructor { get; set; }

    [DataMember]
    public string description { get; set; }

    [DataMember]
    public string department { get; set; }

    [DataMember]
    public string flier { get; set; }

    [DataMember]
    public string price { get; set; }

    [DataMember]
    public bool isApproved { get; set; }
}
