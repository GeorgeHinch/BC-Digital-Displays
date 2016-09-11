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

namespace BC_Digital_Displays.Controls
{
    public sealed partial class BrochureView : UserControl
    {
        public BrochureView()
        {
            this.InitializeComponent();
        }

        #region Dependency Properties
        public static readonly DependencyProperty BrochureIDProperty = DependencyProperty.Register(
            "Brochure ID",                   // The name of the DependencyProperty
            typeof(Guid),               // The type of the DependencyProperty
            typeof(BrochureView),     // The type of the owner of the DependencyProperty
            null
        );

        public Guid BrochureID
        {
            get { return (Guid)GetValue(BrochureIDProperty); }
            set { SetValue(BrochureIDProperty, value); }
        }
        #endregion
    }
}
