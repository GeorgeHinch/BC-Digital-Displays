using BC_Digital_Displays.Classes;
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
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

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
                        tbState.TextTrimming = TextTrimming.CharacterEllipsis;
                        tbState.FontSize = 28;
                        adminRegionGrid.Children.Add(tbState);

                        if (rc.sortCountry == country && rc.sortState != state)
                        {
                            Line divLine = new Line();
                            divLine.X2 = 1610;
                            divLine.Margin = new Thickness(-275, 25, 50, 50);
                            divLine.Stroke = Application.Current.Resources["UI_Return"] as SolidColorBrush;

                            adminClubsSP.Children.Add(divLine);
                        }

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

                    // current city is the different from last loops AND the current state is the same from last loops

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
                    clubName.Tapped += moreInfo_Tapped;

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
                    
                    TextBlock moreInfo = new TextBlock();
                    moreInfo.Text = "";
                    moreInfo.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    moreInfo.FontSize = 36;
                    moreInfo.HorizontalAlignment = HorizontalAlignment.Center;
                    moreInfo.VerticalAlignment = VerticalAlignment.Center;
                    moreInfo.Tag = rc;
                    moreInfo.Foreground = Application.Current.Resources["UI_Return"] as SolidColorBrush;
                    moreInfo.Tapped += moreInfo_Tapped;

                    clubGrid.Children.Add(clubSP);
                    clubGrid.Children.Add(moreInfo);
                    Grid.SetColumn(moreInfo, 1);

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

        private void moreInfo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock s = sender as TextBlock;
            bcReciprocalClubs club = s.Tag as bcReciprocalClubs;

            moreInfoGrid.Visibility = Visibility.Visible;

            // Create map geoposition from lat and long
            BasicGeoposition clubPosition = new BasicGeoposition() { Latitude = Convert.ToDouble(club.addressLat), Longitude = Convert.ToDouble(club.addressLong) };
            Geopoint clubPoint = new Geopoint(clubPosition);

            // Create a MapIcon.
            MapIcon mapIcon = new MapIcon();
            mapIcon.Location = clubPoint;
            mapIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon.Title = club.clubName;
            mapIcon.ZIndex = 0;

            // Add the MapIcon to the map.
            clubMap.MapElements.Add(mapIcon);

            // Center and zoom map
            clubMap.Center = clubPoint;
            clubMap.ZoomLevel = 16;

            clubName.Text = club.clubName;
            clubAddress.Text = club.address;
            if (club.fax == "")
            {
                clubPhoneFax.Text = club.phone;
            } else { clubPhoneFax.Text = club.phone + ", Fax: " + club.fax; }
            if (club.specialRequest == "" || club.specialRequest == null)
            {
                clubSpecialRequests.Visibility = Visibility.Collapsed;
            } else { clubSpecialRequests.Text = club.specialRequest; }
            clubDescription.Text = club.clubInfo;
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

        private void return_Tapped(object sender, TappedRoutedEventArgs e)
        {
            moreInfoGrid.Visibility = Visibility.Collapsed;
        }
    }
}
