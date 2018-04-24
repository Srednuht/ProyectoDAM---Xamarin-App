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
            if (NameField.Text.Length > 0)
                OnLoadPageAsync();
            else
                ErrorText.Opacity = 1;
        }

        private async void OnLoadPageAsync()
        {
            await Navigation.PushModalAsync(new MenuPage());
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
           ErrorText.Opacity = (e.NewTextValue.Length > 0) ? 0 : 1;                
        }
    }
}