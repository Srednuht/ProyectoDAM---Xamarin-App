using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Collections.ObjectModel;
using ProyectoDAM.Model;
using ProyectoDAM.Utils;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace ProyectoDAM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        public List<Pin> pins;

        public LatLngUTMConverter uTMConverter = new LatLngUTMConverter("WGS 84");



        public MapPage()
        {
            InitializeComponent();

            pins = new List<Pin>();

            this.Appearing += async (object sender, EventArgs e) =>
            {
                Task<int> _stationTask =  RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

            };

            //OpenMapAtPositionAsync();

        }

        public MapPage(float lat ,float lon)
        {
            InitializeComponent();

            pins = new List<Pin>();

            this.Appearing += async (object sender, EventArgs e) =>
            {
                Task<int> _stationTask = RecoverData("http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON");

            };

            
            
        }

        private async Task<int> RecoverData(string v)
        {

            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(v);

            JObject json = JObject.Parse(res);


            JEnumerable<JToken> results = json["features"].Children();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            foreach (JToken token in results)
            {
                string station = token["properties"].ToString();
                string coordinates = token["geometry"]["coordinates"].ToString();

                List<float> coordList = JsonConvert.DeserializeObject<List<float>>(coordinates);
                Station stationObj = JsonConvert.DeserializeObject<Station>(station);

                var latlng = uTMConverter.convertUtmToLatLng(coordList[0], coordList[1], 30, "N");
                stationObj.Lon = (float)latlng.Lng;
                stationObj.Lat = (float)latlng.Lat;

                Pin pin = new Pin()
                {
                    Label = stationObj.Address + "\n Bicis: " + stationObj.Available + "\n Huecos: " + stationObj.Free,
                    Position = new Position(latlng.Lat, latlng.Lng)
                };

                 pins.Add(pin);

            }

            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 20;


            var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
            Position position = new Position(location.Latitude, location.Longitude);


            for (int i = 0; i < pins.Count; i++)
                mapa.Pins.Add(pins[i]);

            mapa.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
           

            return 1;
        }
    }
}