using BC_Digital_Displays.Classes;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BC_Digital_Displays.Controls
{
    public sealed partial class BrochureView : UserControl
    {
        public BrochureView()
        {
            this.InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClassesGroup.BrochureID = this.BrochureID;
            ClassesGroup.BrochureSection = this.BrochureSection;
            Debug.WriteLine("(2) ID from BrochureView.XAML+=loaded: " + this.BrochureID + " | ");
        }

        #region Build session string for left column
        public void buildSessions()
        {
            //string output = JsonConvert.SerializeObject(samMe);
            //TodoItem tmp = JsonConvert.DeserializeObject<TodoItem>(i.Text);
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty BrochureIDProperty = DependencyProperty.Register(
            "Brochure ID",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(BrochureView),     // The type of the owner of the DependencyProperty
            null
        );

        public string BrochureID
        {
            get { return (string)GetValue(BrochureIDProperty); }
            set { SetValue(BrochureIDProperty, value); }
        }

        public static readonly DependencyProperty BrochureSectionProperty = DependencyProperty.Register(
            "Brochure Section",                   // The name of the DependencyProperty
            typeof(double),               // The type of the DependencyProperty
            typeof(BrochureView),     // The type of the owner of the DependencyProperty
            null
        );
        
        public double BrochureSection
        {
            get { return (double)GetValue(BrochureSectionProperty); }
            set { SetValue(BrochureSectionProperty, value); }
        }
        #endregion
    }
}
