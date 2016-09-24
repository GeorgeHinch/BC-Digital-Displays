using BC_Digital_Displays.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// TODO: Pass values through GA strings

namespace BC_Digital_Displays.Cards
{
    public sealed partial class Class_Card : UserControl
    {
        public Class_Card()
        {
            this.InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void SendEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "email_click", "(" + this.Name + ") Email: " + this.Name, 0);

            this.Visibility = Visibility.Collapsed;
            Debug.WriteLine("Parent: " + this.Parent.ToString() +" |");
            
            Grid grCase = (Grid)this.Parent;
            Grid grCaseCase = (Grid)grCase.Parent;
            Grid neededGrid = (Grid)grCaseCase.FindName("Email_Template");
            neededGrid.Visibility = Visibility.Visible;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Class_Name.Text = this.ClassName;
            Class_AgeDayTime.Text = this.ClassAgeDayTime;
            Class_Sessions.Text = this.ClassSession;
            Class_Description.Text = this.ClassDescription;
        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "close_click", "(" + this.Name + ") Email: " + this.Name, 0);

            YouthBrochure.youthBrochure.classCard_Frame.Navigate(typeof(Page));
        }

        #region Dependency Properties
        public static readonly DependencyProperty ClassNameProperty = DependencyProperty.Register(
            "ClassName",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Class_Preview),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassName
        {
            get { return (string)GetValue(ClassNameProperty); }
            set { SetValue(ClassNameProperty, value); }
        }

        public static readonly DependencyProperty ClassAgeDayTimeProperty = DependencyProperty.Register(
            "ClassAgeDayTime",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Class_Preview),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassAgeDayTime
        {
            get { return (string)GetValue(ClassAgeDayTimeProperty); }
            set { SetValue(ClassAgeDayTimeProperty, value); }
        }

        public static readonly DependencyProperty ClassSessionProperty = DependencyProperty.Register(
            "ClassSession",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Class_Preview),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassSession
        {
            get { return (string)GetValue(ClassSessionProperty); }
            set { SetValue(ClassSessionProperty, value); }
        }

        public static readonly DependencyProperty ClassDescriptionProperty = DependencyProperty.Register(
            "ClassDescritpion",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(Class_Preview),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassDescription
        {
            get { return (string)GetValue(ClassDescriptionProperty); }
            set { SetValue(ClassDescriptionProperty, value); }
        }
        #endregion
    }
}
