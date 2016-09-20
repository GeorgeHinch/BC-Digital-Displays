using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace BC_Digital_Displays.Classes
{
    class GenerateEmail
    {
        public static string buildEmail(RecClass rc, string ses)
        {
            #region Creates assembly information string
            int major = Package.Current.Id.Version.Major;
            int minor = Package.Current.Id.Version.Minor;
            int build = Package.Current.Id.Version.Build;
            int revision = Package.Current.Id.Version.Revision;
            string assemblyInformation = Package.Current.DisplayName + ", Version: " + major + "." + minor + "." + build + " (" + revision + ")";
            #endregion

            #region Creates registration DateTime
            DateTime reg = new DateTime();

            // TODO: SQL query to pull session registration day
            #endregion

            #region Builds description string
            StringBuilder des = new StringBuilder();
            des.AppendLine(rc.description);

            //TODO: Build body copy about class
            #endregion

            #region Builds string for ICS file
            StringBuilder str = new StringBuilder();

            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine(string.Format("PRODID:-//{0}", assemblyInformation));
            str.AppendLine("version2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");

            str.AppendLine(string.Format("DSTART:{0:yyyyMMddTHHmmssZ}", reg));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", reg.AddMinutes(+15)));
            str.AppendLine("LOCATION: Bellevue Club");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION: {0}", des));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", des));
            str.AppendLine(string.Format("SUMMARY:Register - {0}", rc.name));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-P10M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");

            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            #endregion

            return str.ToString();
        }
    }
}
