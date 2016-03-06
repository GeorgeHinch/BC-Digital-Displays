using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class PrecorStatus
    {
        public int count { get; set; }
        public string lastrunstatus { get; set; }
        public string name { get; set; }
        public string newdata { get; set; }
        public string nextrun { get; set; }
        public PrecorResults results { get; set; }
        public string thisversionrun { get; set; }
        public string thisversionstatus { get; set; }
        public int version { get; set; }

        public int studio1active { get; set; }
        public int studio1total { get; set; }
        public int studio2active { get; set; }
        public int studio2total { get; set; }
        public int studio3active { get; set; }
        public int studio3total { get; set; }
        public int studio4active { get; set; }
        public int studio4total { get; set; }
    }

    class PrecorResults
    {
        public PrevaResults[] Preva { get; set; }
    }

    class PrevaResults
    {
        public string LastContact { get; set; }
        public MachineName Name { get; set; }
        public string Status { get; set; }
    }

    class MachineName
    {
        public string href { get; set; }
        public string text { get; set; }
    }
}
