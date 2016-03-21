using BC_Digital_Displays.Classes;
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
    public sealed partial class TrainerPreview : Page
    {
        #region Load Trainer Preview from Card.Tag on Page Load
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Trainer trainer_info = (Trainer)e.Parameter;

            int years;
            int yearsBC;
            string years_String;
            string yearsBC_String;
            if (trainer_info.years != null && trainer_info.years_bc != null)
            {
                try
                {
                    years = (int)DateTime.Now.Year - Int32.Parse(trainer_info.years);
                    years_String = years.ToString();
                }
                catch
                {
                    years_String = "NaN";
                }

                try
                {
                    yearsBC = (int)DateTime.Now.Year - Int32.Parse(trainer_info.years_bc);
                    yearsBC_String = yearsBC.ToString();
                }
                catch
                {
                    yearsBC_String = "NaN";
                }
            }
            else { years_String = "0"; yearsBC_String = "0"; }

            Trainer_Img.UriSource = new Uri(trainer_info.photo, UriKind.Absolute);
            Trainer_Name.Text = trainer_info.name;
            Trainer_Degree.Text = trainer_info.degree;
            Trainer_Years.Text = years_String;
            Trainer_YearsBC.Text = yearsBC_String;
            Trainer_Session.Text = trainer_info.session;
            Trainer_Reward.Text = trainer_info.reward;
            Trainer_Expertise.Text = trainer_info.expertise;
            Trainer_Accomplishment.Text = trainer_info.accomplishment;
        }
        #endregion

        public TrainerPreview()
        {
            this.InitializeComponent();
        }

        #region Back Button Event
        public void previewReturn_Clicked (object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.TrainerCard_Frame.Visibility = Visibility.Visible;
            MainPage.mainPage.FlipviewIndicator_Stackpanel.Visibility = Visibility.Visible;
            MainPage.mainPage.IndTrainerInfo_Frame.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
