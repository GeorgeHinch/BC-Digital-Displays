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
                classView.ClassDay = DayBuilder.dayBuilder(i.days) + ",";
                classView.ClassTime = TimeBuilder.timeBuilder(i.time);
                classView.DepartmentType = i.category;
                classView.Tag = i;

                #region Sort class cards into groups
                if (0 < i.ageMin && i.ageMin < 6)
                {
                    // Group_1.Children.Add(classView);

                    if (i.ageMax > 6)
                    {
                        // Group_2.Children.Add(classView);
                    }
                }
                #endregion

                Group_1.Children.Add(classView);
            }
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
