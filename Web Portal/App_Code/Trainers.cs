using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Trainers
/// </summary>
public class Trainers
{
    public Trainers(bool isActive, Guid guid, DateTime date, string name, string degree, int years, int yearsBC, string expertise, string reward, string session, string accomplishment, string photo)
    {
        this.isActive = isActive;
        this.guid = guid;
        this.date = date;
        this.name = name;
        this.degree = degree;
        this.years = years;
        this.years_bc = years_bc;
        this.expertise = expertise;
        this.reward = reward;
        this.session = session;
        this.accomplishment = accomplishment;
        this.photo = photo;
    }

    public bool isActive { get; set; }

    public Guid guid { get; set; }

    public DateTime date { get; set; }

    public string name { get; set; }
    
    public string degree { get; set; }

    public int years { get; set; }
    
    public int years_bc { get; set; }
    
    public string expertise { get; set; }
    
    public string reward { get; set; }
    
    public string session { get; set; }
    
    public string accomplishment { get; set; }
    
    public string photo { get; set; }
}