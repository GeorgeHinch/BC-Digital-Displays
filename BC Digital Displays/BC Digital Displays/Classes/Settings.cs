using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class Settings
    {
        [DataMember]
        public string pass { get; set; }

        [DataMember]
        public string background_type { get; set; }

        [DataMember]
        public string background { get; set; }

        [DataMember]
        public string logo { get; set; }
    }
}
