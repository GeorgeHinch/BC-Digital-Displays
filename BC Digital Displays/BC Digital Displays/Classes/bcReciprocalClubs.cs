using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class bcReciprocalClubs
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
}
