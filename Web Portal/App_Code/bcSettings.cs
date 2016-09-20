using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

    [DataContract]
    class bcSettings
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
        public bool isActive { get; set; }

        [DataMember]
        public string logoURL { get; set; }

        [DataMember]
        public string backgroundType { get; set; }

        [DataMember]
        public string backgroundURL { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string theme { get; set; }

        [DataMember]
        public bool welcomeActive { get; set; }

        [DataMember]
        public string welcomeType { get; set; }

        [DataMember]
        public string welcomeMessage { get; set; }
    }
