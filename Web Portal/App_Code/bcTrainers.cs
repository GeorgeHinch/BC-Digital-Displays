using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

    [DataContract]
    public class bcTrainers
    {
        public bcTrainers(string id, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string name, string degree, double years, double yearsBC, string expertise, string reward, string expectation, string accomplishment, string photo, string reflections)
        {
            this.id = id;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            this.deleted = deleted;
            this.name = name;
            this.degree = degree;
            this.years = years;
            this.yearsBC = yearsBC;
            this.expertise = expertise;
            this.reward = reward;
            this.expectation = expectation;
            this.accomplishment = accomplishment;
            this.photo = photo;
            this.reflections = reflections;
        }

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
        public string degree { get; set; }

        [DataMember]
        public double years { get; set; }

        [DataMember]
        public double yearsBC { get; set; }

        [DataMember]
        public string expertise { get; set; }

        [DataMember]
        public string reward { get; set; }

        [DataMember]
        public string expectation { get; set; }

        [DataMember]
        public string accomplishment { get; set; }

        [DataMember]
        public string photo { get; set; }

        [DataMember]
        public string reflections { get; set; }
    }
