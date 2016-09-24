using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class DataBuilder
    {
        #region Builds day string from bits
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

                if (d[1] == '1')
                {
                    returnString.Append("Tu");
                }

                if (d[2] == '1')
                {
                    returnString.Append("W");
                }

                if (d[3] == '1')
                {
                    returnString.Append("Th");
                }

                if (d[4] == '1')
                {
                    returnString.Append("F");
                }

                if (d[5] == '1')
                {
                    returnString.Append("Sa");
                }

                if (d[6] == '1')
                {
                    returnString.Append("Su");
                }

                return returnString.ToString();
            }
        }
        #endregion

        #region Builds email string
        public static string emailBuilder(bcRecClasses thisClass, bool tb1, bool tb2, bool tb3, bool tb4, bool tb5)
        {
            StringBuilder returnString = new StringBuilder();


            if (tb1)
            {
                returnString.AppendLine("<b>" + "//empty" + "</b><br />");
            }
            else
            {
                returnString.AppendLine("//empty" + "<br />");
            }

            if (tb2)
            {
                returnString.AppendLine("<b>" + "//empty" + "</b><br />");
            }
            else
            {
                returnString.AppendLine("//empty" + "<br />");
            }

            if (tb3)
            {
                returnString.AppendLine("<b>" + "//empty" + "</b><br />");
            }
            else
            {
                returnString.AppendLine("//empty" + "<br />");
            }

            if (tb4)
            {
                returnString.AppendLine("<b>" + "//empty" + "</b><br />");
            }
            else
            {
                returnString.AppendLine("//empty" + "<br />");
            }

            if (tb5)
            {
                returnString.AppendLine("<b>" + "//empty" + "</b><br />");
            }
            else
            {
                returnString.AppendLine("//empty" + "<br />");
            }

            return returnString.ToString();
        }
        #endregion

        #region Builds session string from bits
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

                char[] trimArray = { ',', ' ' };
                return returnString.ToString().TrimEnd(trimArray);
            }
        }
        #endregion

        #region Builds time string
        public static string timeBuilder(string T)
        {
            cTimes t = JsonConvert.DeserializeObject<cTimes>(T);

            if (t.cStartTime.ToString("tt") == t.cEndTime.ToString("tt"))
            {
                return t.cStartTime.ToString("h:mm") + " - " + t.cEndTime.ToString("h:mm tt");
            }
            else
            {
                return t.cStartTime.ToString("h:mm tt") + " - " + t.cEndTime.ToString("h:mm tt");
            }
        }
        #endregion
    }
}
