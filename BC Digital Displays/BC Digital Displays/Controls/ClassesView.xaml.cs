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

namespace BC_Digital_Displays.Controls
{
    public sealed partial class ClassesView : UserControl
    {
        public ClassesView()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(ClassesView);
            this.DataContext = this;
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Checked: " + this.FilterChecked + " |");
            if (this.FilterChecked == "Monday")
            {
                Debug.WriteLine("YAYYYYYYYY!!!");
            }
        }

        #region Dependency Properties
        public static readonly DependencyProperty IssueProperty = DependencyProperty.Register(
            "Issue",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassesView),     // The type of the owner of the DependencyProperty
            null
        );

        public string Issue
        {
            get { return (string)GetValue(IssueProperty); }
            set { SetValue(IssueProperty, value); }
        }

        public static readonly DependencyProperty DeptTabProperty = DependencyProperty.Register(
            "DeptTab",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassesView),     // The type of the owner of the DependencyProperty
            null
        );

        public string DeptTab
        {
            get { return (string)GetValue(DeptTabProperty); }
            set { SetValue(DeptTabProperty, value); }
        }

        public static readonly DependencyProperty FilterCheckedProperty = DependencyProperty.Register(
            "FilterChecked",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassesView),     // The type of the owner of the DependencyProperty
            null
        );

        public string FilterChecked
        {
            get { return (string)GetValue(FilterCheckedProperty); }
            set { SetValue(FilterCheckedProperty, value); }
        }
        #endregion
    }
}
