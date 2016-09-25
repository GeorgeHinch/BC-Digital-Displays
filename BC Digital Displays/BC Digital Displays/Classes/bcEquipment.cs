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

    //TODO: delete these when you've updated the needed files

    [DataContract]		
    public class Machines
    {		
        [DataMember]		
        public Studios main { get; set; }		
    }		
	
    [DataContract]		
    public class Studios
    {		
        [DataMember]		
        public IList<string> Studio1 { get; set; }		

        [DataMember]		
        public IList<string> Studio2 { get; set; }		

        [DataMember]		
        public IList<string> Studio3 { get; set; }

        [DataMember]		
        public IList<string> Studio4 { get; set; }		
    }
}
