using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoDAM
{
    /// <summary>
    /// Esta clase se utiliza únicamente a modo de "Splash Screen"
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            OnLoadPageAsync();         
        }

        /// <summary>
        /// Carga de la página de inicio
        /// </summary>
        private async void OnLoadPageAsync()
        {
            await Task.Delay(3000);
            await Navigation.PushModalAsync(new StartPage());
        }
    }
    
}
