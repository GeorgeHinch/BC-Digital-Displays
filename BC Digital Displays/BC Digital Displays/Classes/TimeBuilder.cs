﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    class TimeBuilder
    {
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
    }
}
