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

        //Inicializa la página y ejecuta la recuperación de las paradas 
        public ClosestStation()
        {
            InitializeComponent();

           

            this.Appearing += async (object sender, EventArgs e) =>
            {
               ObservableCollection<Station> _stations = await StationViewModel.RecoverData(StationViewModel.url);
               await orderCollectionAsync(_stations);
            };

            
        }

        /// <summary>
        /// Calcula la distancia a las estaciones y las ordena por proximidad
        /// </summary>
        /// <param name="stations"> Colección de estaciones</param>
        /// <returns></returns>
        private async Task orderCollectionAsync(ObservableCollection<Station> stations)
        {

            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 20;

            var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
            Position position = new Position(location.Latitude, location.Longitude);

            var UTM_GPS = StationViewModel.uTMConverter.convertLatLngToUtm(location.Latitude, location.Longitude);
           
            for (int i = 0; i < stations.Count; i++)
            {
                var UTM_Station = StationViewModel.uTMConverter.convertLatLngToUtm(stations[i].Lat, stations[i].Lon);

                stations[i].CurrentDist = Math.Sqrt(Math.Pow((UTM_Station.Easting - UTM_GPS.Easting), 2) + Math.Pow((UTM_Station.Northing - UTM_GPS.Northing), 2));
            }
            
            MyListView.ItemsSource = stations.OrderBy(x => x.CurrentDist);
        }


        /// <summary>
        /// Se ejecuta al seleccionar un elemento de la lista. Lanza la página siguiente en la navegación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Station s = (Station)e.Item;
            await nextPageAsync(s);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        /// <summary>
        /// Método asincrono para el cambio de página. Añade la estación al BindingContext
        /// </summary>
        /// <param name="s"> Estación a añadir al BindingContext </param>
        /// <returns></returns>
        private async Task nextPageAsync(Station s)
        {
            var detailsPage = new StationDetailedView();
            detailsPage.BindingContext = s;
            await Navigation.PushModalAsync(detailsPage);
        }



        /// <summary>
        /// Funcion que se ejecuta al actualizar la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async Task MyListView_RefreshingAsync(object sender, EventArgs e)
        {
            await orderCollectionAsync(await StationViewModel.RecoverData(StationViewModel.url));

            MyListView.EndRefresh();
        }
    }
}
