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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BC_Digital_Displays.Cards
{
    public sealed partial class Trainer_Card : UserControl
    {
        public Trainer_Card()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(Trainer_Card);
            this.DataContext = this;
        }

        public static readonly DependencyProperty TrainerNameProperty = DependencyProperty.Register(
            "TrainerName",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Trainer_Card),     // The type of the owner of the DependencyProperty
            null
        );

        public string TrainerName
        {
            get { return (string)GetValue(TrainerNameProperty); }
            set { SetValue(TrainerNameProperty, value); }
        }

        public static readonly DependencyProperty YearsExpProperty = DependencyProperty.Register(
            "YearsExp",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Trainer_Card),     // The type of the owner of the DependencyProperty
            null
        );

        public string YearsExp
        {
            get { return (string)GetValue(YearsExpProperty); }
            set { SetValue(YearsExpProperty, value); }
        }

        public static readonly DependencyProperty YearsBCProperty = DependencyProperty.Register(
            "YearsBC",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Trainer_Card),     // The type of the owner of the DependencyProperty
            null
        );

        public string YearsBC
        {
            get { return (string)GetValue(YearsBCProperty); }
            set { SetValue(YearsBCProperty, value); }
        }

        public static readonly DependencyProperty ExpProperty = DependencyProperty.Register(
            "Exp",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Trainer_Card),     // The type of the owner of the DependencyProperty
            null
        );

        public string Exp
        {
            get { return (string)GetValue(ExpProperty); }
            set { SetValue(ExpProperty, value); }
        }

        public static readonly DependencyProperty TrainerPhotoURLProperty = DependencyProperty.Register(
            "TrainerPhotoURL",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Trainer_Card),     // The type of the owner of the DependencyProperty
            null
        );

        public string TrainerPhotoURL
        {
            get { return (string)GetValue(TrainerPhotoURLProperty); }
            set { SetValue(TrainerPhotoURLProperty, value); }
        }

        private void LearnMore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            //GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "open_click", "(" + ID + ") Open: from recipe ID #" + ID, 0);

            //SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage), ID);
        }
    }
}
