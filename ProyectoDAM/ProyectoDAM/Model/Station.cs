using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoDAM.Model
{
    //Modelo de datos para almacenar la información de una estación
    public class Station : INotifyPropertyChanged
    {
       
        private string name;                //Nombre de la estación
        private string address;             //Dirección de la estación

        private int number;                 //Número idenficador de la estación
        private int available;              //Número de bicicletas disponiblesen para recoger
        private int free;                   //Número de espacios libres para dejar la bicicleta


        private Coordinates coordinates;    //Coordenadas;

        private float lat;                  //Latitud de la estación (coords)
        private float lon;                  //Longitud de la estación (coords)

        private double currentDist;          //Variable que almacena la distancia puntual al usuario

        //Constructor por defecto
        public Station()
        {
            Name = "";
            Number = 0;
            Address = "";
            Available = 0;
            Free = 0;
            Coordinates = null;
            CurrentDist = 100000;
        }

        public Station(string name, string address, int number, int available, int free,Coordinates coords)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Number = number;
            Available = available;
            Free = free;
            Coordinates = coords;
        }



        //Implementación de la interfaz que notifica los cambios

        public event PropertyChangedEventHandler PropertyChanged;

        //Getters && Setters
        public string Name { get => name; set => UpdateName(value);  }
        public string Address { get => address; set => UpdateAddress(value);  }
        public int Number { get => number; set => UpdateNumber(value); } 
        public int Available { get => available; set => UpdateAvailable(value); }
        public int Free { get => free; set => UpdateFree(value); }
        public Coordinates Coordinates { get => coordinates; set => UpdateCoordinates(value); }
        public float Lat { get => lat; set => UpdateLat(value); }
        public float Lon { get => lon; set => UpdateLon(value); }
        public double CurrentDist { get => currentDist; set => currentDist = value; }

        public string Availability
        {
            get
            {
                return string.Format("{0} / " + (available + free).ToString(), available);
            }
        }



        public void UpdateCoordinates(Coordinates value)
        {
             if(value != Coordinates)
            {
                coordinates = value;
                UpdateLat(value.lat);
                UpdateLon(value.lon);
                //PropertyChanged(this, new PropertyChangedEventArgs("coordinates"));
            }

        }

        private void UpdateName(string value)
        {
            if(value != Name)
            {
                string[] a = value.Split('_');
                
                for(int i = 1; i < a.Length; i++)
                {
                    name += a[i] + " ";
                }
                
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
                //PropertyChanged(this, new PropertyChangedEventArgs("lat"));
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

    public class Coordinates
    {
        public float lat;
        public float lon;
    }
}
