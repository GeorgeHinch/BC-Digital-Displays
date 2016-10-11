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
    class bcRecClasses
    {
        [DataMember]
        [Column(TypeName = "nvarchar(255)")]
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
        public string ageRange { get; set; }

        [DataMember]
        [Column(TypeName = "float")]
        public double ageMin { get; set; }

        [DataMember]
        [Column(TypeName = "float")]
        public double ageMax { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string days { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string time { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string location { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string sessions { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string description { get; set; }

        [DataMember]
        [Column(TypeName = "float")]
        public double category { get; set; }

        [DataMember]
        [Column(TypeName = "nvarchar(max)")]
        public string brochureID { get; set; }
    }

    [DataContract]
    public class cTimes
    {
        [DataMember]
        public DateTime cStartTime { get; set; }

        [DataMember]
        public DateTime cEndTime { get; set; }
    }
}
