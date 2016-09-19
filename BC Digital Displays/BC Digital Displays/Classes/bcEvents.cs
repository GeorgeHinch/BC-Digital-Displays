using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class bcEvents
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public DateTime createdAt { get; set; }

        [DataMember]
        public DateTime updatedAt { get; set; }

        [DataMember]
        public bool deleted { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public bool allDay { get; set; }

        [DataMember]
        public DateTime orderTime { get; set; }

        [DataMember]
        public string startTime { get; set; }

        [DataMember]
        public string endTime { get; set; }

        [DataMember]
        public string location { get; set; }

        [DataMember]
        public string instructor { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string department { get; set; }

        [DataMember]
        public string flier { get; set; }

        [DataMember]
        public string price { get; set; }

        [DataMember]
        public bool isApproved { get; set; }
    }
}
