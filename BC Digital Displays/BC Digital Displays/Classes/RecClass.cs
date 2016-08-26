using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class RecClass
    {
        [DataMember(Name = "guid")]
        public Guid guid { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "category")]
        public string category { get; set; }

        [DataMember(Name = "age")]
        public string age { get; set; }

        [DataMember(Name = "days")]
        public IList<Day> days { get; set; }

        [DataMember(Name = "time")]
        public string time { get; set; }

        [DataMember(Name = "location")]
        public string location { get; set; }

        [DataMember(Name = "sessions")]
        public IList<Sessions> session { get; set; }

        [DataMember(Name = "brochureID")]
        public Guid brochureID { get; set; }
    }

    [DataContract]
    class Day
    {
        [DataMember(Name = "Monday")]
        public bool monday { get; set; }

        [DataMember(Name = "Tuesday")]
        public bool tuesday { get; set; }

        [DataMember(Name = "Wednesday")]
        public bool wednesday { get; set; }

        [DataMember(Name = "Thursday")]
        public bool thursday { get; set; }

        [DataMember(Name = "Friday")]
        public bool friday { get; set; }

        [DataMember(Name = "Saturday")]
        public bool saturday { get; set; }

        [DataMember(Name = "Sunday")]
        public bool sunday { get; set; }
    }
}
