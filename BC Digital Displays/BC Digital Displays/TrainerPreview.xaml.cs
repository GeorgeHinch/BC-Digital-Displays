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
            bcTrainers trainer_info = (bcTrainers)e.Parameter;

            int years;
            int yearsBC;
            string years_String;
            string yearsBC_String;
            if (trainer_info.years != null && trainer_info.yearsBC != null)
            {
                try
                {
                    years = (int)DateTime.Now.Year - (int)trainer_info.years;
                    years_String = years.ToString();
                }
                catch
                {
                    years_String = "NaN";
                }

                try
                {
                    yearsBC = (int)DateTime.Now.Year - (int)trainer_info.yearsBC;
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
            Trainer_Session.Text = trainer_info.expectation;
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
            TrainerFlipview.trainerFlipView.Trainer_Flipview.Visibility = Visibility.Visible;
            TrainerFlipview.trainerFlipView.FlipviewIndicator_Stackpanel.Visibility = Visibility.Visible;
            TrainerFlipview.trainerFlipView.IndTrainerInfo_Frame.Navigate(typeof(Page));
            TrainerFlipview.trainerFlipView.IndTrainerInfo_Frame.Visibility = Visibility.Collapsed;
            TrainerFlipview.trainerFlipView.GoBack_Button.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
