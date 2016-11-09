using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for GeoResponse
/// </summary>
public class GeoBuilder
{
    [DataContract]
    public class AddressComponent
    {
        [DataMember(Name = "long_name")]
        public string long_name { get; set; }

        [DataMember(Name = "short_name")]
        public string short_name { get; set; }

        [DataMember(Name = "types")]
        public IList<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        [DataMember(Name = "address_components")]
        public IList<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public IList<string> types { get; set; }
    }

    public class geoResponse
    {
        public IList<Result> results { get; set; }
        public string status { get; set; }
    }
}