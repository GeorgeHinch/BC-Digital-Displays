using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class bcNotification
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
        public string glyph { get; set; }

        [DataMember]
        public string subject { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public DateTimeOffset startDate { get; set; }

        [DataMember]
        public DateTimeOffset endDate { get; set; }
    }
}
