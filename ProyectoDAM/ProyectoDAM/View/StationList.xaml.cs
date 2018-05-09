
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProyectoDAM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;



namespace ProyectoDAM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StationList : ContentPage
    {
        public ObservableCollection<Station> Stations { get; set; }



        public StationList()
        {
            InitializeComponent();
            Stations = new ObservableCollection<Station>();
            this.Appearing += async (object sender, EventArgs e) =>
           {
               Task<int> _stationTask =  RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

           };

          



            // RefreshCommand();
            //Task<int> _stationTask;



            /*{
                new Station("MEDITERRANEO", "Mediterráneo - Plaza Cruz de Cañamelar", 161, 12, 2,39,0.4f),
                new Station("PZA. ARMADA ESPAÑOLA", "Armada Española - Mariano Cuber", 162, 14, 8, 39,0.4f),
                new Station("PASEO NEPTUNO", "Paseo Neptuno 32-34 ", 162, 3, 15, 39,0.4f),
                new Station("PAVIA 2", "Pavía - Espadán", 162, 10, 8, 39,0.4f),
                new Station("AVDA. PERIS Y VALERO", "Peris y Valero - Cuba", 162, 5, 19, 39,0.4f),
                new Station("CALLE_BARCAS_FRENTE _TEATRO_PRINCIPAL", "Barcas, 11", 162, 13, 7, 39,0.4f),
                new Station("AVDA. GRAL. URRUTIA", "General Urrutia - Granada", 162, 6, 8, 39,0.4f),
                new Station("PROGRESO", "Don Vicente Guillot - Progreso", 162, 12, 3, 39,0.4f),
                new Station("CALLE CAMPOS CRESPO ", "Campos Crespo - Juan de Garay", 162, 4, 12, 39,0.4f)



            };
            */

            
            StationListView.ItemsSource = Stations;
        }

        private async Task<int> RecoverData(string v)
        {
            

            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(v);
            //string json = res.RequestMessage.ToString();

            JObject json = JObject.Parse(res);


            JEnumerable<JToken> results = json["features"].Children();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            foreach(JToken token in results)
            {
                string station = token["properties"].ToString();

                Station stationObj = JsonConvert.DeserializeObject<Station>(station);

                Stations.Add(stationObj);
            }
            StationListView.ItemsSource = Stations;
            return 1;
         }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void StationListView_Refreshing(object sender, EventArgs e)
        {
            Task<int> _stationTask = RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

            _stationTask.Wait(3000);
            StationListView.EndRefresh();
        }
    }
}
