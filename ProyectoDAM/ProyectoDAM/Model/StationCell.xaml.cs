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
    public partial class StationCell : ViewCell
    {

        //The ID of the bike station
        public static readonly BindableProperty IDProperty = 
        BindableProperty.Create("ID", typeof(string), typeof(StationCell), "");

        public string ID
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        
        //The name of the station
        public static readonly BindableProperty NameProperty =
        BindableProperty.Create("Name", typeof(string), typeof(StationCell), "");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }


        //The address of the station
        public static readonly BindableProperty AddressProperty =
        BindableProperty.Create("Address", typeof(string), typeof(StationCell), "");

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        //The current available bikes at the station
        public static readonly BindableProperty AvailableProperty =
        BindableProperty.Create("Available", typeof(string), typeof(StationCell), "");

        public string Available
        {
            get { return (string)GetValue(AvailableProperty); }
            set { SetValue(AvailableProperty, value); }
        }


        //The amount of bikes the station has
        public static readonly BindableProperty TotalProperty =
        BindableProperty.Create("Total", typeof(string), typeof(StationCell), "");

        public string Total
        {
            get { return (string)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }


        //The arrow image of the list item
        public static readonly BindableProperty ArrowImageListProperty =
        BindableProperty.Create("ArrowImage", typeof(string), typeof(StationCell), "");  

        public string ArrowImage
        {
            get { return (string)GetValue(ArrowImageListProperty); }
            set { SetValue(ArrowImageListProperty, value); }
        }

        public StationCell(string id, string name, string addr, string ava, string tot)
        {
            //InitializeComponent();

            this.ID = id;
            this.Name = name;
            this.Address = addr;
            this.Available = ava;
            this.Total = tot;
        }


        public StationCell()
        {
            InitializeComponent();
            this.View.BackgroundColor = Color.Red;
        }


    }
}