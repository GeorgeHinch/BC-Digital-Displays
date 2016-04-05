using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Menus
/// </summary>
public class Menus
{
    public Menus(bool isActive, Guid guid, DateTime lastModified, string name, int menuId, string menuItem1, string menuItem1Link, string menuItem1Icon, string menuItem2, string menuItem2Link, string menuItem2Icon, string menuItem3, string menuItem3Link, string menuItem3Icon, string menuItem4, string menuItem4Link, string menuItem4Icon, string menuItem5, string menuItem5Link, string menuItem5Icon, string menuItem6, string menuItem6Link, string menuItem6Icon, string menuItem7, string menuItem7Link, string menuItem7Icon, string menuItem8, string menuItem8Link, string menuItem8Icon, string menuItem9, string menuItem9Link, string menuItem9Icon)
    {
        this.isActive = isActive;
        this.guid = guid;
        this.lastModified = lastModified;
        this.name = name;
        this.menuId = menuId;
        this.menuItem1 = menuItem1;
        this.menuItem1Link = menuItem1Link;
        this.menuItem1Icon = menuItem1Icon;
        this.menuItem2 = menuItem2;
        this.menuItem2Link = menuItem2Link;
        this.menuItem2Icon = menuItem2Icon;
        this.menuItem3 = menuItem3;
        this.menuItem3Link = menuItem3Link;
        this.menuItem3Icon = menuItem3Icon;
        this.menuItem4 = menuItem4;
        this.menuItem4Link = menuItem4Link;
        this.menuItem4Icon = menuItem4Icon;
        this.menuItem5 = menuItem5;
        this.menuItem5Link = menuItem5Link;
        this.menuItem5Icon = menuItem5Icon;
        this.menuItem6 = menuItem6;
        this.menuItem6Link = menuItem6Link;
        this.menuItem6Icon = menuItem6Icon;
        this.menuItem7 = menuItem7;
        this.menuItem7Link = menuItem7Link;
        this.menuItem7Icon = menuItem7Icon;
        this.menuItem8 = menuItem8;
        this.menuItem8Link = menuItem8Link;
        this.menuItem8Icon = menuItem8Icon;
        this.menuItem9 = menuItem9;
        this.menuItem9Link = menuItem9Link;
        this.menuItem9Icon = menuItem9Icon;
    }

    public bool isActive { get; set; }

    public Guid guid { get; set; }

    public DateTime lastModified { get; set; }
    
    public string name { get; set; }
    
    public int menuId { get; set; }
    
    public string menuItem1 { get; set; }
    public string menuItem1Link { get; set; }
    public string menuItem1Icon { get; set; }

    public string menuItem2 { get; set; }
    public string menuItem2Link { get; set; }
    public string menuItem2Icon { get; set; }

    public string menuItem3 { get; set; }
    public string menuItem3Link { get; set; }
    public string menuItem3Icon { get; set; }

    public string menuItem4 { get; set; }
    public string menuItem4Link { get; set; }
    public string menuItem4Icon { get; set; }

    public string menuItem5 { get; set; }
    public string menuItem5Link { get; set; }
    public string menuItem5Icon { get; set; }

    public string menuItem6 { get; set; }
    public string menuItem6Link { get; set; }
    public string menuItem6Icon { get; set; }

    public string menuItem7 { get; set; }
    public string menuItem7Link { get; set; }
    public string menuItem7Icon { get; set; }

    public string menuItem8 { get; set; }
    public string menuItem8Link { get; set; }
    public string menuItem8Icon { get; set; }

    public string menuItem9 { get; set; }
    public string menuItem9Link { get; set; }
    public string menuItem9Icon { get; set; }
}