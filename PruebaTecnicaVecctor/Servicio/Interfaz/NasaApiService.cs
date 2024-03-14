using Azure;
using Newtonsoft.Json.Linq;
using PruebaTecnicaVecctor.DTOs;
using System.Net;
using System.Net.Http;


namespace PruebaTecnicaVecctor.Servicio.Interfaz
{
    public class NasaApiService : INasaApiService
    {
       
        public List<DatoApiNasaDTO> ObtenerDatos(string ahora, string hasta)
        {

            using (var httpCliente = new HttpClient()) 
            {
                string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={ahora}&end_date={hasta}&api_key=BgkFvbvedy9zfdeApHGbbFOMr2cmhOhWMunVhnX2";

                var dto = new List<DatoApiNasaDTO>();

                HttpResponseMessage response = httpCliente.GetAsync(url).GetAwaiter().GetResult();
                try 
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        if (errorMessage.Contains("The Feed date limit is only 7 Days"))
                        {
                            throw new Exception("La fecha limite para buscar son 7 dias");
                        }
                        else
                        {
                            throw new Exception(HttpStatusCode.BadRequest.ToString());
                        }
                    }
                }
                catch (Exception ex) 
                {
                    dto.Add(new DatoApiNasaDTO
                    {
                        mensaje = ex.Message
                    });
                }
               

                string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var nearEarthObjects = ParseNearEarthObjects(jsonResponse);

                dto = nearEarthObjects.Select(x => new DatoApiNasaDTO
                {
                    near_earth_objects = x
                }).ToList();

                return dto;
            }
                
        }


        private List<NearEarthObjects> ParseNearEarthObjects(string jsonResponse)
        {
            List<NearEarthObjects> nearEarthObjectsLista = new List<NearEarthObjects>();
            var resJSON = jsonResponse;

            JObject parent = JObject.Parse(resJSON);
            var dias = parent.Value<JObject>("near_earth_objects").Properties().ToList();

            foreach (var item in dias)
            {
                foreach (var item2 in item.Value)
                {
                    var obj = item2.ToList();
                    var is_potentially_hazardous_asteroid = bool.Parse(obj[7].First.ToString());

                    if (is_potentially_hazardous_asteroid)
                    {
                        NearEarthObjects nearEarthObjects = new NearEarthObjects();
                        nearEarthObjects.name = obj[3].First.ToString();
                        nearEarthObjects.estimated_diameter = obj[6].First.ToObject<EstimatedDiameter>();
                        nearEarthObjects.mediaDiametro = CalculaMedia(nearEarthObjects.estimated_diameter.kilometers.estimated_diameter_max, nearEarthObjects.estimated_diameter.kilometers.estimated_diameter_min);
                        nearEarthObjects.close_approach_data = obj[8].First.ToObject<List<CloseApproachDatum>>().FirstOrDefault();

                        nearEarthObjectsLista.Add(nearEarthObjects);
                    }

                }
            }

            return nearEarthObjectsLista;
        }




        private double CalculaMedia(double a, double b)
        {
            return ((a + b) / 2);
        }
    }

}
