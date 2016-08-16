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
    public partial class ClassView : UserControl
    {
        public ClassView()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(ClassView);
            this.DataContext = this;
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Department Number: " + (string)DepartmentType + " |");
            Debug.WriteLine("Class Name: " + (string)ClassName + " |");

            if ((string)this.DepartmentType == "1")
            {
                Class_Icon.Foreground = (SolidColorBrush)Application.Current.Resources["Dept_Aquatics"];
            }
        }

        #region Dependency Properties
        public static readonly DependencyProperty ClassTypeProperty = DependencyProperty.Register(
            "ClassType",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassView),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassType
        {
            get { return (string)GetValue(ClassTypeProperty); }
            set { SetValue(ClassTypeProperty, value); }
        }

        public static readonly DependencyProperty ClassNameProperty = DependencyProperty.Register(
            "ClassName",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassView),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassName
        {
            get { return (string)GetValue(ClassNameProperty); }
            set { SetValue(ClassNameProperty, value); }
        }

        public static readonly DependencyProperty ClassDayProperty = DependencyProperty.Register(
            "ClassDay",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassView),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassDay
        {
            get { return (string)GetValue(ClassDayProperty); }
            set { SetValue(ClassDayProperty, value); }
        }

        public static readonly DependencyProperty ClassTimeProperty = DependencyProperty.Register(
            "ClassTime",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassView),     // The type of the owner of the DependencyProperty
            null
        );

        public string ClassTime
        {
            get { return (string)GetValue(ClassTimeProperty); }
            set { SetValue(ClassTimeProperty, value); }
        }

        public static readonly DependencyProperty DepartmentTypeProperty = DependencyProperty.Register(
            "DepartmentType",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassView),     // The type of the owner of the DependencyProperty
            null
        );

        public string DepartmentType
        {
            get { return (string)GetValue(DepartmentTypeProperty); }
            set { SetValue(DepartmentTypeProperty, value); }
        }
        #endregion

        private void MoreInfo_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
