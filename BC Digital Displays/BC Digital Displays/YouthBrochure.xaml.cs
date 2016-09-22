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
    public sealed partial class YouthBrochure : Page
    {
        public static YouthBrochure youthBrochure;
        public YouthBrochure()
        {
            this.InitializeComponent();
            youthBrochure = this;

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("youth_brochure");

            var task = Task.Run(async () => { await findBrochureID(); });
            task.Wait();

            BrochureRec.BrochureID = bID;
            BrochureTennis.BrochureID = bID;
            BrochureSwim.BrochureID = bID;
            BrochureBasketball.BrochureID = bID;
        }

        private MobileServiceCollection<bcRecBrochure, bcRecBrochure> items;
        private IMobileServiceTable<bcRecBrochure> bcRecBrochureTable = App.MobileService.GetTable<bcRecBrochure>();

        string bID;
        public async Task findBrochureID()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                items = await bcRecBrochureTable
                    .Where(aBrochure => aBrochure.isActive == true)
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
                IEnumerable<bcRecBrochure> itemsControl = items;

                bID = items[0].id;
            }
        }
    }
}
