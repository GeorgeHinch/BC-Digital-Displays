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
            try
            {
                var task = Task.Run(async () => { await loadNotifications(); });
                task.Wait();

                if(items.Count != 0)
                {
                    IEnumerable<bcNotification> itemsControl = items;
                    foreach (bcNotification i in items)
                    {
                        NotificationView nV = new NotificationView();
                        nV.GlyphType = i.glyph;
                        nV.Subject = i.subject;
                        nV.Message = i.message;
                        nV.Tag = i;

                        Notification_StackPanel.Children.Add(nV);
                    }
                }
                else
                {
                    Notifications_None.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message + " | ");
            }
        }

        #region Load notifications
        private MobileServiceCollection<bcNotification, bcNotification> items;
        private IMobileServiceTable<bcNotification> bcNotificationTable = App.MobileService.GetTable<bcNotification>();
        public async Task loadNotifications()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await bcNotificationTable
                    .Where(aNotification => aNotification.deleted == false)
                    .Where(aNotification => aNotification.startDate <= DateTime.Now)
                    .Where(aNotification => aNotification.endDate >= DateTime.Now)
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
