using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    public class Trainers
    {
        [DataMember]
        public trainers[] main { get; set; }
    }

    [DataContract]
    public class trainers
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public Guid guid { get; set; }

        [DataMember]
        public bool isActive { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string degree { get; set; }

        [DataMember]
        public string years { get; set; }

        [DataMember]
        public string years_bc { get; set; }

        [DataMember]
        public string expertise { get; set; }

        [DataMember]
        public string reward { get; set; }

        [DataMember]
        public string session { get; set; }

        [DataMember]
        public string accomplishment { get; set; }

        [DataMember]
        public string photo { get; set; }
    }
}