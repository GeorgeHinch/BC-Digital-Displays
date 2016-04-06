using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Menus
/// </summary>
public class Menus
{
    public Menus(bool isActive, Guid guid, DateTime lastModified, string name, int menuId, MenuItem menuItem1, MenuItem menuItem2, MenuItem menuItem3, MenuItem menuItem4, MenuItem menuItem5, MenuItem menuItem6, MenuItem menuItem7, MenuItem menuItem8, MenuItem menuItem9)
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

    public MenuItem menuItem1 { get; set; }

    public MenuItem menuItem2 { get; set; }

    public MenuItem menuItem3 { get; set; }

    public MenuItem menuItem4 { get; set; }

    public MenuItem menuItem5 { get; set; }

    public MenuItem menuItem6 { get; set; }

    public MenuItem menuItem7 { get; set; }

    public MenuItem menuItem8 { get; set; }

    public MenuItem menuItem9 { get; set; }

}