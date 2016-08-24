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
using Microsoft.Toolkit.Uwp.UI.Animations;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BC_Digital_Displays
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Class_Preview : Page
    {
        public static Class_Preview classPreview;
        public Class_Preview()
        {
            this.InitializeComponent();
            
            Gradient_Background.Blur(duration: 10, delay: 0, value: 10).Start();
        }

        private void CloseEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            YouthBrochure.youthBrochure.classCard_Frame.Visibility = Visibility.Collapsed;
        }
    }
}
