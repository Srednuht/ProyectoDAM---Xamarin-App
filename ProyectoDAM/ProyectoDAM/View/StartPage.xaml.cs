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

        public StartPage()
        {
            InitializeComponent();

   
            if (App.Current.Properties.ContainsKey("nombre"))
            {
                InitialText.FontSize = (App.Current.Properties["nombre"].ToString().Length > 6) ? 40 : 60;
                InitialText.Text = "Hola " + App.Current.Properties["nombre"].ToString();
                NameField.Opacity = 0;
                StartButton.Text = "Ir al Menú";
                StartButton.IsEnabled = true;
            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (App.Current.Properties.ContainsKey("nombre"))
            {
                OnLoadPageAsync();
               
            }
            else if (NameField.Text.Length > 0)
            {
                App.Current.Properties.Add("nombre", NameField.Text);
                App.Current.SavePropertiesAsync();
                OnLoadPageAsync();
            }
            else
            {
                ErrorText.Opacity = 1;
            }

        }

        private async void OnLoadPageAsync()
        {
            await Navigation.PushModalAsync(new MenuPage());
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorText.Opacity = (e.NewTextValue.Length > 0) ? 0 : 1;
            StartButton.IsEnabled = (e.NewTextValue.Length > 0) ? true : false;

            if (e.NewTextValue.Length > 10)
                NameField.Text = e.OldTextValue;
        }

        protected override bool OnBackButtonPressed()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            return true;
        }

    }
}