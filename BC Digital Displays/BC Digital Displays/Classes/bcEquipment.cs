using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    public class bcEquipment
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
	
    [DataContract]		
    public class bcStudios
    {		
        public bcStudios()
        {
            this.studio1 = new List<string>();
            this.studio2 = new List<string>();
            this.studio3 = new List<string>();
            this.studio4 = new List<string>();
        }

        [DataMember]		
        public IList<string> studio1 { get; set; }		

        [DataMember]		
        public IList<string> studio2 { get; set; }		

        [DataMember]		
        public IList<string> studio3 { get; set; }

        [DataMember]		
        public IList<string> studio4 { get; set; }		
    }
}
