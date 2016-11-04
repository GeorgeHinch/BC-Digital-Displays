using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for bcMenu
/// </summary>
public class bcMenu
{
    public bcMenu(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string menuItem, double orderVal)
    {
        this.id = id;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.deleted = deleted;
        this.menuItem = menuItem;
        this.orderVal = orderVal;
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
    public double orderVal { get; set; }

    [DataMember]
    public string menuItem { get; set; }
}