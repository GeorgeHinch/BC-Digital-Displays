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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

    // TODO: Pass values through GA strings

namespace BC_Digital_Displays.Cards
{
    public sealed partial class Class_Card : UserControl
    {
        public Class_Card()
        {
            this.InitializeComponent();
        }

        private void SendEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "email_click", "(" + this.Name + ") Email: " + this.Name, 0);

            Class_Preview.classPreview.Class_Template.Visibility = Visibility.Collapsed;
            Class_Preview.classPreview.Email_Template.Visibility = Visibility.Visible;

        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "close_click", "(" + this.Name + ") Email: " + this.Name, 0);

            YouthBrochure.youthBrochure.classCard_Frame.Visibility = Visibility.Collapsed;
        }
    }
}
