using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Equipment
/// </summary>
public class Equipment
{
    public Equipment(bool isActive, Guid guid, DateTime lastModified, string studio, string name)
    {
        this.isActive = isActive;
        this.guid = guid;
        this.lastModified = lastModified;
        this.studio = studio;
        this.name = name;
    }

    public bool isActive { get; set; }

    public Guid guid { get; set; }

    public DateTime lastModified { get; set; }

    public string studio { get; set; }

    public string name { get; set; }
}