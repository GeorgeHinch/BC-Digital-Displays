using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class Trainer
    {
        [DataMember]
        public string name { get; set; }

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
        public string photo { get; set; }
    }

    [DataContract]
    class Trainers
    {
        [DataMember]
        public Trainer[] main { get; set; }
    }
}
