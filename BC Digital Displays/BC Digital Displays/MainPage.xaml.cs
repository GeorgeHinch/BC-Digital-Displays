using BC_Digital_Displays.Classes;
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
        public static MainPage mainPage;
        public MainPage()
        {
            this.InitializeComponent();
            mainPage = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler<object>(dispatchTimer_Tick);
            timer.Start();

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("main");

            ScheduleGrid.Visibility = Visibility.Collapsed;
            Trainer_Grid.Visibility = Visibility.Collapsed;
            TrainerCard_Frame.Navigate(typeof(TrainerFlipview));
            EquipmentPreview_Frame.Visibility = Visibility.Collapsed;
            EquipmentPreview_Frame.Navigate(typeof(EquipmentPreview));

            LoadSettings();
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

        public void LoadSettings()
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

                        var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

                        /* Store Profile Image */
                        roamingSettings.Values["ProfileImage"] = status.logo;

                        /* Store Background Image */
                        roamingSettings.Values["BackgroundImage"] = status.background;
                    }
                }
            }
        }

        public void LoadBCLogo()
        {
            // Loads profile image from Windows Roaming Settings
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string logo;
            if (roamingSettings.Values["ProfileImage"] != null)
            {
                logo = roamingSettings.Values["ProfileImage"].ToString();
            }
            else
            {
                logo = "https://pbs.twimg.com/profile_images/1080017859/New_BC_Box_Only.jpg";
            }

            // sets image url as BitmapImage
            BClogo.Source = new BitmapImage(
                        new Uri(logo, UriKind.Absolute));
        }

        public void LoadBackgroundImage()
        {
            // Loads background image from Windows Roaming Settings
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string backgroundImg;
            if (roamingSettings.Values["BackgroundImage"] != null)
            {
                backgroundImg = roamingSettings.Values["BackgroundImage"].ToString();
            }
            else
            {
                backgroundImg = "http://www.bellevueclub.com/digital-signage/bc-background.jpg";
            }

            // Sets image url as background brush
            ImageBrush background = new ImageBrush
            {
                ImageSource = new BitmapImage(
                        new Uri(backgroundImg, UriKind.Absolute)),
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
            //Debug.WriteLine(client);
            if (internet == false)
            {
                NoInternetAlert();
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
                        var serializer = new DataContractJsonSerializer(typeof(Sched));
                        Sched events = (Sched)serializer.ReadObject(stream);

                        for (int i = 0; i < events.main.Length; ++i)
                        {
                            Event current = events.main[i];

                            Appointment newappointment = new Appointment();
                            DateTime start = DateTime.ParseExact(current.StartTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
                            DateTime end = DateTime.ParseExact(current.EndTime, "yyyy, M, d, H, m, s", System.Globalization.CultureInfo.CurrentCulture);
                            newappointment.StartTime = start;
                            newappointment.EndTime = end;
                            newappointment.Subject = current.Subject;
                            newappointment.Location = current.Location;
                            newappointment.Notes = current.Description;
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
                            else if (current.Department == "Fitness")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 155, 89, 182));
                            }
                            else if (current.Department == "Food & Beverage")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 243, 156, 18));
                            }
                            else if (current.Department == "Member Events")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 234, 76, 136));
                            }
                            else if (current.Department == "Recreation")
                            {
                                newappointment.AppointmentBackground = new SolidColorBrush(Color.FromArgb(255, 197, 57, 43));
                            }
                            else if (current.Department == "Tennis")
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
                            SfCalendarView.Appointments.Add(newappointment);
                        }
                    }
                }
            }
        }

        public void LoadMainMenu()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Display-Menu.txt");
            HttpClient client = new HttpClient();
            //Debug.WriteLine(client);
            if (internet == false)
            {
                NoInternetAlert();
            } else
            {
                var response = client.SendAsync(request).Result;
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

                            radioButtons[i] = new RadioButton();
                            radioButtons[i].Content = current.Text;
                            radioButtons[i].Tag = current;
                            radioButtons[i].GroupName = WebUtility.HtmlDecode(current.Icon);
                            radioButtons[i].IsChecked = current.IsActive;
                            radioButtons[i].Style = this.Resources["SplitViewNavButtonStyle"] as Style;

                            if (current.Link == "*cal")
                            {
                                radioButtons[i].Checked += new RoutedEventHandler(radioButtonCal_Checked);
                            }
                            else if (current.Link == "*equip")
                            {
                                radioButtons[i].Checked += new RoutedEventHandler(radioButtonEquip_Checked);
                            }
                            else if (current.Link == "*trainer")
                            {
                                radioButtons[i].Checked += new RoutedEventHandler(radioButtonTrainer_Checked);
                            }
                            else
                            {
                                radioButtons[i].Checked += new RoutedEventHandler(radioButton_Checked);
                            }

                            this.NavStack.Children.Add(radioButtons[i]);
                        }

                        RadioButton schedule = new RadioButton();
                        schedule.Name = "Schedule_RB";
                        schedule.Content = "Schedule";
                        schedule.GroupName = WebUtility.HtmlDecode("&#xE787;");
                        schedule.Style = this.Resources["SplitViewNavButtonStyle"] as Style;
                        schedule.Checked += new RoutedEventHandler(radioButtonCal_Checked);

                        RadioButton equipment = new RadioButton();
                        equipment.Name = "Equipment_RB";
                        equipment.Content = "Equipment";
                        equipment.GroupName = WebUtility.HtmlDecode("&#xE1C4;");
                        equipment.Style = this.Resources["SplitViewNavButtonStyle"] as Style;
                        equipment.Checked += new RoutedEventHandler(radioButtonEquip_Checked);

                        this.NavStack.Children.Add(equipment);
                        this.NavStack.Children.Add(schedule);
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
            RadioButton radioButton = ((RadioButton)sender);
            MenuItems item = (MenuItems)radioButton.Tag;
            foreach (RadioButton rb in NavStack.Children)
            {
                if (radioButton.Content != rb.Content)
                {
                    rb.IsChecked = false;
                }
            }
            radioButton.IsChecked = true;

            ScheduleGrid.Visibility = Visibility.Collapsed;
            EquipmentPreview_Frame.Visibility = Visibility.Collapsed;
            WebView.Source = new Uri(item.Link);
            WebView.Visibility = Visibility.Visible;

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", selectedContent, 0);
        }

        private void radioButtonCal_Checked(object sender, RoutedEventArgs routedEventArgs)
        {
            RadioButton radioButton = ((RadioButton)sender);
            foreach (RadioButton rb in NavStack.Children)
            {
                if(rb.Content != radioButton.Content)
                {
                    rb.IsChecked = false;
                }
            }
            radioButton.IsChecked = true;

            WebView.Visibility = Visibility.Collapsed;
            EquipmentPreview_Frame.Visibility = Visibility.Collapsed;
            Trainer_Grid.Visibility = Visibility.Collapsed;
            ScheduleGrid.Visibility = Visibility.Visible;

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", "Schedule", 0);
        }

        private void radioButtonEquip_Checked(object sender, RoutedEventArgs routedEventArgs)
        {
            RadioButton radioButton = ((RadioButton)sender);
            foreach (RadioButton rb in NavStack.Children)
            {
                if (rb.Content != radioButton.Content)
                {
                    rb.IsChecked = false;
                }
            }
            radioButton.IsChecked = true;

            WebView.Visibility = Visibility.Collapsed;
            ScheduleGrid.Visibility = Visibility.Collapsed;
            Trainer_Grid.Visibility = Visibility.Collapsed;
            EquipmentPreview_Frame.Visibility = Visibility.Visible;

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", "Equipment", 0);
        }

        private void radioButtonTrainer_Checked(object sender, RoutedEventArgs routedEventArgs)
        {
            RadioButton radioButton = ((RadioButton)sender);
            foreach (RadioButton rb in NavStack.Children)
            {
                if (rb.Content != radioButton.Content)
                {
                    rb.IsChecked = false;
                }
            }
            radioButton.IsChecked = true;

            WebView.Visibility = Visibility.Collapsed;
            ScheduleGrid.Visibility = Visibility.Collapsed;
            EquipmentPreview_Frame.Visibility = Visibility.Collapsed;
            Trainer_Grid.Visibility = Visibility.Visible;

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", "Equipment", 0);
        }

        private void refreshPageButton(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "options_click", "Options", 0);

            Options_Frame.Navigate(typeof(Lock));
        }

        public void refreshPage()
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "refresh_click", "Refresh", 0);

            this.NavStack.Children.Clear();
            SfCalendarView.Appointments.Clear();
            EquipmentPreview_Frame.Visibility = Visibility.Collapsed;
            EquipmentPreview_Frame.Navigate(typeof(EquipmentPreview));
            ScheduleGrid.Visibility = Visibility.Collapsed;
            Trainer_Grid.Visibility = Visibility.Collapsed;
            WebView.Visibility = Visibility.Visible;
            LoadSettings();
            LoadBackgroundImage();
            LoadMainMenu();
            LoadCalendarEvents();
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

        private void SfCalendarView_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.Cancel = true;
            if (e.Appointment != null)
            {
                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "appointment_click", "Appointment Open", 0);

                Appointment App = (Appointment)e.Appointment;
            
                AppointmentPreview_Frame.Navigate(typeof(AppointmentPreview), App);
            }
            else
            {
                AppointmentPreview_Frame.Navigate(typeof(Page));
            }
        }

        public void Close_Btn_Tapped(object sender, RoutedEventArgs e)
        {
            AppointmentPreview_Frame.Navigate(typeof(Page));
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

    #region Class for Deserializing Appointments
    public class Event
    {
        public string Subject { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Instructor { get; set; }

        public string Department { get; set; }

        public string Price { get; set; }

        public string FlierJPG { get; set; }

        public bool AllDay { get; set; }
    }
    #endregion

    #region Class for Deserializing Main JSON
    public class Sched
    {
        public Event[] main { get; set; }
    }
    #endregion
}
