using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Digital_Displays.Classes
{
    public class EmailSender
    {
        public static async Task emailSender(string recipient, string subject, string body, List<string> attatchments)
        {
            /*SmtpClient client = new SmtpClient("example.com", 25, false, "info@example.com", "Pa$$w0rd");
            EmailMessage emailMessage = new EmailMessage();
            
            emailMessage.To.Add(new EmailRecipient());
            emailMessage.Subject = "Subject line of your message";
            emailMessage.Body = "This is an email sent from a WinRT app!";
            foreach (string s in attatchments)
            {
                emailMessage.Attachments.Add(s);
            }

            await client.SendMail(emailMessage);/**/
        }
    }
}
