using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


[DataContract]
public class bcRecBrochure
{
    public bcRecBrochure(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string name, string sessions, bool isActive)
    {
        this.id = id;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.deleted = deleted;
        this.name = name;
        this.sessions = sessions;
        this.isActive = isActive;
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
    public string sessions { get; set; }

    [DataMember]
    public bool isActive { get; set; }
}

[DataContract]
public class bcSessions
{
    public bcSessions(string name, string start, string end)
    {
        this.name = name;
        this.start = start;
        this.end = end;
    }

    [DataMember]
    public string name { get; set; }

    [DataMember]
    public string start { get; set; }

    [DataMember]
    public string end { get; set; }
}
