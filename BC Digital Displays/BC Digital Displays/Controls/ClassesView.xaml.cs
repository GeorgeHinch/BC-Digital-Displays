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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BC_Digital_Displays.Controls
{
    public sealed partial class ClassesView : UserControl
    {
        public static ClassesView classView;
        public ClassesView()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(ClassesView);
            this.DataContext = this;
            this.Loaded += UserControl_Loaded;
        }

        public string bID;
        public double bCAT;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bID = this.BrochureID;
            bCAT = this.BrochureSection;

            var task = Task.Run(async () => { await loadClassCards(); });
            task.Wait();

            IEnumerable<bcRecClasses> itemsControl = items;
            foreach (bcRecClasses i in items)
            {
                ClassView classView = new ClassView();
                classView.ClassName = i.name;
                classView.ClassDay = DataBuilder.dayBuilder(i.days) + ",";
                classView.ClassTime = DataBuilder.timeBuilder(i.time);
                classView.DepartmentType = i.category;
                classView.Tag = i;

                #region Sort class cards into groups
                if (i.ageMin == 0 && i.ageMax == 0)
                {
                    Group_1.Children.Add(classView);
                }

                if (0 < i.ageMin && i.ageMin < 3)
                {
                    Group_2.Children.Add(classView);

                    if (3 <= i.ageMax)
                    {
                        Group_3.Children.Add(classView.CreateCopy());
                    }

                    if (7 <= i.ageMax)
                    {
                        Group_4.Children.Add(classView.CreateCopy());
                    }

                    if (12 <= i.ageMax)
                    {
                        Group_5.Children.Add(classView.CreateCopy());
                    }

                    if (15 <= i.ageMax)
                    {
                        Group_6.Children.Add(classView.CreateCopy());
                    }
                }

                if (3 <= i.ageMin && i.ageMin < 7)
                {
                    Group_3.Children.Add(classView);

                    if (7 <= i.ageMax)
                    {
                        Group_4.Children.Add(classView.CreateCopy());
                    }

                    if (12 <= i.ageMax)
                    {
                        Group_5.Children.Add(classView.CreateCopy());
                    }

                    if (15 <= i.ageMax)
                    {
                        Group_6.Children.Add(classView.CreateCopy());
                    }
                }

                if (7 <= i.ageMin && i.ageMin < 12)
                {
                    Group_4.Children.Add(classView);

                    if (12 <= i.ageMax)
                    {
                        Group_5.Children.Add(classView.CreateCopy());
                    }

                    if (15 <= i.ageMax)
                    {
                        Group_6.Children.Add(classView.CreateCopy());
                    }
                }

                if (12 <= i.ageMin && i.ageMin < 15)
                {
                    Group_5.Children.Add(classView);

                    if (15 <= i.ageMax)
                    {
                        Group_6.Children.Add(classView.CreateCopy());
                    }
                }

                if (15 <= i.ageMin)
                {
                    Group_6.Children.Add(classView);
                }
                #endregion
            }

            hideSections();
            
            disableRB();
        }

        #region Update UI line size based
        private void Group_1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Group1_Line.Y2 = Group_1.ActualHeight;
        }

        private void Group_2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Group2_Line.Y2 = Group_2.ActualHeight;
        }

        private void Group_3_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Group3_Line.Y2 = Group_3.ActualHeight;
        }

        private void Group_4_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Group4_Line.Y2 = Group_4.ActualHeight;
        }

        private void Group_5_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Group5_Line.Y2 = Group_5.ActualHeight;
        }

        private void Group_6_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Group6_Line.Y2 = Group_6.ActualHeight;
        }
        #endregion

        #region Load class cards
        private MobileServiceCollection<bcRecClasses, bcRecClasses> items;
        private IMobileServiceTable<bcRecClasses> bcRecClassesTable = App.MobileService.GetTable<bcRecClasses>();
        public async Task loadClassCards()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await bcRecClassesTable
                    .Where(aBrochure => aBrochure.brochureID == bID)
                    .Where(aBrochure => aBrochure.category == bCAT)
                    .Where(aBrochure => aBrochure.deleted == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
                Debug.WriteLine("Exception: " + exception.Message + " | ");
            }

            if (exception != null)
            {
                Debug.WriteLine("Exception: " + exception.Message + " | ");
            }
            else
            {
                Debug.WriteLine("Made it here;");
            }
        }
        #endregion

        #region Hide sections that have no children
        public void hideSections()
        {
            if (Group_1.Children.Count < 2)
            {
                Grid_1.Visibility = Visibility.Collapsed;
            }

            if (Group_2.Children.Count < 2)
            {
                Grid_2.Visibility = Visibility.Collapsed;
            }

            if (Group_3.Children.Count < 2)
            {
                Grid_3.Visibility = Visibility.Collapsed;
            }

            if (Group_4.Children.Count < 2)
            {
                Grid_4.Visibility = Visibility.Collapsed;
            }

            if (Group_5.Children.Count < 2)
            {
                Grid_5.Visibility = Visibility.Collapsed;
            }

            if (Group_6.Children.Count < 2)
            {
                Grid_6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Controller for filters
        public void disableRB()
        {
            if (Group_1.Children.Count < 2)
            {
                Rb_1.IsEnabled = false;
            }

            if (Group_2.Children.Count < 2)
            {
                Rb_2.IsEnabled = false;
            }

            if (Group_3.Children.Count < 2)
            {
                Rb_3.IsEnabled = false;
            }

            if (Group_4.Children.Count < 2)
            {
                Rb_4.IsEnabled = false;
            }

            if (Group_5.Children.Count < 2)
            {
                Rb_5.IsEnabled = false;
            }

            if (Group_6.Children.Count < 2)
            {
                Rb_6.IsEnabled = false;
            }
        }

        private void yearRB_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Tag.ToString() == "1")
            {
                Grid_1.Visibility = Visibility.Visible;
                Grid_2.Visibility = Visibility.Collapsed;
                Grid_3.Visibility = Visibility.Collapsed;
                Grid_4.Visibility = Visibility.Collapsed;
                Grid_5.Visibility = Visibility.Collapsed;
                Grid_6.Visibility = Visibility.Collapsed;
            }
            else if (rb.Tag.ToString() == "2")
            {
                Grid_1.Visibility = Visibility.Collapsed;
                Grid_2.Visibility = Visibility.Visible;
                Grid_3.Visibility = Visibility.Collapsed;
                Grid_4.Visibility = Visibility.Collapsed;
                Grid_5.Visibility = Visibility.Collapsed;
                Grid_6.Visibility = Visibility.Collapsed;
            }
            else if (rb.Tag.ToString() == "3")
            {
                Grid_1.Visibility = Visibility.Collapsed;
                Grid_2.Visibility = Visibility.Collapsed;
                Grid_3.Visibility = Visibility.Visible;
                Grid_4.Visibility = Visibility.Collapsed;
                Grid_5.Visibility = Visibility.Collapsed;
                Grid_6.Visibility = Visibility.Collapsed;
            }
            else if (rb.Tag.ToString() == "4")
            {
                Grid_1.Visibility = Visibility.Collapsed;
                Grid_2.Visibility = Visibility.Collapsed;
                Grid_3.Visibility = Visibility.Collapsed;
                Grid_4.Visibility = Visibility.Visible;
                Grid_5.Visibility = Visibility.Collapsed;
                Grid_6.Visibility = Visibility.Collapsed;
            }
            else if (rb.Tag.ToString() == "5")
            {
                Grid_1.Visibility = Visibility.Collapsed;
                Grid_2.Visibility = Visibility.Collapsed;
                Grid_3.Visibility = Visibility.Collapsed;
                Grid_4.Visibility = Visibility.Collapsed;
                Grid_5.Visibility = Visibility.Visible;
                Grid_6.Visibility = Visibility.Collapsed;
            }
            else if (rb.Tag.ToString() == "6")
            {
                Grid_1.Visibility = Visibility.Collapsed;
                Grid_2.Visibility = Visibility.Collapsed;
                Grid_3.Visibility = Visibility.Collapsed;
                Grid_4.Visibility = Visibility.Collapsed;
                Grid_5.Visibility = Visibility.Collapsed;
                Grid_6.Visibility = Visibility.Visible;
            }
        }

        private void resetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Grid_1.Visibility = Visibility.Visible;
            Grid_2.Visibility = Visibility.Visible;
            Grid_3.Visibility = Visibility.Visible;
            Grid_4.Visibility = Visibility.Visible;
            Grid_5.Visibility = Visibility.Visible;
            Grid_6.Visibility = Visibility.Visible;

            foreach (RadioButton rb in Rb_StackPanel.Children)
            {
                rb.IsChecked = false;
            }

            hideSections();
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty BrochureIDProperty = DependencyProperty.Register(
            "Brochure ID",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(ClassesView),     // The type of the owner of the DependencyProperty
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
            typeof(ClassesView),     // The type of the owner of the DependencyProperty
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
