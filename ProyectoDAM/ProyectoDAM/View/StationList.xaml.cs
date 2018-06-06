
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
    /// <summary>
    /// Renderiza la lista de las estaciones
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StationList : ContentPage
    {


        //Inicializa la página y ejecuta la recuperación de las paradas 
        public StationList()
        {
            InitializeComponent();

            this.Appearing += async (object sender, EventArgs e) =>
           {
                 StationListView.ItemsSource = await StationViewModel.RecoverData(StationViewModel.url);
           };

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
        private async Task StationListView_RefreshingAsync(object sender, EventArgs e)
        {
            StationListView.ItemsSource = await StationViewModel.RecoverData(StationViewModel.url);

            StationListView.EndRefresh();
        }
    }
}
