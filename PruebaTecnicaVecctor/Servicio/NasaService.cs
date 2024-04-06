using PruebaTecnicaVecctor.DTOs;
using PruebaTecnicaVecctor.Entidad;
using PruebaTecnicaVecctor.Genericos;
using PruebaTecnicaVecctor.Models;
using PruebaTecnicaVecctor.Servicio.Interfaz;

namespace PruebaTecnicaVecctor.Servicio
{
    public class NasaService : INasaService
    {
        private readonly INasaApiService _apiService;

        public NasaService(INasaApiService apiService)
        {
            _apiService = apiService;
        }

        public ServiceResult<NasaModels> ObtenerJson(int? days)
        {
            var result = new ServiceResult<NasaModels>();

            if (days is null)
            {
                result.Data = new NasaModels()
                {
                    CodigoResultado = 1,
                    NasaAsteroids = null,
                    Mensaje = "Es obligatorio especificar el número de días. Entre 1 y 7."
                };
                return result;
            }

            if (days > 7)
            {
                result.Data = new NasaModels()
                {
                    CodigoResultado = 1,
                    NasaAsteroids = null,
                    Mensaje = "El número de días válido es entre 1 y 7."
                };
                return result;
            }



            var datosObtenidos = _apiService.ObtenerDatos(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(Double.Parse(days.ToString())).ToString("yyyy-MM-dd"));

            if (datosObtenidos.Count() == 0) 
            {
                result.Data = new NasaModels()
                {
                    CodigoResultado = 0,
                    NasaAsteroids = new List<NasaAsteroids>(),
                    Mensaje = "No se han encontrado satelites para ese rango de fechas"
                };
                return result;
            }

            if (!string.IsNullOrEmpty(datosObtenidos.FirstOrDefault().mensaje)) 
            {
                result.Data = new NasaModels()
                {
                    CodigoResultado = 1,
                    NasaAsteroids = null,
                    Mensaje = datosObtenidos.FirstOrDefault().mensaje
                };
                return result;
            }



            var asteroides = datosObtenidos.Select(x => x.near_earth_objects)
                .Where(obj => obj.is_potentially_hazardous_asteroid)
                .Select(obj => new NasaAsteroids
                {
                    Id = int.Parse(obj.id),
                    Name = obj.name,
                    Diametro = obj.mediaDiametro,
                    Velocidad = double.Parse(obj.close_approach_data.relative_velocity.kilometers_per_hour),
                    Fecha = DateTime.Parse(obj.close_approach_data.close_approach_date),
                    Planeta = obj.close_approach_data.orbiting_body
                })
                .ToList();



            if (asteroides.Count() == 0)
            {
                result.Data = new NasaModels()
                {
                    CodigoResultado = 0,
                    NasaAsteroids = asteroides,
                    Mensaje = "No se han encontrado satelites potencialmente peligrosos"
                };
                return result;
            }

            var asteroids = asteroides.OrderByDescending(datos => datos.Diametro).Take(3).ToList();

            result.Data = new NasaModels()
            {
                CodigoResultado = 0,
                NasaAsteroids = asteroides,
                Mensaje = "Asteroides encontrados"
            };

            return result;
        }
    }
}
