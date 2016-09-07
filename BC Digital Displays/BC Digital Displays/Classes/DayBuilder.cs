using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class DayBuilder
    {
        public static string dayBuilder(string d)
        {
            if (d == "1111100")
            {
                return "M-F";
            }

            else if (d == "1111111")
            {
                return "Daily";
            }

            else if (d == "0000011")
            {
                return "Weekends";
            }

            else
            {
                StringBuilder returnString = new StringBuilder();

                // Week start: M
                if (d[0] == '1')
                {
                    returnString.Append("M");
                }

                else if (d[1] == '1')
                {
                    returnString.Append("Tu");
                }

                else if (d[2] == '1')
                {
                    returnString.Append("W");
                }

                else if (d[3] == '1')
                {
                    returnString.Append("Th");
                }

                else if (d[4] == '1')
                {
                    returnString.Append("F");
                }

                else if (d[5] == '1')
                {
                    returnString.Append("Sa");
                }

                else if (d[6] == '1')
                {
                    returnString.Append("Su");
                }

                return returnString.ToString();
            }
        }
    }
}
