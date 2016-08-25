using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class SessionInfo
    {
        [DataMember(Name = "sessionName")]
        public string sessionName { get; set; }

        [DataMember(Name = "sessionStart")]
        public string sessionStart { get; set; }

        [DataMember(Name = "sessionEnd")]
        public string sessionEnd { get; set; }

        [DataMember(Name = "sessionReg")]
        public string sessionReg { get; set; }
    }

    [DataContract]
    class Sessions
    {
        [DataMember(Name = "session")]
        public bool session { get; set; }
    }
}
