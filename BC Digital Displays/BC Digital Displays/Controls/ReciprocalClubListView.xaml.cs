﻿using BC_Digital_Displays.Classes;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
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
    public sealed partial class ReciprocalClubListView : UserControl
    {
        public static ReciprocalClubListView reciprocalClubListView;

        public ReciprocalClubListView()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(ReciprocalClubListView);
            this.DataContext = this;
        }

        public void loadContent()
        {
            MobileServiceCollection<bcReciprocalClubs, bcReciprocalClubs> items = this.Tag as MobileServiceCollection<bcReciprocalClubs, bcReciprocalClubs>;

            string country = "";
            string state = "";

            #region Creates grids and stackpanels for sorting
            Grid adminRegionGrid = new Grid();
            Grid clubGrid = new Grid();
            StackPanel adminClubsSP = new StackPanel();
            #endregion

            try
            {
                foreach (bcReciprocalClubs rc in items)
                {
                    
                    #region Adds textblock to adminRegion level grid when new state
                    if (rc.sortState != state)
                    {
                        adminRegionGrid.Children.Add(adminClubsSP);
                        Grid.SetColumn(adminClubsSP, 1);

                        clubsStackPanel.Children.Add(adminRegionGrid);

                        adminRegionGrid = new Grid();
                        adminRegionGrid.Name = "AdminRegion_" + rc.sortState;
                        adminRegionGrid.Margin = new Thickness(50, 0, 0, 0);
                        ColumnDefinition c0 = new ColumnDefinition();
                        c0.Width = new GridLength(1, GridUnitType.Star);
                        adminRegionGrid.ColumnDefinitions.Add(c0);
                        ColumnDefinition c1 = new ColumnDefinition();
                        c1.Width = new GridLength(5, GridUnitType.Star);
                        adminRegionGrid.ColumnDefinitions.Add(c1);

                        TextBlock tbState = new TextBlock();

                        tbState.Text = rc.sortState;
                        tbState.FontSize = 28;
                        adminRegionGrid.Children.Add(tbState);

                        adminClubsSP = new StackPanel();
                    }
                    #endregion

                    #region Adds country textblock for every new country
                    if (rc.sortCountry != country)
                    {
                        TextBlock tbCountry = new TextBlock();

                        tbCountry.Text = rc.sortCountry;
                        tbCountry.FontSize = 28;
                        tbCountry.FontWeight = FontWeights.SemiBold;
                        tbCountry.Margin = new Thickness(0, 50, 0, 15);

                        clubsStackPanel.Children.Add(tbCountry);
                    }
                    #endregion

                    clubGrid = new Grid();
                    clubGrid.Name = "ClubGrid_" + rc.clubName;
                    ColumnDefinition c2 = new ColumnDefinition();
                    c2.Width = new GridLength(1, GridUnitType.Star);
                    clubGrid.ColumnDefinitions.Add(c2);
                    ColumnDefinition c3 = new ColumnDefinition();
                    c3.Width = new GridLength(.1, GridUnitType.Star);
                    clubGrid.ColumnDefinitions.Add(c3);

                    StackPanel clubSP = new StackPanel();
                    clubSP.Name = "ClubSP_" + rc.clubName;
                    clubSP.Orientation = Orientation.Vertical;
                    clubSP.Margin = new Thickness(0, 0, 0, 20);

                    TextBlock clubName = new TextBlock();
                    clubName.Text = rc.clubName;
                    clubName.FontSize = 28;
                    clubName.FontWeight = FontWeights.Thin;

                    TextBlock clubAddress = new TextBlock();
                    clubAddress.Text = rc.address;
                    clubAddress.FontSize = 24;
                    clubAddress.FontWeight = FontWeights.Thin;

                    TextBlock clubPhoneFax = new TextBlock();
                    clubPhoneFax.Text = rc.phone + ", Fax: " + rc.fax;
                    clubPhoneFax.FontSize = 24;
                    clubPhoneFax.FontWeight = FontWeights.Thin;

                    clubSP.Children.Add(clubName);
                    clubSP.Children.Add(clubAddress);
                    clubSP.Children.Add(clubPhoneFax);

                    clubGrid.Children.Add(clubSP);

                    adminClubsSP.Children.Add(clubGrid);

                    

                    country = rc.sortCountry;
                    state = rc.sortState;
                }

                adminRegionGrid.Children.Add(adminClubsSP);
                Grid.SetColumn(adminClubsSP, 1);

                clubsStackPanel.Children.Add(adminRegionGrid);
            }

            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine(ex.Data);
                Debug.WriteLine(ex.InnerException);
            }
        }

        #region Dependency Properties
        public static readonly DependencyProperty ClubListProperty = DependencyProperty.Register(
            "ClubList",                   // The name of the DependencyProperty
            typeof(MobileServiceCollection<bcReciprocalClubs, bcReciprocalClubs>),               // The type of the DependencyProperty
            typeof(ReciprocalClubListView),     // The type of the owner of the DependencyProperty
            null
        );

        public MobileServiceCollection<bcReciprocalClubs, bcReciprocalClubs> ClubList
        {
            get { return (MobileServiceCollection<bcReciprocalClubs, bcReciprocalClubs>)GetValue(ClubListProperty); }
            set { SetValue(ClubListProperty, value); }
        }
        #endregion
    }
}