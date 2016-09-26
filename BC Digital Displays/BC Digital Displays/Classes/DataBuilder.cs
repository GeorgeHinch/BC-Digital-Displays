using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public static int dayAdder(string d)
        {
            // Week start: M
            if (d[0] == '1')
            {
                return 0;
            }

            if (d[1] == '1')
            {
                return 1;
            }

            if (d[2] == '1')
            {
                return 2;
            }

            if (d[3] == '1')
            {
                return 3;
            }

            if (d[4] == '1')
            {
                return 4;
            }

            if (d[5] == '1')
            {
                return 5;
            }

            if (d[6] == '1')
            {
                return 6;
            }

            return 0;
        }
        #endregion

        #region Builds email string for classes
        public static string emailRecClassBuilder(bcRecClasses thisClass, bool tb1, bool tb2, bool tb3, bool tb4, bool tb5)
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

        #region Builds ics file from string
        // Builds ics file for rec classes
        public static List<string> icsBuilder(bcRecClasses thisClass, bcRecBrochure thisBrochure, bool tb1, bool tb2, bool tb3, bool tb4, bool tb5)
        {
            List<string> returnList = new List<string>();
            List<bcSessions> thisSessions = JsonConvert.DeserializeObject<List<bcSessions>>(thisBrochure.sessions);
            List<bool> tbBools = new List<bool>();
                tbBools.Add(tb1);
                tbBools.Add(tb2);
                tbBools.Add(tb3);
                tbBools.Add(tb4);
                tbBools.Add(tb5);
            string days = dayBuilder(thisClass.days);

            int sessionNum = 0;
            foreach (bool b in tbBools)
            {
                if (b == true)
                {
                    cTimes thisTime = JsonConvert.DeserializeObject<cTimes>(thisClass.time);
                    
                    int addDays = DataBuilder.dayAdder(thisClass.days);
                    DateTime sessionStart = DateTime.ParseExact(thisSessions[sessionNum].start + thisTime.cStartTime.ToString("HHmm"), "yyyy-MM-ddHHmm", CultureInfo.InvariantCulture).AddDays(addDays);
                    DateTime sessionEnd = DateTime.ParseExact(thisSessions[sessionNum].start + thisTime.cEndTime.ToString("HHmm"), "yyyy-MM-ddHHmm", CultureInfo.InvariantCulture).AddDays(addDays);
                    DateTime sessionFinal = DateTime.ParseExact(thisSessions[sessionNum].end, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    StringBuilder returnString = new StringBuilder();

                    returnString.AppendLine("BEGIN:VCALENDAR");
                    returnString.AppendLine("PRODID:-//Bellevue Club//BC Digital Signage Controller//EN");
                    returnString.AppendLine("VERSION:2.0");
                    
                    returnString.AppendLine("BEGIN:VEVENT");
                    returnString.AppendLine("SUMMARY;LANGUAGE=en-us:" + thisClass.name);
                    returnString.AppendLine("CLASS:PUBLIC");
                    returnString.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
                    returnString.AppendLine("DESCRIPTION:" + thisClass.description);
                    returnString.AppendLine("DTSTART:" + string.Format("{0:yyyyMMddTHHmmssZ}", sessionStart));
                    returnString.AppendLine("DTEND:" + string.Format("{0:yyyyMMddTHHmmssZ}", sessionEnd));
                    returnString.AppendLine("RRULE:FREQ=WEEKLY;UNTIL=" + string.Format("{0:yyyyMMddTHHmmssZ}",sessionFinal));
                    returnString.AppendLine("UID:" + Guid.NewGuid());
                    returnString.AppendLine("LOCATION:Bellevue Club " + thisClass.location);

                    returnString.AppendLine("BEGIN:VALARM");
                    returnString.AppendLine("UID:" + Guid.NewGuid());
                    returnString.AppendLine("TRIGGER:-PT1H");
                    returnString.AppendLine("DESCRIPTION:Bellevue Club " + thisClass.name);
                    returnString.AppendLine("ACTION:DISPLAY");
                    returnString.AppendLine("END:VALARM");

                    returnString.AppendLine("END:VEVENT");
                    returnString.AppendLine("END:VCALENDAR");
                    
                    returnList.Add(returnString.ToString());
                }

                sessionNum++;
            }
            
            return returnList;
        }

        // Builds ics file for single events
        public static string icsBuilder(bcEvents thisEvent)
        {
            DateTime eventStart = DateTime.ParseExact(thisEvent.startTime, "yyyy,  M,  d,  H,  m,  s", CultureInfo.InvariantCulture);
            DateTime eventEnd = DateTime.ParseExact(thisEvent.endTime, "yyyy,  M,  d,  H,  m,  s", CultureInfo.InvariantCulture);

            StringBuilder returnString = new StringBuilder();

            returnString.AppendLine("BEGIN:VCALENDAR");
            returnString.AppendLine("PRODID:-//Bellevue Club//BC Digital Signage Controller//EN");
            returnString.AppendLine("VERSION:2.0");

            returnString.AppendLine("BEGIN:VEVENT");
            returnString.AppendLine("SUMMARY;LANGUAGE=en-us:" + thisEvent.name);
            returnString.AppendLine("CLASS:PUBLIC");
            returnString.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            returnString.AppendLine("DESCRIPTION:" + thisEvent.description);
            returnString.AppendLine("DTSTART:" + string.Format("{0:yyyyMMddTHHmmssZ}", eventStart));
            returnString.AppendLine("DTEND:" + string.Format("{0:yyyyMMddTHHmmssZ}", eventEnd));
            returnString.AppendLine("UID:" + Guid.NewGuid());
            returnString.AppendLine("LOCATION:Bellevue Club " + thisEvent.location);

            returnString.AppendLine("BEGIN:VALARM");
            returnString.AppendLine("UID:" + Guid.NewGuid());
            returnString.AppendLine("TRIGGER:-PT1H");
            returnString.AppendLine("DESCRIPTION:Bellevue Club " + thisEvent.name);
            returnString.AppendLine("ACTION:DISPLAY");
            returnString.AppendLine("END:VALARM");

            returnString.AppendLine("END:VEVENT");
            returnString.AppendLine("END:VCALENDAR");
            
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
