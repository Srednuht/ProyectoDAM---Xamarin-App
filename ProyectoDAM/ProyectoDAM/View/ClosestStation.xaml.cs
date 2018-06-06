using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using ProyectoDAM.Model;
using ProyectoDAM.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoDAM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClosestStation : ContentPage
    {
        public ObservableCollection<Station> Stations { get; set; }

        public LatLngUTMConverter uTMConverter = new LatLngUTMConverter("WGS 84");

        public ClosestStation()
        {
            InitializeComponent();

            Stations = new ObservableCollection<Station>();

            this.Appearing += async (object sender, EventArgs e) =>
            {
                Task<int> _stationTask = RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

            };

            
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



        private async Task<int> RecoverData(string v)
        {

            Stations = new ObservableCollection<Station>();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(v);

            JObject json = JObject.Parse(res);


            JEnumerable<JToken> results = json["features"].Children();


            /* Calculamos la posición del usuario */
            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 25;
            var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
            /* */

            foreach (JToken token in results)
            {
                string station = token["properties"].ToString();
                string coordinates = token["geometry"]["coordinates"].ToString();

                List<float> coordList = JsonConvert.DeserializeObject<List<float>>(coordinates);
                Station stationObj = JsonConvert.DeserializeObject<Station>(station);

                var latlng = uTMConverter.convertUtmToLatLng(coordList[0], coordList[1], 30, "N");
                stationObj.Lon = (float)latlng.Lng;
                stationObj.Lat = (float)latlng.Lat;

                var UTM = uTMConverter.convertLatLngToUtm(location.Latitude, location.Longitude);

                stationObj.CurrentDist = Math.Sqrt(Math.Pow((coordList[0] - UTM.Easting), 2) + Math.Pow((double)(coordList[1] - UTM.Northing), 2));
                Stations.Add(stationObj);
            }


            var aux = Stations.OrderBy(x => x.CurrentDist);
            
            MyListView.ItemsSource = aux;
            return 1;
        }

        private void MyListView_Refreshing(object sender, EventArgs e)
        {
            Task<int> _stationTask = RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

            _stationTask.Wait(3000);
            MyListView.EndRefresh();
        }
    }
}
