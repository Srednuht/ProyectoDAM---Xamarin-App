using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoDAM.View
{
    /// <summary>
    /// Pantalla para la modificación del nombre de usuario
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifyUser : ContentPage
	{
		public ModifyUser ()
		{
			InitializeComponent ();
            Store.IsEnabled = false;
		}


        //Callback que se ejecuta cuando se está escribiendo en el TextField
        private void UserNameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Store.IsEnabled = (e.NewTextValue.Length > 0) ? true : false;
            UserNameField.Text = (e.NewTextValue.Length > 10) ? e.OldTextValue : e.NewTextValue;
        }

        //Callback que se ejecuta al pulsar el botón de Guardar. Almacena el nuevo nombre de usuario
        private void Store_Clicked(object sender, EventArgs e)
        {

            App.Current.Properties.Remove("nombre");
            App.Current.Properties.Add("nombre", UserNameField.Text);
            App.Current.SavePropertiesAsync();
            goBackAsync();
        }


        //CallBack que se ejecuta al pulsar el botón de cancelar. Regresa al menu.
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