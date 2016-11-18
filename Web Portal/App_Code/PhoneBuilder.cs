using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Twilio.Lookups;

/// <summary>
/// Summary description for PhoneBuilder
/// </summary>
public class PhoneBuilder
{
    public static string buildPhoneNumber(string num, string reg)
    {
        string returnNum = "";
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        try
        {
            PhoneNumber phoneNum = phoneUtil.Parse(num, reg);

            if (reg == "US")
            {
                returnNum = phoneUtil.Format(phoneNum, PhoneNumberFormat.NATIONAL);
            }
            else
            {
                returnNum = phoneUtil.Format(phoneNum, PhoneNumberFormat.INTERNATIONAL);
            }
        }
        catch (NumberParseException e)
        {
            Debug.WriteLine("Failure parsing phone number.");
            Debug.WriteLine(e.Message);
        }

        return returnNum;
    }
}