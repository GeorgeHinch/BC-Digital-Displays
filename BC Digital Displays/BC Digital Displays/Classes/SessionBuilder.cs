using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class SessionBuilder
    {
        public static string sessionBuilder(string s)
        {
            if (s == "11111")
            {
                return "Session: 1-5";
            }
            else
            {
                StringBuilder returnString = new StringBuilder();
                returnString.Append("Session: ");

                // Week start: M
                if (s[0] == '1')
                {
                    returnString.Append("1, ");
                }

                if (s[1] == '1')
                {
                    returnString.Append("2, ");
                }

                if (s[2] == '1')
                {
                    returnString.Append("3, ");
                }

                if (s[3] == '1')
                {
                    returnString.Append("4, ");
                }

                if (s[4] == '1')
                {
                    returnString.Append("5, ");
                }

                char[] trimArray = { ',', ' '};
                return returnString.ToString().TrimEnd(trimArray);
            }
        }
    }
}
