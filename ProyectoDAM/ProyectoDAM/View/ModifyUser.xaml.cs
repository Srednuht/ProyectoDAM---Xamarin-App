using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoDAM.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifyUser : ContentPage
	{
		public ModifyUser ()
		{
			InitializeComponent ();
		}

        private void UserNameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Store.Opacity = (e.NewTextValue.Length > 3) ? 1 : 0;

            UserNameField.Text = (e.NewTextValue.Length > 10) ? e.OldTextValue : e.NewTextValue;
        }

        private void Store_Clicked(object sender, EventArgs e)
        {

            App.Current.Properties.Remove("nombre");
            App.Current.Properties.Add("nombre", UserNameField.Text);
            App.Current.SavePropertiesAsync();
            goBackAsync();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            goBackAsync();
        }

        private async Task goBackAsync()
        {
            await Navigation.PopModalAsync();
        }

       
    }
}