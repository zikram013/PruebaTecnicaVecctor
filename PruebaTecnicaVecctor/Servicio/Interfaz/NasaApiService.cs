﻿using Azure;
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
                string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={ahora}&end_date={hasta}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";

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

                    return dto;
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
            JObject parent = JObject.Parse(jsonResponse);
            var nearEarthObjects = parent["near_earth_objects"];

            List<NearEarthObjects> nearEarthObjectsLista = nearEarthObjects
                .SelectMany(day => day.Values<JArray>())
                .SelectMany(asteroids => asteroids)
                .Where(asteroid => asteroid["is_potentially_hazardous_asteroid"].Value<bool>())
                .Select(asteroid =>
                {
                    var estimatedDiameter = asteroid["estimated_diameter"].ToObject<EstimatedDiameter>();
                    var maxDiameter = asteroid["estimated_diameter"]["kilometers"]["estimated_diameter_max"].Value<double>();
                    var minDiameter = asteroid["estimated_diameter"]["kilometers"]["estimated_diameter_min"].Value<double>();

                    return new NearEarthObjects
                    {
                        id = (string)asteroid["id"],
                        name = asteroid["name"].Value<string>(),
                        estimated_diameter = estimatedDiameter,
                        mediaDiametro = CalculaMedia(maxDiameter, minDiameter),
                        close_approach_data = asteroid["close_approach_data"].FirstOrDefault()?.ToObject<CloseApproachDatum>()
                    };
                })
                .ToList();

            return nearEarthObjectsLista;
        }






        private double CalculaMedia(double a, double b)
        {
            return ((a + b) / 2);
        }
    }

}
