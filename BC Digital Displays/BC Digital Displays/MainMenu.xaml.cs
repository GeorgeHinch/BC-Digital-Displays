﻿using System;
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
        }

        private void Menu_ClubNews_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(NotificationPreview), null);
        }

        private void Menu_RecBrochure_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(YouthBrochure), null);
        }

        private void Menu_Trainers_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(TrainerFlipview), null);
        }

        private void Menu_Equipment_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(EquipmentPreview), null);
        }

        private void Menu_Calendar_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            menuFlipView.Visibility = Visibility.Collapsed;
            mainFrame.Navigate(typeof(CalendarPreview), null);
        }
    }
}