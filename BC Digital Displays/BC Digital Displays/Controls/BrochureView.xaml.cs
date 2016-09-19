using BC_Digital_Displays.Classes;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BC_Digital_Displays.Controls
{
    public sealed partial class BrochureView : UserControl
    {
        public BrochureView()
        {
            this.InitializeComponent();

            //buildSessions();
            trialRun();

        }

        #region Dependency Properties
        public static readonly DependencyProperty BrochureIDProperty = DependencyProperty.Register(
            "Brochure ID",                   // The name of the DependencyProperty
            typeof(Guid),               // The type of the DependencyProperty
            typeof(BrochureView),     // The type of the owner of the DependencyProperty
            null
        );

        public Guid BrochureID
        {
            get { return (Guid)GetValue(BrochureIDProperty); }
            set { SetValue(BrochureIDProperty, value); }
        }
        #endregion

        #region Load Brochure
        public void LoadBorchure()
        {
            /*ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://www.bellevueclub.com/digital-signage/BC-Trainers.txt");
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Trainers));
                        Trainers status = (Trainers)serializer.ReadObject(stream);

                        double trainerGrid = (status.main.Length / 3);
                        int numOfGrid = (int)Math.Ceiling(trainerGrid);

                        List<Trainer_Card> cardList = new List<Trainer_Card>();

                        StackPanel indexSP = new StackPanel();
                        int indexVal = 1;
                        IList<StackPanel> spList = new List<StackPanel>(numOfGrid);

                        int tbVal = 0;

                        #region Creates trainer cards
                        foreach (Trainer t in status.main)
                        {
                            
                        }
                        #endregion
                    }
                }
            }/**/




            /*string connString = ConfigurationManager.ConnectionStrings["BC_DisplaysConnectionString"].ConnectionString;
            SqlConnection conn = null;

            try
            {
                List<Events> data = new List<Events>();
                conn = new SqlConnection(connString);
                SqlCommand command = new SqlCommand("SELECT * FROM [rec-brochure] WHERE [isActive]='1'", conn);
                conn.Open();
                SqlDataReader sdr = command.ExecuteReader();

                while (sdr.Read())
                {
                    Events obj = new Events(
                        (bool)sdr["isActive"],
                        (Guid)sdr["guid"],
                        (DateTime)sdr["created"],
                        (string)sdr["name"],
                        (string)sdr["location"],
                        (string)sdr["description"],
                        (DateTime)sdr["orderTime"],
                        (string)sdr["startTime"],
                        (string)sdr["endTime"],
                        (string)sdr["instructor"],
                        (string)sdr["department"],
                        (string)sdr["price"],
                        (string)sdr["flier"],
                        (bool)sdr["allDay"]);
                    data.Add(obj);
                }

                conn.Close();
                return data;
            }
            catch (Exception ex)
            {
                //log error 
                //display friendly error to user
                Debug.WriteLine("----");
                Debug.WriteLine("Source: " + ex.Source + " |");
                Debug.WriteLine("Message: " + ex.Message + " |");
                Debug.WriteLine("Stacktrace: " + ex.StackTrace + " |");
                Debug.WriteLine("Inner Exception: " + ex.InnerException + " |");
                Debug.WriteLine("----");
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    //cleanup connection i.e close 
                    conn.Close();
                }
            }/**/
        }
        #endregion

        #region Build session string for left column
        public async void buildSessions()
        {
            sampleMe samMe = new sampleMe
            {
                Text = "here store this",
                isActive = true
            };

            string output = JsonConvert.SerializeObject(samMe);

            //var json = new JavaScriptSerializer().Serialize(samMe);

            TodoItem item = new TodoItem
            {
                Text = output
            };

            Debug.WriteLine("Serial: " + output + " | ");
            await App.MobileService.GetTable<TodoItem>().InsertAsync(item);
        }
        #endregion

        private MobileServiceCollection<TodoItem, TodoItem> items;
        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();
        public async Task trialRun()
        {
            items = await todoTable
                    .ToCollectionAsync();

            IEnumerable<TodoItem> itemsControl = items;

            foreach (TodoItem i in itemsControl)
            {
                TodoItem tmp = JsonConvert.DeserializeObject<TodoItem>(i.Text);
                Debug.WriteLine("This: " + tmp.Text + " | ");
            }
            Debug.WriteLine("Degree: " + items.Count + " | ");
            
            
            /*
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(aTrainers => aTrainers.isActive == true)
                    .ToCollectionAsync();
                Debug.WriteLine("Degree: " + items[0].Text + " | ");
                //Debug.WriteLine("Degree: " + aTrainers + " | ");
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
                //ListItems.ItemsSource = items;
                //Debug.WriteLine("Item 1: " + items[0].name + " | ");
            }*/
        }
    }
}
