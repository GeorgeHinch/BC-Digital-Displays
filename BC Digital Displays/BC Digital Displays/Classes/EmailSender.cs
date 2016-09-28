using LightBuzz.SMTP;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.Storage.Streams;
using Windows.UI.Notifications;

namespace BC_Digital_Displays.Classes
{
    public class EmailSender
    {
        //public SmtpClient client = new SmtpClient("nemesis.bellevueclub.com", 25, false, "georgeh@bellevueclub.com", "Communications1");
        public static SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 25, false, "ghtestacc@outlook.com", "!testacc!");
        public static async Task emailSender(string recipient, string subject, string body, string attachmentName, List<string> attatchments)
        {
            try
            {
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
                EmailSender.popToast();
            }
            catch(Exception e)
            {
                GoogleAnalytics.EasyTracker.GetTracker().SendException(e.Message, false);
                Debug.WriteLine("Exception: " + e.Message + " | ");
            }
        }

        public static async Task emailSender(string recipient, string subject, string body, string attachmentName, string attatchments)
        {
            try
            {
                EmailMessage emailMessage = new EmailMessage();

                emailMessage.To.Add(new EmailRecipient(recipient));
                emailMessage.Subject = subject;
                emailMessage.Body = body;
                int attachNum = 1;

                string attatchName = attachmentName + "_" + attachNum.ToString() + ".ics";

                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile icsFile = await storageFolder.CreateFileAsync(attatchName,
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.WriteTextAsync(icsFile, attatchments);

                emailMessage.Attachments.Add(new EmailAttachment(attatchName, RandomAccessStreamReference.CreateFromFile(icsFile), "text/calendar"));

                attachNum++;

                await client.SendMail(emailMessage);
                EmailSender.popToast();
            }

            catch (Exception e)
            {
                GoogleAnalytics.EasyTracker.GetTracker().SendException(e.Message, false);
                Debug.WriteLine("Exception: " + e.Message + " | ");
            }
        }

        public static void popToast()
        {
            // Generate the toast notification content and pop the toast
            ToastContent content = GenerateToastContent();
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }

        public static ToastContent GenerateToastContent()
        {
            return new ToastContent()
            {
                Scenario = ToastScenario.Default,

                Duration = ToastDuration.Short,

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Email Sent"
                            },

                            new AdaptiveText()
                            {
                                Text = "You will recieve an email from the Bellevue Club within the next few minutes."
                            }
                        }
                    }
                }
            };
        }
    }
}
