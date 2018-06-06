
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProyectoDAM.Model;
using ProyectoDAM.Utils;
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

        public LatLngUTMConverter uTMConverter = new LatLngUTMConverter("WGS 84");


        public StationList()
        {
            InitializeComponent();
            Stations = new ObservableCollection<Station>();

            this.Appearing += async (object sender, EventArgs e) =>
           {
               Task<int> _stationTask =  RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

           };

                     
            StationListView.ItemsSource = Stations;
        }

        private async Task<int> RecoverData(string v)
        {

            Stations = new ObservableCollection<Station>();
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
                string coordinates = token["geometry"]["coordinates"].ToString();

                List<float> coordList = JsonConvert.DeserializeObject<List<float>>(coordinates);
                Station stationObj = JsonConvert.DeserializeObject<Station>(station);

                var latlng = uTMConverter.convertUtmToLatLng(coordList[0], coordList[1],30,"N");
                stationObj.Lon = (float)latlng.Lng;
                stationObj.Lat = (float)latlng.Lat;
                
                Stations.Add(stationObj);
            }

            StationListView.ItemsSource = Stations;
            return 1;
         }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Station s = (Station)e.Item;
            //await DisplayAlert("Item Tapped", "Esta parada está en la posición: " + s.Lat + "," + s.Lon  , "OK");
           
            await nextPageAsync(s);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async Task nextPageAsync(Station s)
        {
            var detailsPage = new StationDetailedView();
            detailsPage.BindingContext = s;
            await Navigation.PushModalAsync(detailsPage);
        }

        private void StationListView_Refreshing(object sender, EventArgs e)
        {
            Task<int> _stationTask = RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

            _stationTask.Wait(3000);
            StationListView.EndRefresh();
        }
    }
}
