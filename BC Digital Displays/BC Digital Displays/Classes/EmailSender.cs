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
                //SmtpClient client = new SmtpClient("nemesis.bellevueclub.com", 25, false, "georgeh@bellevueclub.com", "Communications1");
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 25, false, "ghtestacc@outlook.com", "!testacc!");
                EmailMessage emailMessage = new EmailMessage();

                emailMessage.To.Add(new EmailRecipient(recipient));
                emailMessage.Subject = subject;
                emailMessage.Body = body;
                int attachNum = 1;
                foreach (string s in attatchments)
                {
                    string attatchName = attachmentName + "_" + attachNum.ToString() + ".ics";

                    Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile icsFile = await storageFolder.CreateFileAsync(attatchName,
                            Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    await Windows.Storage.FileIO.WriteTextAsync(icsFile, s);

                    emailMessage.Attachments.Add(new EmailAttachment(attatchName, RandomAccessStreamReference.CreateFromFile(icsFile), "text/calendar"));

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
