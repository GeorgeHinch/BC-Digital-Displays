using LightBuzz.SMTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.Storage.Streams;

namespace BC_Digital_Displays.Classes
{
    public class EmailSender
    {
        public static async Task emailSender(string recipient, string subject, string body, string attachmentName, List<string> attatchments)
        {
            try
            {
                SmtpClient client = new SmtpClient("nemesis.bellevueclub.com", 25, false, "georgeh@bellevueclub.com", "Communications1");
                EmailMessage emailMessage = new EmailMessage();

                emailMessage.To.Add(new EmailRecipient(recipient));
                emailMessage.Subject = subject;
                emailMessage.Body = body;
                int attachNum = 1;
                foreach (string s in attatchments)
                {
                    string attatchName = attachmentName + "_" + attachNum.ToString() + ".ics";
                    var bytes = Encoding.UTF8.GetBytes(s);
                    MemoryStream stream = new MemoryStream(bytes);
                    //emailMessage.Attachments.Add(new EmailAttachment(attatchName, RandomAccessStreamReference.CreateFromStream(stream), "text/calendar"));

                    attachNum++;
                }

                await client.SendMail(emailMessage);
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message + " | ");
                throw;
            }
            
        }

        public static async Task emailSender(string recipient, string subject, string body, string attachmentName, string attatchments)
        {
            // TODO: send email with event
        }
    }
}
