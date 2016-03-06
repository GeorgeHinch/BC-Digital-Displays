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
    public sealed partial class EquipmentPreview : Page
    {
        public EquipmentPreview()
        {
            this.InitializeComponent();

            checkMachineStatus();
        }

        public void checkMachineStatus()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://bc-machine-status.firebaseio.com/kimono/api/czvzv1jg/latest.json?auth=LHhnx6MUimP9hJEFZ3qPmsrPMsLVKs0lkKAyhLou");
            HttpClient client = new HttpClient();
            Debug.WriteLine(client);
            if (internet == false)
            {
                Debug.WriteLine("No internet");
            }
            else
            {
                var response = client.SendAsync(request).Result;
                Debug.WriteLine(response);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(PrecorStatus));
                        PrecorStatus status = (PrecorStatus)serializer.ReadObject(stream);

                        Debug.WriteLine("Count: " + status.count + " |");
                        Debug.WriteLine("Version: " + status.version + " |");
                        Debug.WriteLine("Stusio 1 Active: " + status.studio1active + " |");

                        foreach (PrevaResults pr in status.results.Preva)
                        {
                            Debug.WriteLine("Name: " + pr.Name + " | Status: " + pr.Status + " |");
                        }
                    }
                }
            }
        }
    }
}
