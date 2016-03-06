using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class PrecorStatus
    {
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public string lastrunstatus { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string newdata { get; set; }
        [DataMember]
        public string nextrun { get; set; }
        [DataMember]
        public PrecorResults results { get; set; }
        [DataMember]
        public string thisversionrun { get; set; }
        [DataMember]
        public string thisversionstatus { get; set; }
        [DataMember]
        public int version { get; set; }
        [DataMember]
        public int studio1active { get; set; }
        [DataMember]
        public int studio1total { get; set; }
        [DataMember]
        public int studio2active { get; set; }
        [DataMember]
        public int studio2total { get; set; }
        [DataMember]
        public int studio3active { get; set; }
        [DataMember]
        public int studio3total { get; set; }
        [DataMember]
        public int studio4active { get; set; }
        [DataMember]
        public int studio4total { get; set; }
    }

    [DataContract]
    class PrecorResults
    {
        [DataMember]
        public IList<PrevaResults> Preva { get; set; }
    }

    [DataContract]
    class PrevaResults
    {
        [DataMember]
        public string LastContact { get; set; }
        [DataMember]
        public MachineName Name { get; set; }
        [DataMember]
        public string Status { get; set; }
    }

    [DataContract]
    class MachineName
    {
        [DataMember]
        public string href { get; set; }
        [DataMember]
        public string text { get; set; }
    }
}
