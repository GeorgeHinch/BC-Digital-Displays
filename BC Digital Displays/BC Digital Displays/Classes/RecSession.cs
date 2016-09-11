using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class RecSession
    {
        [DataMember(Name = "guid")]
        public Guid guid { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "session")]
        public int session { get; set; }

        [DataMember(Name = "startDay")]
        public DateTime startDay { get; set; }

        [DataMember(Name = "endDay")]
        public DateTime endDay { get; set; }
    }
}
