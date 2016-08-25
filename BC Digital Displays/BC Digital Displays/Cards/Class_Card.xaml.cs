using BC_Digital_Displays.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            RecClass rec = (RecClass)this.Tag;
        }

        private void SendEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "email_click", "(" + this.Name + ") Email: " + this.Name, 0);

            this.Visibility = Visibility.Collapsed;
            Debug.WriteLine("Parent: " + this.Parent.ToString() +" |");
            
            Grid grCase = (Grid)this.Parent;
            Grid grCaseCase = (Grid)grCase.Parent;
            Grid neededGrid = (Grid)grCaseCase.FindName("Email_Template");
            neededGrid.Visibility = Visibility.Visible;
        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "close_click", "(" + this.Name + ") Email: " + this.Name, 0);

            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
            YouthBrochure.youthBrochure.classCard_Frame.Visibility = Visibility.Collapsed;
        }
    }
}
