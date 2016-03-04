using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Windows.UI;
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
            LoadCalendarEvents();
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

        public void LoadCalendarEvents()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Schedule.txt");
            HttpClient client = new HttpClient();
            Debug.WriteLine(client);
            if (internet == false)
            {
                NoInternetAlert();
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
                        var serializer = new DataContractJsonSerializer(typeof(Sched));
                        Debug.WriteLine("Stream: " + stream.ToString());
                        Sched events = (Sched)serializer.ReadObject(stream);

                        for (int i = 0; i < events.main.Length; ++i)
                        {
                            Event current = events.main[i];
                            //Debug.WriteLine($"Subject: {current.Subject_E}, Location: {current.Location_E}, Notes: {current.Notes_E}, Instructor: {current.Instructor_E}, Department: {current.Department_E}, StartTime: {current.StartTime_E}, EndTime: {current.EndTime_E}, AllDay: {current.AllDay_E}.");

                            Appointment newappointment = new Appointment();
                            DateTime start = DateTime.ParseExact(current.StartTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
                            DateTime end = DateTime.ParseExact(current.EndTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
                            Debug.WriteLine("Start Time: " + start);
                            newappointment.StartTime = start;
                            newappointment.EndTime = end;
                            newappointment.Subject = current.Subject;
                            newappointment.Location = current.Location;
                            newappointment.Notes = current.Notes;
                            newappointment.TimeStart = String.Format("{0:t}", newappointment.StartTime);
                            newappointment.TimeEnd = String.Format("{0:t}", newappointment.EndTime);
                            newappointment.DayStart = String.Format("{0:m}", newappointment.StartTime);
                            newappointment.DayEnd = String.Format("{0:m}", newappointment.EndTime);
                            newappointment.DaySpan = newappointment.DayStart;
                            if (newappointment.DayStart != newappointment.DayEnd)
                            {
                                newappointment.DaySpan = newappointment.DayStart + "-" + newappointment.EndTime.Day.ToString();
                            }
                            newappointment.Instructor = current.Instructor;
                            if(current.Department == "Aquatics")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 52, 152, 219));
                            }
                            if (current.Department == "Fitness")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 155, 89, 182));
                            }
                            if (current.Department == "Food & Beverage")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 230, 126, 34));
                            }
                            if (current.Department == "Member Events")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 234, 76, 136));
                            }
                            if (current.Department == "Recreation")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 197, 57, 43));
                            }
                            if (current.Department == "Tennis")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 39, 174, 96));
                            }
                            newappointment.Department = current.Department;
                            newappointment.Price = current.Price;
                            newappointment.FlierJPG = current.FlierJPG;
                            if (current.FlierJPG == null || current.FlierJPG == "")
                            {
                                newappointment.Flier = new BitmapImage();
                            }
                            else
                            {
                                newappointment.Flier = new BitmapImage(new Uri(current.FlierJPG, UriKind.Absolute));
                            }
                            newappointment.AllDay = current.AllDay;
                            if(current.Instructor == null)
                            {
                                newappointment.Info = current.Department + " | " + current.Price;
                            }
                            else
                            {
                                newappointment.Info = current.Department + " | " + current.Instructor + " | " + current.Price;
                            }
                            //newappointment.ReadOnly = true;
                            SfCalendarView.Appointments.Add(newappointment);
                        }
                    }
                }
            }
        }

        public void LoadSampleCalendarEvents()
        {
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            ScheduleAppointment app = new ScheduleAppointment()
            {
                StartTime = new DateTime(year, month, day, 5, 0, 0),
                EndTime = new DateTime(year, month, day, 6, 30, 0),
                Subject = "Mobile UX Workshop",
                Location = "Kids' Camp Room",
                Notes = "Kids learn what it takes to make user-friendly websites and mobile applications. This workshop will start off with students learning about the stages of product development and using the Activity Scenario Method.",
                AllDay = false,
                ReadOnly = true         
            };
            ScheduleAppointment app1 = new ScheduleAppointment()
            {
                StartTime = new DateTime(year, month, day, 7, 0, 0),
                EndTime = new DateTime(year, month, day, 9, 30, 0),
                Subject = "Bellevue Club Mixer",
                Location = "Atrium",
                Notes = "Come network with fellow members and Bellevue Club staff at our mixer. Enjoy complimentary food, drink and mu-sic. Bring a guest for an introductory tour of the Club.",
                AllDay = false,
                ReadOnly = true
            };
            ScheduleAppointment app2 = new ScheduleAppointment()
            {
                StartTime = new DateTime(year, month, day, 7, 0, 0),
                EndTime = new DateTime(year, month, day, 9, 30, 0),
                Subject = "Feldenkrais Workshop: Releasing Neck and Shoulders",
                Location = "Yoga Studio",
                Notes = "Do stiff or painful neck and shoulders affect your experience of life? Learn innovative and relaxing exercises to improve comfort in your neck, shoulders and upper back. Experience less pain and more freedom in your movement. Prolong the benefits with simple exercises to practice anytime for on- the-spot results. $45/member",
                AllDay = false,
                ReadOnly = true,
            };
            
            SfCalendarView.AllowEditing = false;
            SfCalendarView.Appointments.Add(app);
            SfCalendarView.Appointments.Add(app1);
            SfCalendarView.Appointments.Add(app2);
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

        private void SfCalendarView_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.Cancel = true;
            ScheduleCommands.EditCommand.Execute(this.SfCalendarView);
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

    #region Appointment Class

    public class Appointment : ScheduleAppointment
    {
        #region Public Properties       

        public string TimeStart { get; set; }

        public string TimeEnd { get; set; }

        public string DaySpan { get; set; }

        public string DayStart { get; set; }

        public string DayEnd { get; set; }

        public string Instructor { get; set; }

        public string Department { get; set; }

        public string Price { get; set; }

        public string Info { get; set; }

        public string FlierJPG { get; set; }

        public BitmapImage Flier { get; set; }


        #endregion
    }
    #endregion

    public class Event
    {
        public string Subject { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Instructor { get; set; }

        public string Department { get; set; }

        public string Price { get; set; }

        public string FlierJPG { get; set; }

        public bool AllDay { get; set; }
    }

    public class Sched
    {
        public Event[] main { get; set; }
    }
}
