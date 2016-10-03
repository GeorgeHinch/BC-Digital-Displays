using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AppointmentPreview : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Appointment App = (Appointment)e.Parameter;
            loadContent(App);
        }

        public AppointmentPreview()
        {
            this.InitializeComponent();
        }

        public void loadContent(Appointment e)
        {
            Flier.Source = e.Flier;
            Title.Text = e.Subject;
            Date.Text = e.DaySpan;
            Hours.Text = e.TimeStart + "-" + e.TimeEnd;
            Location.Text = e.Location;
            Notes.Text = e.Notes;
            Info.Text = e.Info;
        }

        private void Close_Btn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CalendarPreview.calendarPreview.AppointmentPreview_Frame.Navigate(typeof(Page));
        }
    }
}
