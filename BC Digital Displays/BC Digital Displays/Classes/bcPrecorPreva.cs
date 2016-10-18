using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    [DataContract]
    public class bcPrecorPreva
    {
        [DataMember(Name = "status")]
        public IList<string> status { get; set; }

        [DataMember(Name = "_type")]
        public string _type { get; set; }

        [DataMember(Name = "last_contact")]
        public IList<string> last_contact { get; set; }

        [DataMember(Name = "_index")]
        public int _index { get; set; }

        [DataMember(Name = "url")]
        public string url { get; set; }

        [DataMember(Name = "_cached_page_id")]
        public string _cached_page_id { get; set; }

        [DataMember(Name = "_template")]
        public string _template { get; set; }

        [DataMember(Name = "name")]
        public IList<string> name { get; set; }
    }
}
