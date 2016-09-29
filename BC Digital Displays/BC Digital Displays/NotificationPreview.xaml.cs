using BC_Digital_Displays.Classes;
using BC_Digital_Displays.Controls;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BC_Digital_Displays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationPreview : Page
    {
        public static NotificationPreview notificatioPreview;
        public NotificationPreview()
        {
            this.InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(async () => { await loadNotifications(); });
            task.Wait();

            IEnumerable<bcNotification> itemsControl = items;
            foreach (bcNotification i in items)
            {
                NotificationView nV = new NotificationView();
                nV.Subject = i.subject;
                nV.Tag = i;
            }
        }

        #region Load notifications
        private MobileServiceCollection<bcNotification, bcNotification> items;
        private IMobileServiceTable<bcNotification> bcRecClassesTable = App.MobileService.GetTable<bcNotification>();
        public async Task loadNotifications()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await bcRecClassesTable
                    .Where(aBrochure => aBrochure.deleted == false)
                    .Where(aBrochure => aBrochure.startDate >= DateTime.Now)
                    .Where(aBrochure => aBrochure.endDate <= DateTime.Now)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                Debug.WriteLine("Exception: " + exception.Message + " | ");
                GoogleAnalytics.EasyTracker.GetTracker().SendException(e.Message, false);
            }
        }
        #endregion
    }
}
