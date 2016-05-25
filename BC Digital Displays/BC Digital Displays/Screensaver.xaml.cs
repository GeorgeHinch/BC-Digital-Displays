using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class Screensaver : Page
    {
        public Screensaver()
        {
            this.InitializeComponent();


            // When loading the images, use this code to create an indicator icon

            /*              TextBlock tb = new TextBlock();
                            tb.Name = tbVal.ToString();
                            tb.Text = WebUtility.HtmlDecode("&#xEA3A;");
                            tb.FontFamily = new FontFamily("Segoe MDL2 Assets");
                            tb.TextAlignment = TextAlignment.Center;
                            tb.Margin = new Thickness(10, 0, 10, 0);
                            tb.Foreground = new SolidColorBrush(Color.FromArgb(127,255,255,255));
                            tb.Tapped += new TappedEventHandler(indicator_Clicked);
                            Screensaver_FlipviewIndicator.Children.Add(tb); /**/

            #region Timer changes screensaver image every N seconds
            int change = 1;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(7);
            timer.Tick += (o, a) =>
            {
                // Once screensaver reaches its bounds, it reverses direction
                int newIndex = Screensaver_Flipview.SelectedIndex + change;
                if (newIndex >= Screensaver_Flipview.Items.Count || newIndex < 0)
                {
                    change *= -1;
                }

                Screensaver_Flipview.SelectedIndex += change;
            };

            timer.Start();
            #endregion
        }

        #region Loads images and adds them to the flipview
        public void loadImages()
        {
            List<string> imgLinks = null;

            foreach (string s in imgLinks)
            {

            }
        }
        #endregion

        #region Creating Click Event for Flipview Indicators
        public void indicator_Clicked(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int tbValOut = Int32.Parse(tb.Name);

            Screensaver_Flipview.SelectedIndex = tbValOut;
        }
        #endregion

        #region Flipview Indicator on Flipview Index Change
        private void Screensaver_Flipview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int siVal = Screensaver_Flipview.SelectedIndex;

            foreach (TextBlock t in Screensaver_FlipviewIndicator.Children)
            {
                t.Text = WebUtility.HtmlDecode("&#xEA3A;");
                t.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            }

            TextBlock tb = (TextBlock)Screensaver_FlipviewIndicator.Children.ElementAt(siVal);
            tb.Text = WebUtility.HtmlDecode("&#xEA3B;");
            tb.Foreground = new SolidColorBrush(Color.FromArgb(191, 255, 255, 255));
        }
        #endregion
    }
}
