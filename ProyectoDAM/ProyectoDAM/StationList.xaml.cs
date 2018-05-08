using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoDAM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StationList : ContentPage
    {
        public ObservableCollection<StationCell> Items { get; set; }

        public StationList()
        {
            InitializeComponent();

            Items = new ObservableCollection<StationCell>
            {

                 new StationCell("1", "hola", "asasa", "12", "15"),
                 new StationCell("2", "hola2", "asasa", "12", "15"),
                 new StationCell("3", "hola3", "asasa", "12", "15")
            };
            
            StationListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
