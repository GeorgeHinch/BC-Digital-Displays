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
using Windows.UI;
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

        #region Load Trainers
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
                        
                        double trainerGrid = (status.main.Length / 3);
                        int numOfGrid = (int)Math.Ceiling(trainerGrid);
                        
                        List<Trainer_Card> cardList = new List<Trainer_Card>();

                        StackPanel indexSP = new StackPanel();
                        int indexVal = 1;
                        IList<StackPanel> spList = new List<StackPanel>(numOfGrid);

                        int tbVal = 0;

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

                            if(card.TrainerName == null)
                            {
                                card.Visibility = Visibility.Collapsed;
                            }

                            cardList.Add(card);
                        }

                        foreach (Trainer_Card card in cardList)
                        {
                            indexSP.Children.Add(card);

                            if (indexVal % 3 == 0)
                            {
                                spList.Add(indexSP);
                                indexSP = new StackPanel();
                            }

                            indexVal++;
                        }

                        foreach (StackPanel sp in spList)
                        {
                            Debug.WriteLine("Children: " + sp.Children.Count);

                            TextBlock tb = new TextBlock();
                            tb.Name = tbVal.ToString();
                            tb.Text = WebUtility.HtmlDecode("&#xEA3A;");
                            tb.FontFamily = new FontFamily("Segoe MDL2 Assets");
                            tb.TextAlignment = TextAlignment.Center;
                            tb.Margin = new Thickness(10, 0, 10, 0);
                            tb.Foreground = new SolidColorBrush(Color.FromArgb(127,255,255,255));
                            tb.Tapped += new TappedEventHandler(indicator_Clicked);
                            MainPage.mainPage.FlipviewIndicator_Stackpanel.Children.Add(tb);

                            tbVal++;

                            sp.Orientation = Orientation.Horizontal;
                            sp.VerticalAlignment = VerticalAlignment.Center;
                            sp.HorizontalAlignment = HorizontalAlignment.Center;

                            Trainer_Flipview.Items.Add(sp);
                        }
                    }
                }
            }
        }
        #endregion

        #region Creating Click Event for Flipview Indicators
        public void indicator_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (TextBlock t in MainPage.mainPage.FlipviewIndicator_Stackpanel.Children)
            {
                t.Text = WebUtility.HtmlDecode("&#xEA3A;");
                t.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            }

            TextBlock tb = (TextBlock)sender;
            tb.Text = WebUtility.HtmlDecode("&#xEA3B;");
            tb.Foreground = new SolidColorBrush(Color.FromArgb(191, 255, 255, 255));

            int tbValOut = Int32.Parse(tb.Name);

            Trainer_Flipview.SelectedIndex = tbValOut;
        }
        #endregion
    }
}
