using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
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
    public sealed partial class MenuSelect : Page
    {
        public MenuSelect()
        {
            this.InitializeComponent();
        }

        private void key_Click(object sender, RoutedEventArgs e)
        {
            if (MenuSelect_Textbox.Text.Length <= 5)
            {
                Button btn = (Button)e.OriginalSource;
                string s = btn.Content.ToString();
                MenuSelect_Textbox.Text += s;
            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if(MenuSelect_Textbox.Text != null)
            {
                var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

                /* Store Selected Menu */
                roamingSettings.Values["SelectedMenu"] = MenuSelect_Textbox.Text;

                //MainPage.mainPage.Options_Frame.Navigate(typeof(Page));
                //MainPage.mainPage.refreshPage();
            }
            else
            {
                Error_Bar.Visibility = Visibility.Visible;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            //MainPage.mainPage.Options_Frame.Navigate(typeof(Page));
        }
    }
}
