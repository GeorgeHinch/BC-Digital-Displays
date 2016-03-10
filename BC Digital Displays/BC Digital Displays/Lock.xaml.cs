using BC_Digital_Displays.Classes;
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
    public sealed partial class Lock : Page
    {
        public Lock()
        {
            this.InitializeComponent();
        }

        private void key_Click(object sender, RoutedEventArgs e)
        {
            if (Password_Box.Password.Length <= 5)
            {
                Button btn = (Button)e.OriginalSource;
                string s = btn.Content.ToString();
                Password_Box.Password += s;
            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Settings.txt");
            HttpClient client = new HttpClient();
            Debug.WriteLine(client);
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Settings));
                        Settings status = (Settings)serializer.ReadObject(stream);

                        if(Password_Box.Password == status.pass)
                        {
                            //Navigate to options
                            MainPage.mainPage.Options_Frame.Navigate(typeof(OptionsMenu));
                        }
                        else
                        {
                            Error_Bar.Visibility = Visibility.Visible;
                            Password_Box.Password = "";
                        }
                    }
                }
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.Options_Frame.Navigate(typeof(Page));
        }
    }
}
