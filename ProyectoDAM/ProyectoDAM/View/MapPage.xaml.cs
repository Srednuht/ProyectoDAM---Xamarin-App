using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;

namespace ProyectoDAM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            OpenMapAtPositionAsync();





        }


        private async Task OpenMapAtPositionAsync()
        {
            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 50;


            var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
            Position position = new Position(location.Latitude, location.Longitude);

            System.Diagnostics.Debug.WriteLine(position.Latitude + "," + position.Longitude);

            Map MyMap = new Map(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
           

            
        }
    }
}