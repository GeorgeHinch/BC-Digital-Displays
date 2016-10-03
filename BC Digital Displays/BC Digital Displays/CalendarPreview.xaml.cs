using BC_Digital_Displays.Classes;
using BC_Digital_Displays.Controls;
using Microsoft.WindowsAzure.MobileServices;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BC_Digital_Displays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPreview : Page
    {
        public static CalendarPreview calendarPreview;
        public CalendarPreview()
        {
            this.InitializeComponent();
            calendarPreview = this;
            this.Loaded += UserControl_Loaded;
        }

        #region Creates events on calendar from loaded SQL data
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var task = Task.Run(async () => { await loadEvents(); });
                task.Wait();

                IEnumerable<bcEvents> itemsControl = items;
                foreach (bcEvents eV in items)
                {
                    Appointment newappointment = new Appointment();
                    DateTime start = DateTime.ParseExact(eV.startTime, "yyyy,  M,  d,  H,  m,  s", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime end = DateTime.ParseExact(eV.endTime, "yyyy,  M,  d,  H,  m,  s", System.Globalization.CultureInfo.CurrentCulture);
                    newappointment.StartTime = start;
                    newappointment.EndTime = end;
                    newappointment.Subject = eV.name;
                    newappointment.Location = eV.location;
                    newappointment.Notes = eV.description;
                    newappointment.TimeStart = String.Format("{0:t}", newappointment.StartTime);
                    newappointment.TimeEnd = String.Format("{0:t}", newappointment.EndTime);
                    newappointment.DayStart = String.Format("{0:m}", newappointment.StartTime);
                    newappointment.DayEnd = String.Format("{0:m}", newappointment.EndTime);
                    newappointment.DaySpan = newappointment.DayStart;
                    if (newappointment.DayStart != newappointment.DayEnd)
                    {
                        newappointment.DaySpan = newappointment.DayStart + "-" + newappointment.EndTime.Day.ToString();
                    }
                    newappointment.Instructor = eV.instructor;
                    if (eV.department == "Aquatics")
                    {
                        newappointment.AppointmentBackground = (SolidColorBrush)Application.Current.Resources["Dept_Aquatics"];
                    }
                    else if (eV.department == "Fitness")
                    {
                        newappointment.AppointmentBackground = (SolidColorBrush)Application.Current.Resources["Dept_Fitness"];
                    }
                    else if (eV.department == "Food & Beverage")
                    {
                        newappointment.AppointmentBackground = (SolidColorBrush)Application.Current.Resources["Dept_FoodAndBeverage"];
                    }
                    else if (eV.department == "Member Events")
                    {
                        newappointment.AppointmentBackground = (SolidColorBrush)Application.Current.Resources["Dept_MemberEvents"];
                    }
                    else if (eV.department == "Recreation")
                    {
                        newappointment.AppointmentBackground = (SolidColorBrush)Application.Current.Resources["Dept_Recreation"];
                    }
                    else if (eV.department == "Tennis")
                    {
                        newappointment.AppointmentBackground = (SolidColorBrush)Application.Current.Resources["Dept_Tennis"];
                    }
                    newappointment.Department = eV.department;
                    newappointment.Price = eV.price;
                    newappointment.FlierJPG = eV.flier;
                    if (eV.flier == null || eV.flier == "")
                    {
                        newappointment.Flier = new BitmapImage();
                    }
                    else
                    {
                        newappointment.Flier = new BitmapImage(new Uri(eV.flier, UriKind.Absolute));
                    }
                    newappointment.AllDay = eV.allDay;
                    if (eV.instructor == null)
                    {
                        newappointment.Info = eV.department + " | " + eV.price;
                    }
                    else
                    {
                        newappointment.Info = eV.department + " | " + eV.instructor + " | " + eV.price;
                    }
                    SfCalendarView.Appointments.Add(newappointment);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message + " | ");
                throw;
            }
        }
        #endregion

        #region Load events from SQL
        private MobileServiceCollection<bcEvents, bcEvents> items;
        private IMobileServiceTable<bcEvents> bcEventsTable = App.MobileService.GetTable<bcEvents>();
        public async Task loadEvents()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await bcEventsTable
                    .Where(aEvent => aEvent.deleted == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                Debug.WriteLine("Exception: " + exception.Message + " | ");
                GoogleAnalytics.EasyTracker.GetTracker().SendException(e.Message, false);
            }
        }
        #endregion

        #region Button actions for calendar
        public void Close_Btn_Tapped(object sender, RoutedEventArgs e)
        {
            AppointmentPreview_Frame.Navigate(typeof(Page));
        }

        private void GoBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainMenu.mainMenu.menuFlipView.Visibility = Visibility.Visible;
            MainMenu.mainMenu.mainFrame.Navigate(typeof(Page));
        }

        private void SfCalendarView_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.Cancel = true;
            if (e.Appointment != null)
            {
                Appointment App = (Appointment)e.Appointment;

                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "appointment_click", "Appointment Open: " + App.Subject, 0);

                AppointmentPreview_Frame.Navigate(typeof(AppointmentPreview), App);
            }
            else
            {
                AppointmentPreview_Frame.Navigate(typeof(Page));
            }
        }
        #endregion
    }

    #region Appointment Class Extension
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
}
