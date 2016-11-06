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

            var task = Task.Run(async () => { await loadMenu(); });
            task.Wait();
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
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                Debug.WriteLine("Exception: " + exception.Message + " | ");
                GoogleAnalytics.EasyTracker.GetTracker().SendException(e.Message, false);
            }
            finally
            {
                List<Button> buttonList = new List<Button>();

                foreach (bcMenu menu in items)
                {
                    Debug.WriteLine("Menu Item: " + menu.menuItem + ", " + menu.orderVal + " |");
                    Button returnButton = DataBuilder.buttonBuilder(menu.menuItem);
                    buttonList.Add(returnButton);
                }
            }
        }
        #endregion

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
