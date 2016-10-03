using BC_Digital_Displays.Classes;
using Syncfusion.UI.Xaml.Gauges;
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
    public sealed partial class EquipmentPreview : Page
    {
        public EquipmentPreview()
        {
            this.InitializeComponent();

            // Remove sample gauge ranges from the xaml before enabling
            //checkMachineStudio();
        }

        #region Set Color Brushes
        public SolidColorBrush Alizarin = new SolidColorBrush(Color.FromArgb(255, 231, 76, 60));
        public SolidColorBrush SunFlower = new SolidColorBrush(Color.FromArgb(255, 241, 196, 15));
        public SolidColorBrush PeterRiver = new SolidColorBrush(Color.FromArgb(255, 52, 152, 219));
        public SolidColorBrush Clouds = new SolidColorBrush(Color.FromArgb(255, 236, 240, 241));
        #endregion

        #region Deserializes Machine Locations
        public void checkMachineStudio()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Machines.txt");
            HttpClient client = new HttpClient();
            if (internet == false)
            {
                Debug.WriteLine("No internet");
            }
            else
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Machines));
                        Machines status = (Machines)serializer.ReadObject(stream);

                        Studios studio = (Studios)status.main;

                        checkMachineStatus(studio);
                    }
                }
            }
        }
        #endregion

        #region Checks the status of each machine
        public void checkMachineStatus(Studios s)
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://bc-machine-status.firebaseio.com/kimono/api/3rgdu3xk/latest.json?auth=LHhnx6MUimP9hJEFZ3qPmsrPMsLVKs0lkKAyhLou");
            HttpClient client = new HttpClient();
            if (internet == false)
            {
                Debug.WriteLine("No internet");
            }
            else
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(PrecorStatus));
                        PrecorStatus status = (PrecorStatus)serializer.ReadObject(stream);

                        double studio1per;
                        double studio1fin;
                        double studio2per;
                        double studio2fin;
                        double studio3per;
                        double studio3fin;
                        double studio4per;
                        double studio4fin;

                        SolidColorBrush Studio1Brush;
                        SolidColorBrush Studio2Brush;
                        SolidColorBrush Studio3Brush;
                        SolidColorBrush Studio4Brush;

                        #region Compare Machine Names and Studio Locaiton, Then Set Count
                        var activeMachines = from machine in status.results.Preva where machine.Status.Equals("In Use") select machine.Name.text;
                        HashSet<String> setOfMachines = new HashSet<string>(activeMachines);

                        int s1active = getNumberOfMachineActive(s.Studio1, setOfMachines);
                        int s1Total = s.Studio1.Count;

                        int s2active = getNumberOfMachineActive(s.Studio2, setOfMachines);
                        int s2Total = s.Studio2.Count;

                        int s3active = getNumberOfMachineActive(s.Studio3, setOfMachines);
                        int s3Total = s.Studio3.Count;

                        int s4active = getNumberOfMachineActive(s.Studio4, setOfMachines);
                        int s4Total = s.Studio4.Count;
                        #endregion

                        #region Percentage Math
                        if (s1Total != 0)
                        {
                            studio1per = ((double)s1active / (double)s1Total) * 100;
                            studio1fin = Math.Round(studio1per, 0);
                        }
                        else { studio1fin = 0; }

                        if (s2Total != 0)
                        {
                            studio2per = ((double)s2active / (double)s2Total) * 100;
                            studio2fin = Math.Round(studio2per, 0);
                        }
                        else { studio2fin = 0; }

                        if (s3Total != 0)
                        {
                            studio3per = ((double)s3active / (double)s3Total) * 100;
                            studio3fin = Math.Round(studio3per, 0);
                        }
                        else { studio3fin = 0; }

                        if (s4Total != 0)
                        {
                            studio4per = ((double)s4active / (double)s4Total) * 100;
                            studio4fin = Math.Round(studio4per, 0);
                        }
                        else { studio4fin = 0; }
                        #endregion

                        #region Set Scales
                        #region Studio 1 Scale
                        CircularScale _mainscale1 = new CircularScale();
                        if (studio1fin >= 0 && studio1fin <= 74)
                        {
                            Studio1Brush = PeterRiver;
                        }
                        else if (studio1fin >= 75 && studio1fin <= 89)
                        {
                            Studio1Brush = SunFlower;
                        }
                        else if (studio1fin >= 90 && studio1fin <= 100)
                        {
                            Studio1Brush = Alizarin;
                        }
                        else { Studio1Brush = Clouds; }

                        _mainscale1.Ranges.Add(new CircularRange()
                        {
                            StartValue = 0,
                            EndValue = studio1fin,
                            Stroke = Studio1Brush
                        });
                        _mainscale1.Ranges.Add(new CircularRange()
                        {
                            StartValue = studio1fin,
                            EndValue = 100,
                            Stroke = Clouds
                        });
                        Studio1Header.Text = studio1fin + "%";
                        _mainscale1.StartValue = 0;
                        _mainscale1.EndValue = 100;
                        _mainscale1.TickStrokeThickness = 0;
                        _mainscale1.TickLength = 0;
                        _mainscale1.SmallTickLength = 0;
                        _mainscale1.SmallTickStrokeThickness = 0;
                        _mainscale1.Interval = 100;
                        Studio1_Gauge.Scales.Add(_mainscale1);
                        #endregion

                        #region Studio 2 Scale
                        CircularScale _mainscale2 = new CircularScale();
                        if (studio2fin >= 0 && studio2fin <= 74)
                        {
                            Studio2Brush = PeterRiver;
                        }
                        else if (studio2fin >= 75 && studio2fin <= 89)
                        {
                            Studio2Brush = SunFlower;
                        }
                        else if (studio2fin >= 90 && studio2fin <= 100)
                        {
                            Studio2Brush = Alizarin;
                        }
                        else { Studio2Brush = Clouds; }

                        _mainscale2.Ranges.Add(new CircularRange()
                        {
                            StartValue = 0,
                            EndValue = studio2fin,
                            Stroke = Studio2Brush
                        });
                        _mainscale2.Ranges.Add(new CircularRange()
                        {
                            StartValue = studio2fin,
                            EndValue = 100,
                            Stroke = Clouds
                        });
                        Studio2Header.Text = studio2fin + "%";
                        _mainscale2.StartValue = 0;
                        _mainscale2.EndValue = 100;
                        _mainscale2.TickStrokeThickness = 0;
                        _mainscale2.TickLength = 0;
                        _mainscale2.SmallTickLength = 0;
                        _mainscale2.SmallTickStrokeThickness = 0;
                        _mainscale2.Interval = 100;
                        Studio2_Gauge.Scales.Add(_mainscale2);
                        #endregion

                        #region Studio 3 Scale
                        CircularScale _mainscale3 = new CircularScale();
                        if (studio3fin >= 0 && studio3fin <= 74)
                        {
                            Studio3Brush = PeterRiver;
                        }
                        else if (studio3fin >= 75 && studio3fin <= 89)
                        {
                            Studio3Brush = SunFlower;
                        }
                        else if (studio3fin >= 90 && studio3fin <= 100)
                        {
                            Studio3Brush = Alizarin;
                        }
                        else { Studio3Brush = Clouds; }

                        _mainscale3.Ranges.Add(new CircularRange()
                        {
                            StartValue = 0,
                            EndValue = studio3fin,
                            Stroke = Studio3Brush
                        });
                        _mainscale3.Ranges.Add(new CircularRange()
                        {
                            StartValue = studio3fin,
                            EndValue = 100,
                            Stroke = Clouds
                        });
                        Studio3Header.Text = studio3fin + "%";
                        _mainscale3.StartValue = 0;
                        _mainscale3.EndValue = 100;
                        _mainscale3.TickStrokeThickness = 0;
                        _mainscale3.TickLength = 0;
                        _mainscale3.SmallTickLength = 0;
                        _mainscale3.SmallTickStrokeThickness = 0;
                        _mainscale3.Interval = 100;
                        Studio3_Gauge.Scales.Add(_mainscale3);
                        #endregion

                        #region Studio 4 Scale
                        CircularScale _mainscale4 = new CircularScale();
                        if (studio4fin >= 0 && studio4fin <= 74)
                        {
                            Studio4Brush = PeterRiver;
                        }
                        else if (studio4fin >= 75 && studio4fin <= 89)
                        {
                            Studio4Brush = SunFlower;
                        }
                        else if (studio4fin >= 90 && studio4fin <= 100)
                        {
                            Studio4Brush = Alizarin;
                        }
                        else { Studio4Brush = Clouds; }

                        _mainscale4.Ranges.Add(new CircularRange()
                        {
                            StartValue = 0,
                            EndValue = studio4fin,
                            Stroke = Studio4Brush
                        });
                        _mainscale4.Ranges.Add(new CircularRange()
                        {
                            StartValue = studio4fin,
                            EndValue = 100,
                            Stroke = Clouds
                        });
                        Studio4Header.Text = studio4fin + "%";
                        _mainscale4.StartValue = 0;
                        _mainscale4.EndValue = 100;
                        _mainscale4.TickStrokeThickness = 0;
                        _mainscale4.TickLength = 0;
                        _mainscale4.SmallTickLength = 0;
                        _mainscale4.SmallTickStrokeThickness = 0;
                        _mainscale4.Interval = 100;
                        Studio4_Gauge.Scales.Add(_mainscale4);
                        #endregion
                        #endregion
                    }
                }
            }
        }
        #endregion

        #region Method to Compare Machine Lists to Determine Studio
        public int getNumberOfMachineActive(IList<String> sutdioMachines, HashSet<String> activeMachines)
        {
            int active = 0;
            foreach (String m in sutdioMachines)
            {
                if (activeMachines.Contains(m))
                {
                    active++;
                }
            }
            return active;
        }
        #endregion

        private void GoBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainMenu.mainMenu.menuFlipView.Visibility = Visibility.Visible;
            MainMenu.mainMenu.mainFrame.Navigate(typeof(Page));
        }
    }
}
