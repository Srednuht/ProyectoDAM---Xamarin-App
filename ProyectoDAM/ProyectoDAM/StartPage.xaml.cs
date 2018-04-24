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
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{ 
			InitializeComponent ();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            OnLoadPageAsync();
        }

        private async void OnLoadPageAsync()
        {
            await Navigation.PushModalAsync(new MenuPage());
        }
    }
}