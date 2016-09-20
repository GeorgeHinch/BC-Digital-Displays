using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

    [DataContract]
    class bcEquipment
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
        public double studio { get; set; }
    }
