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
using Windows.UI.Xaml.Documents;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BC_Digital_Displays.Controls
{
    public sealed partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            this.InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Paragraph richMessage = DataBuilder.markdownBuilder(this.Message);
            Notification_Message.Blocks.Add(richMessage);
        }

        private void MoreInfo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bcNotification thisNotification = (bcNotification)this.Tag;

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "open_click", "Open Notification: " + thisNotification.subject, 0);

            if (Notification_Message.Visibility == Visibility.Collapsed)
            {
                Notification_Message.Visibility = Visibility.Visible;
                Notification_Button.Text = "Less Info";
            }
            else
            {
                Notification_Message.Visibility = Visibility.Collapsed;
                Notification_Button.Text = "More Info";
            }
        }

        #region Dependency Properties
        public static readonly DependencyProperty GlyphTypeProperty = DependencyProperty.Register(
            "Glyph",                   // The name of the DependencyProperty
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

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(NotificationView),     // The type of the owner of the DependencyProperty
            null
        );

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion
    }
}
