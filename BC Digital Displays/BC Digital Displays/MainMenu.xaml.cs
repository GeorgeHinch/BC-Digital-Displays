using BC_Digital_Displays.Classes;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class MainMenu : Page
    {
        public static MainMenu mainMenu;
        public MainMenu()
        {
            this.InitializeComponent();
            mainMenu = this;

            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var task = Task.Run(async () => { await loadMenu(); });
                task.Wait();

                addMenu();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message + " | ");
            }
        }

        #region Load menu from SQL
        private MobileServiceCollection<bcMenu, bcMenu> items;
        private IMobileServiceTable<bcMenu> bcMenuTable = App.MobileService.GetTable<bcMenu>();
        public async Task loadMenu()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await bcMenuTable
                    .Where(aMenu => aMenu.deleted == false)
                    .OrderBy(aMenu => aMenu.orderVal)
                    .ToCollectionAsync();

                foreach (bcMenu menu in items)
                {
                    Debug.WriteLine("Try Menu Item: " + menu.menuItem + ", " + menu.orderVal + " |");
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                Debug.WriteLine("Exception: " + exception.Message + " | ");
                GoogleAnalytics.EasyTracker.GetTracker().SendException(e.Message, false);
            }
        }
        #endregion

        public void addMenu()
        {
            List<Button> buttonList = new List<Button>();
            List<StackPanel> deviceStackpanelList = new List<StackPanel>();
            List<StackPanel> mainStackpanelList = new List<StackPanel>();
            StackPanel indexSP = new StackPanel();
            StackPanel mainIndexSP = new StackPanel();
            int indexVal = 1;
            int mainIndexVal = 1;

            foreach (bcMenu menu in items)
            {
                Debug.WriteLine("Finally Menu Item: " + menu.menuItem + ", " + menu.orderVal + " |");
                Button returnButton = DataBuilder.buttonBuilder(menu);
                buttonList.Add(returnButton);
            }

            Debug.WriteLine("Button List: " + buttonList.Count + " |");
            #region Creates stackpanel rows of 4 buttons
            foreach (Button btn in buttonList)
            {
                indexSP.Children.Add(btn);

                if (indexVal % 4 == 0)
                {
                    indexSP.Orientation = Orientation.Horizontal;
                    indexSP.Margin = new Thickness(0, 0, 0, 75);
                    deviceStackpanelList.Add(indexSP);
                    indexSP = new StackPanel();
                }

                indexVal++;
            }

            if (indexSP.Children.Count != 0)
            {
                indexSP.Orientation = Orientation.Horizontal;
                indexSP.Margin = new Thickness(0, 0, 0, 75);
                deviceStackpanelList.Add(indexSP);
            }
            #endregion

            Debug.WriteLine("dSP: " + deviceStackpanelList.Count + " |");
            #region Creates stackpanels of two stackpanel rows
            foreach (StackPanel sp in deviceStackpanelList)
            {
                mainIndexSP.Children.Add(sp);

                if (mainIndexVal % 2 == 0)
                {
                    mainIndexSP.HorizontalAlignment = HorizontalAlignment.Center;
                    mainIndexSP.Height = 600;
                    mainIndexSP.VerticalAlignment = VerticalAlignment.Center;
                    mainIndexSP.Width = 1200;
                    mainStackpanelList.Add(mainIndexSP);
                    mainIndexSP = new StackPanel();
                }

                mainIndexVal++;
            }

            if (mainIndexSP.Children.Count != 0)
            {
                mainStackpanelList.Add(mainIndexSP);
            }
            #endregion

            Debug.WriteLine("mSP: " + mainStackpanelList.Count + " |");
            #region Adds main stackpanels to flipview
            foreach (StackPanel sp in mainStackpanelList)
            {
                Debug.WriteLine("Added to FV: "  + " |");
                menuFlipView.Items.Add(sp);
            }
            #endregion
        }

        public void Menu_ClubNews_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(NotificationPreview), null);
        }

        public void Menu_RecBrochure_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(YouthBrochure), null);
        }

        public void Menu_Trainers_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(TrainerFlipview), null);
        }

        public void Menu_Equipment_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(EquipmentPreview), null);
        }

        public void Menu_Calendar_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(CalendarPreview), null);
        }

        public void Menu_Fitness_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(FitnessPage), null);
        }

        public void Menu_ReciprocalClubs_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(ReciprocalClubPage), null);
        }
    }
}
