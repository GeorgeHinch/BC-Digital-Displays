using BC_Digital_Displays.Classes;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
            Card_Template.ClassAgeDayTime = thisClass.ageRange + ", " + DataBuilder.dayBuilder(thisClass.days) + ", " + DataBuilder.timeBuilder(thisClass.time);
            Card_Template.ClassSession = DataBuilder.sessionBuilder(thisClass.sessions);
            Card_Template.ClassDescription = thisClass.description;
            
            #region Disable session buttons based on class data
            if (thisClass.sessions[0].ToString() == "0")
            {
                tbSession1.IsEnabled = false;
            }
            if (thisClass.sessions[1].ToString() == "0")
            {
                tbSession2.IsEnabled = false;
            }
            if (thisClass.sessions[2].ToString() == "0")
            {
                tbSession3.IsEnabled = false;
            }
            if (thisClass.sessions[3].ToString() == "0")
            {
                tbSession4.IsEnabled = false;
            }
            if (thisClass.sessions[4].ToString() == "0")
            {
                tbSession5.IsEnabled = false;
            }
            #endregion
        }
        #endregion
        public Class_Preview()
        {
            this.InitializeComponent();

            Gradient_Background.Blur(duration: 10, delay: 0, value: 10).Start();

            var task = Task.Run(async () => { await findBrochure(); });
            task.Wait();

            buildSessions();
        }

        public void buildSessions()
        {
            List<bcSessions> sessions = JsonConvert.DeserializeObject<List<bcSessions>>(thisBrochure.sessions);
            Session1_data.Text = sessions[0].name + ": " + Convert.ToDateTime(sessions[0].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(sessions[0].end).ToString("MMMM d, yyyy");
            Session2_data.Text = sessions[1].name + ": " + Convert.ToDateTime(sessions[1].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(sessions[1].end).ToString("MMMM d, yyyy");
            Session3_data.Text = sessions[2].name + ": " + Convert.ToDateTime(sessions[2].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(sessions[2].end).ToString("MMMM d, yyyy");
            Session4_data.Text = sessions[3].name + ": " + Convert.ToDateTime(sessions[3].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(sessions[3].end).ToString("MMMM d, yyyy");
            Session5_data.Text = sessions[4].name + ": " + Convert.ToDateTime(sessions[4].start).ToString("MMMM d, yyyy") + " - " + Convert.ToDateTime(sessions[4].end).ToString("MMMM d, yyyy");
        }

        private void CloseEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
        }
        
        private void EmailSent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "emailSent_click", "(" + thisClass.name + ") Email: " + thisClass.name, (long)thisClass.category);

            string emailSubject = "Bellevue Club Details for " + thisClass.name;
            string emailBody = DataBuilder.emailRecClassBuilder(thisClass, thisBrochure, (bool)tbSession1.IsChecked, (bool)tbSession2.IsChecked, (bool)tbSession3.IsChecked, (bool)tbSession4.IsChecked, (bool)tbSession5.IsChecked);
            List<string> emailAttachments = DataBuilder.icsBuilder(thisClass, thisBrochure, (bool)tbSession1.IsChecked, (bool)tbSession2.IsChecked, (bool)tbSession3.IsChecked, (bool)tbSession4.IsChecked, (bool)tbSession5.IsChecked);
            EmailSender.emailSender(userEmailTB.Text, emailSubject, emailBody, thisClass.name, emailAttachments);

            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
            YouthBrochure.youthBrochure.classCard_Frame.Visibility = Visibility.Collapsed;
        }

        private MobileServiceCollection<bcRecBrochure, bcRecBrochure> items;
        private IMobileServiceTable<bcRecBrochure> bcRecBrochureTable = App.MobileService.GetTable<bcRecBrochure>();

        bcRecBrochure thisBrochure;
        public async Task findBrochure()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await bcRecBrochureTable
                    .Where(aBrochure => aBrochure.isActive == true)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                Debug.WriteLine("Exception: " + exception.Message + " | ");
            }

            if (exception != null)
            {
                Debug.WriteLine("Exception: " + exception.Message + " | ");
            }
            else
            {
                IEnumerable<bcRecBrochure> itemsControl = items;

                thisBrochure = items[0];
            }
        }
    }
}
