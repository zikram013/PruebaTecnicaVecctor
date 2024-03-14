using Newtonsoft.Json.Linq;
using PruebaTecnicaVecctor.DTOs;
using System.Net;
using System.Net.Http;


namespace PruebaTecnicaVecctor.Servicio.Interfaz
{
    public class NasaApiService : INasaApiService
    {
        private readonly HttpClient _httpClient;

        public NasaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<DatoApiNasaDTO> ObtenerDatos(string ahora, string hasta)
        {
            string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={ahora}&end_date={hasta}&api_key=BgkFvbvedy9zfdeApHGbbFOMr2cmhOhWMunVhnX2";

            HttpResponseMessage response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (errorMessage.Contains("The Feed date limit is only 7 Days"))
                {
                    throw new Exception("Date Format Exception - La fecha limite para buscar son 7 dias");
                }
                else
                {
                    throw new Exception(HttpStatusCode.BadRequest.ToString());
                }
            }

            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var nearEarthObjects = ParseNearEarthObjects(jsonResponse);

            var dto = nearEarthObjects.Select(x => new DatoApiNasaDTO
            {
                near_earth_objects = x
            }).ToList();

            return dto;
        }


        private List<NearEarthObjects> ParseNearEarthObjects(string jsonResponse)
        {
            JObject parent = JObject.Parse(jsonResponse);

            var nearEarthObjectsLista = parent["near_earth_objects"]
                .SelectMany(item => item)
                .Select(asteroidData => asteroidData.ToObject<NearEarthObjectData>())
                .Where(data => data.IsPotentiallyHazardous)
                .Select(data => new NearEarthObjects
                {
                    name = data.Name,
                    estimated_diameter = data.EstimatedDiameter,
                    close_approach_data = data.CloseApproachData.FirstOrDefault(),
                    mediaDiametro = (data.EstimatedDiameter.kilometers.estimated_diameter_max + data.EstimatedDiameter.kilometers.estimated_diameter_min) / 2
                })
                .ToList();

            return nearEarthObjectsLista;
        }
    }

}
