using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class bcRecClasses
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public DateTimeOffset createdAt { get; set; }

        [DataMember]
        public DateTimeOffset updatedAt { get; set; }

        [DataMember]
        public bool deleted { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string ageRange { get; set; }

        [DataMember]
        public double ageMin { get; set; }

        [DataMember]
        public double ageMax { get; set; }

        [DataMember]
        public string days { get; set; }

        [DataMember]
        public string time { get; set; }

        [DataMember]
        public string location { get; set; }

        [DataMember]
        public string sessions { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public double category { get; set; }

        [DataMember]
        public string brochureID { get; set; }
    }
}
