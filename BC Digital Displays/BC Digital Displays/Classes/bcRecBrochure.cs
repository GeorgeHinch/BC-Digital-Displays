using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    class bcRecBrochure
    {
        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string id { get; set; }

        [DataMember]
        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset createdAt { get; set; }

        [DataMember]
        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset updatedAt { get; set; }

        [DataMember]
        [Column(TypeName = "bit")]
        public bool deleted { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string name { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string sessions { get; set; }

        [DataMember]
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
    }

    [DataContract]
    public class bcSessions
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string start { get; set; }

        [DataMember]
        public string end { get; set; }
    }
}
