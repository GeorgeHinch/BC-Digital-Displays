using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Menus
/// </summary>
public class Menus
{
    public Menus(bool isActive, Guid guid, DateTime lastModified, string name, int menuId, string menuItem1, string menuItem2, string menuItem3, string menuItem4, string menuItem5, string menuItem6, string menuItem7, string menuItem8, string menuItem9)
    {
        this.isActive = isActive;
        this.guid = guid;
        this.lastModified = lastModified;
        this.name = name;
        this.menuId = menuId;
        this.menuItem1 = menuItem1;
        this.menuItem2 = menuItem2;
        this.menuItem3 = menuItem3;
        this.menuItem4 = menuItem4;
        this.menuItem5 = menuItem5;
        this.menuItem6 = menuItem6;
        this.menuItem7 = menuItem7;
        this.menuItem8 = menuItem8;
        this.menuItem9 = menuItem9;
    }

    public bool isActive { get; set; }

    public Guid guid { get; set; }

    public DateTime lastModified { get; set; }
    
    public string name { get; set; }
    
    public int menuId { get; set; }
    
    public string menuItem1 { get; set; }

    public string menuItem2 { get; set; }

    public string menuItem3 { get; set; }

    public string menuItem4 { get; set; }

    public string menuItem5 { get; set; }

    public string menuItem6 { get; set; }

    public string menuItem7 { get; set; }

    public string menuItem8 { get; set; }

    public string menuItem9 { get; set; }
}