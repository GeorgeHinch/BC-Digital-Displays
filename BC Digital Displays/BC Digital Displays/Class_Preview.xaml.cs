using BC_Digital_Displays.Classes;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BC_Digital_Displays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Class_Preview : Page
    {
        public static Class_Preview classPreview;

        bcRecClasses thisClass;
        #region Load Class Preview from Card.Tag on Page Load
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            thisClass = (bcRecClasses)e.Parameter;
            Card_Template.ClassName = thisClass.name;
            Card_Template.ClassAgeDayTime = thisClass.ageRange + ", " + DayBuilder.dayBuilder(thisClass.days) + ", " + TimeBuilder.timeBuilder(thisClass.time);
            Card_Template.ClassSession = SessionBuilder.sessionBuilder(thisClass.sessions);
            Card_Template.ClassDescription = thisClass.description;
        }
        #endregion
        public Class_Preview()
        {
            this.InitializeComponent();

            Gradient_Background.Blur(duration: 10, delay: 0, value: 10).Start();
        }

        private void CloseEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
        }

        private void SendEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailSent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "emailSent_click", "(" + this.Name + ") Email: " + this.Name, 0);

            /*SmtpClient client = new SmtpClient("example.com", 25, false, "info@example.com", "Pa$$w0rd");
            EmailMessage emailMessage = new EmailMessage();
            
            emailMessage.To.Add(new EmailRecipient());
            emailMessage.Subject = "Subject line of your message";
            emailMessage.Body = "This is an email sent from a WinRT app!";
            emailMessage.Attachments.Add(null);

            await client.SendMail(emailMessage);/**/

            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
            YouthBrochure.youthBrochure.classCard_Frame.Visibility = Visibility.Collapsed;
        }
    }
}
