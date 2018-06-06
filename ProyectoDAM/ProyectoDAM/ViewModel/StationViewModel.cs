using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using ProyectoDAM.Model;
using ProyectoDAM.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDAM.View
{

    public class StationViewModel
    {

        public StationViewModel() { }

        //Url para recuperar la lista de paradas y su información
        public static string url = "http://mapas.valencia.es/lanzadera/opendata/Valenbisi/JSON";
        //Colección que almacena las estaciones de bicis
        public static ObservableCollection<Station> Stations { get; private set; }
        //Instancia del conversor de coordenadas
        public static LatLngUTMConverter uTMConverter = new LatLngUTMConverter("WGS 84");


        /// <summary>
        /// Lanza la petición HTTP para las paradas. Se parsea el JSON recibido y se deserializa
        /// dicho JSON en las clases definidas en el modelo ( Station y Coordinates ). Se añade la lista
        /// a la variable asociada al ListView para que esta pueda mostrar los datos.
        /// </summary>
        /// <param name="url">Dirección para recuperar el JSON</param>
        /// <returns> Lista de estaciones </returns>
        public static async Task<ObservableCollection<Station>> RecoverData(string url)
        {

            Stations = new ObservableCollection<Station>();
            HttpClient client = new HttpClient();
            string res = await client.GetStringAsync(url);

            JObject json = JObject.Parse(res);

            JEnumerable<JToken> results = json["features"].Children();

            foreach (JToken token in results)
            {
                string station = token["properties"].ToString();
                string coordinates = token["geometry"]["coordinates"].ToString();

                List<float> coordList = JsonConvert.DeserializeObject<List<float>>(coordinates);
                Station stationObj = JsonConvert.DeserializeObject<Station>(station);

                var latlng = uTMConverter.convertUtmToLatLng(coordList[0], coordList[1], 30, "N");
                stationObj.Lon = (float)latlng.Lng;
                stationObj.Lat = (float)latlng.Lat;

                Stations.Add(stationObj);

            }

            return Stations;
        }


    }


}
