using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for bcReciprocalClubs
/// </summary>
public class bcReciprocalClubs
{
    public bcReciprocalClubs(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string clubName, string address, string phone, string fax, string email, string website, string specialRequest, string clubInfo, string addressLat, string addressLong, string sortCountry, string sortState, string sortCity)
    {
        this.id = id;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.deleted = deleted;
        this.clubName = clubName;
        this.address = address;
        this.phone = phone;
        this.fax = fax;
        this.email = email;
        this.website = website;
        this.specialRequest = specialRequest;
        this.clubInfo = clubInfo;
        this.addressLat = addressLat;
        this.addressLong = addressLong;
        this.sortCountry = sortCountry;
        this.sortState = sortState;
        this.sortCity = sortCity;
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
    public string clubName { get; set; }

    [DataMember]
    public string address { get; set; }

    [DataMember]
    public string phone { get; set; }

    [DataMember]
    public string fax { get; set; }

    [DataMember]
    public string email { get; set; }

    [DataMember]
    public string website { get; set; }

    [DataMember]
    public string specialRequest { get; set; }

    [DataMember]
    public string clubInfo { get; set; }

    [DataMember]
    public string addressLat { get; set; }

    [DataMember]
    public string addressLong { get; set; }

    [DataMember]
    public string sortCountry { get; set; }

    [DataMember]
    public string sortState { get; set; }

    [DataMember]
    public string sortCity { get; set; }
}