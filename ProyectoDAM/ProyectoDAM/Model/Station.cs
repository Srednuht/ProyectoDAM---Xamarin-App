using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoDAM.Model
{
    //Modelo de datos para almacenar la información de una estación
    public class Station : INotifyPropertyChanged
    {
       
        private string name;         //Nombre de la estación
        private string address;      //Dirección de la estación

        private int number;          //Número idenficador de la estación
        private int available;       //Número de bicicletas disponiblesen para recoger
        private int free;            //Número de espacios libres para dejar la bicicleta

        private float lat;           //Latitud de la estación (coords)
        private float lon;           //Longitud de la estación (coords)



        //Constructor por defecto
        public Station()
        {
            Name = "";
            Number = 0;
            Address = "";
            Available = 0;
            Free = 0;
            Lat = 0f;
            Lon = 0f;

        }

        public Station(string name, string address, int number, int available, int free, float lat, float lon)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Number = number;
            Available = available;
            Free = free;
            Lat = lat;
            Lon = lon;
        }



        //Implementación de la interfaz que notifica los cambios
        public event PropertyChangedEventHandler PropertyChanged;

        //Getters && Setters
        public string Name { get => name; set { UpdateName(value); } }
        public string Address { get => address; set { UpdateAddress(value); } }
        public int Number { get => number; set { UpdateNumber(value); } }
        public int Available { get => available; set { UpdateAvailable(value); } }
        public int Free { get => free; set { UpdateFree(value); } }
        public float Lat { get => lat; set { UpdateLat(value); }}
        public float Lon { get => lon; set { UpdateLon(value); }}

        public string Availability
        {
            get
            {
                return string.Format("{0} / " + (available + free).ToString(), available);
            }
        }


        private void UpdateName(string value)
        {
            if(value != Name)
            {
                name = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("name"));
            }
                
        }


        private void UpdateAddress(string value)
        {
            if (value != Address)
            {
                address = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("address"));
            }
               
        }

        private void UpdateNumber(int value)
        {
            if (value != Number)
            {
                number = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("number"));
            }
        }

        private void UpdateAvailable(int value)
        {
            if (value != Available)
            {
                available = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("available"));
            }
        }

        private void UpdateFree(int value)
        {
            if (value != Free)
            {
                free = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("free"));
            }
        }

        private void UpdateLat(float value)
        {
            if (value != Lat)
            {
                lat = value;
               // PropertyChanged(this, new PropertyChangedEventArgs("lat"));
            }
        }

        private void UpdateLon(float value)
        {
            if (value != Lon)
            {
                lon = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("lon"));
            }
        }

    }
}
