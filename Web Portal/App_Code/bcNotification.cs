using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

public class bcNotification
{
    public bcNotification(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string glyph, string subject, string message, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        this.id = id;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.deleted = deleted;
        this.glyph = glyph;
        this.subject = subject;
        this.message = message;
        this.startDate = startDate;
        this.endDate = endDate;
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
    public string glyph { get; set; }

    [DataMember]
    public string subject { get; set; }

    [DataMember]
    public string message { get; set; }

    [DataMember]
    public DateTimeOffset startDate { get; set; }

    [DataMember]
    public DateTimeOffset endDate { get; set; }
}
