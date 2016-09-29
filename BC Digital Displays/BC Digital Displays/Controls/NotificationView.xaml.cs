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
using Windows.UI.Xaml.Navigation;
using BC_Digital_Displays.Classes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BC_Digital_Displays.Controls
{
    public sealed partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            this.InitializeComponent();
        }

        private void MoreInfo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bcNotification thisNotification = (bcNotification)this.Tag;

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "open_click", "Open Notification: " + thisNotification.subject, 0);

            NotificationPreview.notificatioPreview.NotificationInfo_Frame.Visibility = Visibility.Visible;
        }

        #region Dependency Properties
        public static readonly DependencyProperty GlyphTypeProperty = DependencyProperty.Register(
            "GlyphType",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(NotificationView),     // The type of the owner of the DependencyProperty
            null
        );

        public string GlyphType
        {
            get { return (string)GetValue(GlyphTypeProperty); }
            set { SetValue(GlyphTypeProperty, value); }
        }

        public static readonly DependencyProperty SubjectProperty = DependencyProperty.Register(
            "Subject",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(NotificationView),     // The type of the owner of the DependencyProperty
            null
        );

        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }
        #endregion
    }
}
