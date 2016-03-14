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
                        int indSpNum = 0;
                        double trainerGrid = (status.main.Length / 3);
                        int numOfGrid = (int)Math.Ceiling(trainerGrid);
                        
                        StackPanel indSP = new StackPanel();
                        indSP.Orientation = Orientation.Horizontal;
                        indSP.VerticalAlignment = VerticalAlignment.Center;

                        StackPanel[] spArray = new StackPanel[2];
                        List<Trainer_Card> cardList = new List<Trainer_Card>();
                        List<StackPanel> spList = new List<StackPanel>();

                        foreach (Trainer t in status.main)
                        {
                            Trainer_Card card = new Trainer_Card();
                            card.TrainerName = t.name;
                            card.Degree = t.degree;
                            card.YearsExp = t.years;
                            card.YearsBC = t.years_bc;
                            card.Exp = t.expertise;
                            card.TrainerPhotoURL = t.photo;
                            card.Tag = t;
                            card.Width = 450;
                            card.Height = 800;
                            card.Margin = new Thickness(31, 0, 31, 0);

                            cardList.Add(card);
                            //indSP.Children.Add(card);

                            #region scraps
                            //Trainer_Flipview.Items.Add(card);

                            //if (indCardNum % 3 == 0)
                            //{
                            //    //indSP.Name = "SP_" + indSpNum;

                            //    Trainer_Flipview.Items.Add(card);
                            //    indSpNum++;
                            //    indSP.Children.Clear();
                            //}
                            #endregion

                            //if (indSP.Children.Count == 3)
                            //{
                            //    indSP.Name = "SP_" + indSpNum;

                            //    spList.Add(indSP);
                            //    //spArray[indSpNum] = indSP;
                            //    //Trainer_Flipview.Items.Add(indSP);
                            //    indSpNum++;
                            //    //indSP.Children.Clear();
                            //}

                            //indCardNum++;
                        }

                        foreach (Trainer_Card card in cardList)
                        {
                            card.Name = "Card_" + indCardNum;
                            indSP.Children.Add(card);

                            if (indCardNum == 2)
                            {
                                indSP.Name = "IndSp_" + indSpNum;
                                spList.Add(indSP);
                                indSpNum++;
                                indCardNum = -1;
                                indSP.Children.Clear();
                            }

                            indCardNum++;
                        }

                        foreach (StackPanel sp in spList)
                        {
                            Debug.WriteLine("Children: " + sp.Children.Count);
                            //Trainer_Flipview.Items.Add(sp);
                        }
                    }
                }
            }
        }
    }
}
