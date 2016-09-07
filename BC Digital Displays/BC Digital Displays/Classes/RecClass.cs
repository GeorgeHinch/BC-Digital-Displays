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
        public string days { get; set; }

        [DataMember(Name = "time")]
        public string time { get; set; }

        [DataMember(Name = "location")]
        public string location { get; set; }

        [DataMember(Name = "sessions")]
        public string session { get; set; }

        [DataMember(Name = "brochureID")]
        public Guid brochureID { get; set; }
    }
}
