using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MenuItem
/// </summary>
public class MenuItem
{
    public MenuItem(string menuItem, string menuLink, string menuIcon)
    {
        this.menuItem = menuItem;
        this.menuLink = menuLink;
        this.menuIcon = menuIcon;
    }

    public string menuItem { get; set; }

    public string menuLink { get; set; }

    public string menuIcon { get; set; }
}