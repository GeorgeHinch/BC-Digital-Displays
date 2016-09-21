using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


[DataContract]
public class bcRecClasses
{
    public bcRecClasses(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string name, string ageRange, double ageMin, double ageMax, string days, string time, string location, string sessions, string description, double category, string brochureId)
    {
        this.id = id;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.deleted = deleted;
        this.name = name;
        this.ageRange = ageRange;
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.days = days;
        this.time = time;
        this.location = location;
        this.sessions = sessions;
        this.description = description;
        this.category = category;
        this.brochureID = brochureID;
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
    public string ageRange { get; set; }

    [DataMember]
    public double ageMin { get; set; }

    [DataMember]
    public double ageMax { get; set; }

    [DataMember]
    public string days { get; set; }

    [DataMember]
    public string time { get; set; }

    [DataMember]
    public string location { get; set; }

    [DataMember]
    public string sessions { get; set; }

    [DataMember]
    public string description { get; set; }

    [DataMember]
    public double category { get; set; }

    [DataMember]
    public string brochureID { get; set; }
}

[DataContract]
public class cTimes
{
    [DataMember]
    public DateTime cStartTime { get; set; }

    [DataMember]
    public DateTime cEndTime { get; set; }
}
