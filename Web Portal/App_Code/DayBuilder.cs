using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for DayBuilder
/// </summary>
public class DayBuilder
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

            // Week start: M (d[0])
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
}