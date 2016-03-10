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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BC_Digital_Displays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OptionsMenu : Page
    {
        public OptionsMenu()
        {
            this.InitializeComponent();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.refreshPage();
            MainPage.mainPage.Options_Frame.Navigate(typeof(Page));
        }

        private void menuSelect_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.Options_Frame.Navigate(typeof(MenuSelect));
        }
    }
}
