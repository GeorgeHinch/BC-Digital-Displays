using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class DayBuilder
    {
        public static string dayBuilder(IList<Day> days)
        {
            string returnString = "";

            foreach(Day d in days)
            {
                if((d.monday = true) && (d.tuesday = true) && (d.wednesday = true) && (d.thursday = true) && (d.friday = true))
                {
                    returnString = "M-F";
                }
                else
                {
                    returnString = "";
                }
            }

            return returnString;
        }
    }
}
