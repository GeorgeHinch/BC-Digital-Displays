using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

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

        public static string dayBuilder(DateTime start, DateTime end)
        {
            if (start.ToString("MM/dd/yyyy") == end.ToString("MM/dd/yyyy"))
            {
                return start.ToString("MMMMM, d, yyyy") + ", " + DataBuilder.timeBuilder(start, end);
            }
            else
            {
                return start.ToString("MMMM, d") + " - " + end.ToString("MMMM, d, yyyy") + ", " + DataBuilder.timeBuilder(start, end);
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

        #region Builds email string for classes & events
        public static string emailRecClassBuilder(bcRecClasses thisClass, bcRecBrochure thisBrochure, bool tb1, bool tb2, bool tb3, bool tb4, bool tb5)
        {
            List<bcSessions> theseSessions = JsonConvert.DeserializeObject<List<bcSessions>>(thisBrochure.sessions);
            StringBuilder returnString = new StringBuilder();

            #region Email template set up (DO NOT EDIT)
            returnString.AppendLine("<html><head><meta name=\"viewport\" content=\"width=device-width\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>Bellevue Club</title>");
            returnString.AppendLine("<link rel=\"stylesheet\" href=\"http://www.bellevueclub.com/Forms/email/assets/email.css\" />");
            returnString.AppendLine("<link href='https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700,700italic,400italic,300,300italic,200italic,200' rel='stylesheet' type='text/css'>");
            returnString.AppendLine("</head><body bgcolor=\"#f6f6f6\"><!-- body --><table class=\"body-wrap\" bgcolor=\"#f6f6f6\"><tr><td></td><td class=\"container\" bgcolor=\"#FFFFFF\"><!-- content --><div class=\"content\"><table><tr><td>");
            returnString.AppendLine("<div style=\"text-align:center; margin:auto; \"><img src=\"http://www.bellevueclub.com/Forms/email/assets/bc_logo.png\" style=\"width:200px;text-align:center;align:center;margin:auto;\" align=\"middle\"/></div>");
            #endregion

            returnString.AppendLine("<h1>Details for " + thisClass.name + "</h1>");
            returnString.AppendLine("<h2>Class Description</h2>");
            returnString.AppendLine("<p><em>"+ thisClass.ageRange + ", " + DataBuilder.dayBuilder(thisClass.days) + ", " + DataBuilder.timeBuilder(thisClass.time) + "</em></p>");
            returnString.AppendLine("<p>" + thisClass.description + "</p>");

            returnString.AppendLine("<h2>Registering</h2>");
            returnString.AppendLine("<p>From tennis, basketball and swim lessons to art classes and special holiday events, the Bellevue Club and our roster of excellent instructors have lots of plans for your family.</p>");
            returnString.AppendLine("<p>Register for this class and many more at <a href=\"https://members.bellevueclub.com\">members.bellevueclub.com</a>.</p>");

            returnString.AppendLine("<h2>Class Sessions</h2>");
            returnString.AppendLine("<p>The sessions you've shown interest in are bolded below. We've also added calendar events to this email so you never miss an exciting class at the Bellevue Club.</p>  <p>");
            if (tb1)
            {
                returnString.AppendLine("<b>" + theseSessions[0].name + ": " + Convert.ToDateTime(theseSessions[0].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[0].end).ToString("MMMM d, yyyy") + "</b><br />");
            }
            else
            {
                returnString.AppendLine(theseSessions[0].name + ": " + Convert.ToDateTime(theseSessions[0].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[0].end).ToString("MMMM d, yyyy") + "<br />");
            }

            if (tb2)
            {
                returnString.AppendLine("<b>" + theseSessions[1].name + ": " + Convert.ToDateTime(theseSessions[1].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[1].end).ToString("MMMM d, yyyy") + "</b><br />");
            }
            else
            {
                returnString.AppendLine(theseSessions[1].name + ": " + Convert.ToDateTime(theseSessions[1].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[1].end).ToString("MMMM d, yyyy") + "<br />");
            }

            if (tb3)
            {
                returnString.AppendLine("<b>" + theseSessions[2].name + ": " + Convert.ToDateTime(theseSessions[2].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[2].end).ToString("MMMM d, yyyy") + "</b><br />");
            }
            else
            {
                returnString.AppendLine(theseSessions[2].name + ": " + Convert.ToDateTime(theseSessions[2].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[2].end).ToString("MMMM d, yyyy") + "<br />");
            }

            if (tb4)
            {
                returnString.AppendLine("<b>" + theseSessions[3].name + ": " + Convert.ToDateTime(theseSessions[3].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[3].end).ToString("MMMM d, yyyy") + "</b><br />");
            }
            else
            {
                returnString.AppendLine(theseSessions[3].name + ": " + Convert.ToDateTime(theseSessions[3].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[3].end).ToString("MMMM d, yyyy") + "<br />");
            }

            if (tb5)
            {
                returnString.AppendLine("<b>" + theseSessions[4].name + ": " + Convert.ToDateTime(theseSessions[4].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[4].end).ToString("MMMM d, yyyy") + "</b><br />");
            }
            else
            {
                returnString.AppendLine(theseSessions[4].name + ": " + Convert.ToDateTime(theseSessions[4].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(theseSessions[4].end).ToString("MMMM d, yyyy") + "<br />");
            }

            #region Email template footer (DO NOT EDIT)
            returnString.AppendLine("</p></td></tr></table> </div></td><td></td></tr></table> <table class=\"footer-wrap\"> <tr> <td></td><td class=\"container\"> <div class=\"content\"> <table> <tr> <td align=\"center\"> <p> For additional information or questions, contact the Bellevue Club at 425-455-1616. </p></td></tr></table> </div></td><td></td></tr></table> </body></html>");
            #endregion
            
            return returnString.ToString();
        }

        public static string emailEventBuilder(Appointment thisEvent)
        {
            StringBuilder returnString = new StringBuilder();

            #region Email template set up (DO NOT EDIT)
            returnString.AppendLine("<html><head><meta name=\"viewport\" content=\"width=device-width\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"><title>Bellevue Club</title>");
            returnString.AppendLine("<link rel=\"stylesheet\" href=\"http://www.bellevueclub.com/Forms/email/assets/email.css\" />");
            returnString.AppendLine("<link href='https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700,700italic,400italic,300,300italic,200italic,200' rel='stylesheet' type='text/css'>");
            returnString.AppendLine("</head><body bgcolor=\"#f6f6f6\"><!-- body --><table class=\"body-wrap\" bgcolor=\"#f6f6f6\"><tr><td></td><td class=\"container\" bgcolor=\"#FFFFFF\"><!-- content --><div class=\"content\"><table><tr><td>");
            returnString.AppendLine("<div style=\"text-align:center; margin:auto; \"><img src=\"http://www.bellevueclub.com/Forms/email/assets/bc_logo.png\" style=\"width:200px;text-align:center;align:center;margin:auto;\" align=\"middle\"/></div>");
            #endregion

            returnString.AppendLine("<h1>Details for " + thisEvent.Subject + "</h1>");
            returnString.AppendLine("<h2>Event Description</h2>");
            returnString.AppendLine("<p><em>" + DataBuilder.dayBuilder(thisEvent.StartDT, thisEvent.EndDT) + "</em></p>");
            returnString.AppendLine("<p>" + thisEvent.Notes + "</p>");

            returnString.AppendLine("<h2>Registering</h2>");
            returnString.AppendLine("<p>From tennis, basketball and swim lessons to art classes and special holiday events, the Bellevue Club and our roster of excellent instructors have lots of plans for your family.</p>");
            returnString.AppendLine("<p>Register for this event and many more at <a href=\"https://members.bellevueclub.com\">members.bellevueclub.com</a>.</p>");

            #region Email template footer (DO NOT EDIT)
            returnString.AppendLine("</p></td></tr></table> </div></td><td></td></tr></table> <table class=\"footer-wrap\"> <tr> <td></td><td class=\"container\"> <div class=\"content\"> <table> <tr> <td align=\"center\"> <p> For additional information or questions, contact the Bellevue Club at 425-455-1616. </p></td></tr></table> </div></td><td></td></tr></table> </body></html>");
            #endregion

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
                    returnString.AppendLine("DTSTART;TZID=America/Los_Angeles:" + string.Format("{0:yyyyMMddTHHmmss}", sessionStart));
                    returnString.AppendLine("DTEND;TZID=America/Los_Angeles:" + string.Format("{0:yyyyMMddTHHmmss}", sessionEnd));
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
        public static string icsBuilder(Appointment thisEvent)
        {
            StringBuilder returnString = new StringBuilder();

            returnString.AppendLine("BEGIN:VCALENDAR");
            returnString.AppendLine("PRODID:-//Bellevue Club//BC Digital Signage Controller//EN");
            returnString.AppendLine("VERSION:2.0");

            returnString.AppendLine("BEGIN:VEVENT");
            returnString.AppendLine("SUMMARY;LANGUAGE=en-us:" + thisEvent.Subject);
            returnString.AppendLine("CLASS:PUBLIC");
            returnString.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            returnString.AppendLine("DESCRIPTION:" + thisEvent.Notes);
            returnString.AppendLine("DTSTART;TZID=America/Los_Angeles:" + string.Format("{0:yyyyMMddTHHmmss}", thisEvent.StartDT));
            returnString.AppendLine("DTEND;TZID=America/Los_Angeles:" + string.Format("{0:yyyyMMddTHHmmss}", thisEvent.EndDT));
            returnString.AppendLine("UID:" + Guid.NewGuid());
            returnString.AppendLine("LOCATION:Bellevue Club " + thisEvent.Location);

            returnString.AppendLine("BEGIN:VALARM");
            returnString.AppendLine("UID:" + Guid.NewGuid());
            returnString.AppendLine("TRIGGER:-PT1H");
            returnString.AppendLine("DESCRIPTION:Bellevue Club " + thisEvent.Subject);
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

        public static string timeBuilder(DateTime start, DateTime end)
        {
            if (start.ToString("tt") == end.ToString("tt"))
            {
                return start.ToString("h:mm") + " - " + end.ToString("h:mm tt");
            }
            else
            {
                return start.ToString("h:mm tt") + " - " + end.ToString("h:mm tt");
            }
        }
        #endregion

        #region Builds rich text from markdown string
        public static Paragraph markdownBuilder(string s)
        {
            // boldDelimiter = "__";
            // italicDelimiter = "**";
            // tabDelimiter = "···";
            // bulletDelimiter = "··*";
            s = s.Trim();
            s = Regex.Replace(s, "\n", "\n\n");
            s = Regex.Replace(s, "···", "        ");
            s = Regex.Replace(s, @"··\*|\n\n··\*", "\n      • ");

            Paragraph returnParagraph = new Paragraph();

            Match specialChar = Regex.Match(s, @"(.*?)(\*\*|__)(.*?)(\*\*|__)");
            Match lastSpecialChar = specialChar;

            int maxIndex = 0;

            while (specialChar.Success)
            {
                Run firstMatchRun = new Run();
                firstMatchRun.Text = specialChar.Groups[1].ToString();
                returnParagraph.Inlines.Add(firstMatchRun);

                Run specialTextRun = new Run();
                specialTextRun.Text = specialChar.Groups[3].ToString();

                if(specialChar.Groups[2].ToString() == "__")
                {
                    specialTextRun.FontWeight = FontWeights.Bold;
                }
                else if (specialChar.Groups[2].ToString() == "**")
                {
                    specialTextRun.FontStyle = FontStyle.Italic;
                }
                returnParagraph.Inlines.Add(specialTextRun);

                specialChar = specialChar.NextMatch();
                if (maxIndex < specialChar.Index)
                {
                    // We want to track the last successful match so that we can reference it later
                    // This will allow us to get the rest of the string after the last match and make sure that we include that string.
                    maxIndex = specialChar.Index;
                    lastSpecialChar = specialChar;
                }

            }

            Run lastTextRun = new Run();
            if (maxIndex > 0)
            {
                // We were able to match SOMETHING and put special styles


                int lastMatchIndex = lastSpecialChar.Groups[4].Captures[0].Index;

                // The last group begins with either "**" or "__" and contains the remainder of the string that we care about. Add two so that we ignore the "**" or "__"
                lastMatchIndex += 2;

                lastTextRun.Text = s.Substring(lastMatchIndex);
            } else
            {
                // maxIndex == 0, meaning that we never matched anything at all

                lastTextRun.Text = s;
            }
            returnParagraph.Inlines.Add(lastTextRun);

            return returnParagraph;
        }
        #endregion

        #region Builds buttons for main menu
        public static Button buttonBuilder (bcMenu m)
        {
            Button returnButton = new Button();
            StackPanel buttonStackpanel = new StackPanel();

            #region Styles returned button
            returnButton.Width = 300;
            returnButton.Height = 220;
            returnButton.Background = null;
            returnButton.BorderBrush = null;
            returnButton.VerticalAlignment = VerticalAlignment.Top;
            #endregion

            #region Creates && styles button icon
            TextBlock iconTextblock = new TextBlock();

            iconTextblock.FontFamily = new FontFamily("Segoe MDL2 Assets");
            iconTextblock.TextAlignment = TextAlignment.Center;
            iconTextblock.FontSize = 150;
            iconTextblock.Margin = new Thickness(0, 0, 0, 20);
            iconTextblock.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            #endregion

            #region Creates && styles button label
            TextBlock labelTextblock = new TextBlock();

            labelTextblock.FontWeight = FontWeights.Light;
            labelTextblock.TextAlignment = TextAlignment.Center;
            labelTextblock.FontSize = 24;
            labelTextblock.Margin = new Thickness(0, 0, 0, 5);
            labelTextblock.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            #endregion

            #region Club News button creation
            if (m.id == "AD177081-4E50-44B9-B391-5DD9A9E791AB")
            {
                returnButton.Name = "Menu_ClubNews_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_ClubNews_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            #region Fitness button creation
            if (m.id == "5ACBD6C3-7BD6-4633-8CFA-1C2B7F212AB5")
            {
                returnButton.Name = "Menu_Fitness_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_Fitness_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            #region Trainers button creation
            if (m.id == "681990F0-EF8C-4DE3-A77F-9680BD79C57A")
            {
                returnButton.Name = "Menu_Trainers_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_Trainers_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            #region Equipment button creation
            if (m.id == "D3696D55-335B-4C79-A313-4D7A2976B2A0")
            {
                returnButton.Name = "Menu_Equipment_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_Equipment_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            #region Youth Activities button creation
            if (m.id == "CE20C3EE-570B-4AD7-9EA0-311E6F2864BD")
            {
                returnButton.Name = "Menu_RecBrochure_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_RecBrochure_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            #region Calendar button creation
            if (m.id == "2D232378-36B8-47F6-A091-92213B84769A")
            {
                returnButton.Name = "Menu_Calendar_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_Calendar_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            #region Reciprocal Clubs button creation
            if (m.id == "B378209E-DCBD-46A6-9138-18E5C97E0A08")
            {
                returnButton.Name = "Menu_ReciprocalClubs_Button";
                returnButton.Tapped += MainMenu.mainMenu.Menu_Calendar_Button_Tapped;

                iconTextblock.Text = m.glyph;
                labelTextblock.Text = m.menuItem;
            }
            #endregion

            buttonStackpanel.Children.Add(iconTextblock);
            buttonStackpanel.Children.Add(labelTextblock);
            returnButton.Content = buttonStackpanel;

            return returnButton;
        }
        #endregion
    }
}
