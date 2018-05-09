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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            OnLoadPageAsync();

           
        }

        private async void OnLoadPageAsync()
        {
            await Task.Delay(3000);
            await Navigation.PushModalAsync(new StartPage());
        }
    }
    
}
