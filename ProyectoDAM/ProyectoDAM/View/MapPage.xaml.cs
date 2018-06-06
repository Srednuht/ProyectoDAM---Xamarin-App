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
using ProyectoDAM.View;

namespace ProyectoDAM
{   
    /// <summary>
    /// Clase que renderiza el mapa con todas las estaciones
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        
        //Lista de Pins para representar la posición de las estaciones
        public List<Pin> pins;

        //Lista de Estaciones
        public ObservableCollection<Station> stations;

        /// <summary>
        /// Inicializa la lista de pins y recupera la información de las paradas
        /// </summary>
        public MapPage()
        {
            InitializeComponent();

            pins = new List<Pin>();


            this.Appearing += async (object sender, EventArgs e) =>
            {
                    stations = await StationViewModel.RecoverData(StationViewModel.url);
                    await mapSetupAsync(stations);
            };

           

        }

        private async Task mapSetupAsync(ObservableCollection<Station> stations)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                Pin pin = new Pin()
                {
                    Label = stations[i].Address + "\n Bicis: " + stations[i].Available + "\n Huecos: " + stations[i].Free,
                    Position = new Position(stations[i].Lat, stations[i].Lon)
                };

                pins.Add(pin);
            }

        var locator = CrossGeolocator.Current;

        locator.DesiredAccuracy = 20;

        var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
        Position position = new Position(location.Latitude, location.Longitude);

        for (int j = 0; j<pins.Count; j++)
                mapa.Pins.Add(pins[j]);

            mapa.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
           
        }

    
    }
}