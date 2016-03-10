using BC_Digital_Displays.Cards;
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
    public sealed partial class TrainerFlipview : Page
    {
        public TrainerFlipview()
        {
            this.InitializeComponent();

            LoadTrainers();
        }

        public void LoadTrainers()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Trainers.txt");
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Trainers));
                        Trainers status = (Trainers)serializer.ReadObject(stream);

                        int indCardNum = 0;
                        int indGridNum = 1;
                        //double trainerGrid = (status.main.Length / 3);
                        //int numOfGrid = (int)Math.Ceiling(trainerGrid);
                        
                        //StackPanel[] sp = new StackPanel[numOfGrid];
                        StackPanel indSP = new StackPanel();

                        foreach (Trainer t in status.main)
                        {
                            Debug.WriteLine("Made it here.");
                            Trainer_Card card = new Trainer_Card();
                            card.TrainerName = t.name;
                            card.YearsExp = t.years;
                            card.YearsBC = t.years_bc;
                            card.Exp = t.expertise;
                            card.TrainerPhotoURL = t.photo;
                            card.Tag = t;

                            indSP.Children.Add(card);
                            indSP.Name = "SP_" + indGridNum;

                            if (indCardNum % 3 == 0)
                            {
                                Debug.WriteLine("Every third.");
                                Trainer_Flipview.Items.Add(indSP);
                                indGridNum++;
                                indSP.Children.Clear();
                            }

                            indCardNum++;
                        }
                    }
                }
            }
        }
    }
}
