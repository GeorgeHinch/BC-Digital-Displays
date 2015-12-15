using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BC_Digital_Displays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler<object>(dispatchTimer_Tick);
            timer.Start();

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("main");

            LoadBackgroundImage();
            LoadMainMenu();
            LoadBCLogo();
        }

        void dispatchTimer_Tick(object sender, object e)
        {
            TimeBlock.Text = DateTime.Now.ToString("h" + ":" + "mm" + " " + "tt");
            DateBlock.Text = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");
        }

        public void LoadBCLogo()
        {
            BClogo.Source = new BitmapImage(
                        new Uri("https://pbs.twimg.com/profile_images/1080017859/New_BC_Box_Only.jpg", UriKind.Absolute));
        }

        public void LoadBackgroundImage()
        {
            ImageBrush background = new ImageBrush
            {
                ImageSource = new BitmapImage(
                        new Uri("http://www.bellevueclub.com/digital-signage/bc-background.jpg", UriKind.Absolute)),
                Stretch = Stretch.Fill
            };
            MainGrid.Background = background;
        }

        public void LoadMainMenu()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Display-Menu.txt");
            HttpClient client = new HttpClient();
            Debug.WriteLine(client);
            if (internet == false)
            {
                NoInternetAlert();
                //WebView.Source = new Uri("http://www.bellevueclub.com/");
                Debug.WriteLine("No internet");
            } else
            {
                var response = client.SendAsync(request).Result;
                Debug.WriteLine(response);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Menu));
                        Menu nav = (Menu)serializer.ReadObject(stream);

                        RadioButton[] radioButtons = new RadioButton[nav.main.Length];

                        for (int i = 0; i < nav.main.Length; ++i)
                        {
                            MenuItems current = nav.main[i];
                            Debug.WriteLine($"Text: {current.Text}, Icon: {current.Icon}, Link: {current.Link}, IsActive: {current.IsActive}.");

                            radioButtons[i] = new RadioButton();
                            radioButtons[i].Content = current.Text;
                            radioButtons[i].Tag = current;
                            radioButtons[i].GroupName = WebUtility.HtmlDecode(current.Icon);
                            radioButtons[i].IsChecked = current.IsActive;
                            radioButtons[i].Style = this.Resources["SplitViewNavButtonStyle"] as Style;
                            radioButtons[i].Checked += new RoutedEventHandler(radioButton_Checked);

                            this.NavStack.Children.Add(radioButtons[i]);
                        }
                    }
                }
            }
        }

        public class MenuItems
        {
            public string Text { get; set; }
            public string Icon { get; set; }
            public string Link { get; set; }
            public bool IsActive { get; set; }
        }

        public class Menu
        {
            public MenuItems[] main { get; set; }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs routedEventArgs)
        {
            string selectedContent = (string)((RadioButton)sender).Content;
            Debug.WriteLine(selectedContent);
            RadioButton radioButton = ((RadioButton)sender);
            MenuItems item = (MenuItems)radioButton.Tag;
            WebView.Source = new Uri(item.Link);

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", selectedContent, 0);
        }

        private void refreshPageButton(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "refresh_click", "Refresh", 0);

            refreshPage();
        }

        public void refreshPage()
        {
            this.NavStack.Children.Clear();
            LoadBackgroundImage();
            LoadMainMenu();
            LoadBCLogo();
        }

        private async void NoInternetAlert()
        {
            // Create a MessageDialog
            var messageDialog = new MessageDialog("This device is not connected to the internet. Until an active internet connection is established, the application can not continue. Check the network status and then retry.", "No Internet Connection");
            // Or create a separate callback for different commands

            messageDialog.Commands.Add(new UICommand(
                "Retry", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set CommandIndex. 0 means default.
            messageDialog.DefaultCommandIndex = 0;

            // Show MessageDialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            refreshPage();
        }
    }
}
