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

            if(days is null) 
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

            

            var datosObtenidos = _apiService.ObtenerDatos(DateTime.Now.ToString("yyyy-DD-dd") , DateTime.Now.AddDays(Double.Parse(days.ToString())).ToString("yyyy-MM-dd"));

            var asteroides = datosObtenidos.Select((x, index) => new
            {
                Datos = x.near_earth_objects,
                Index = index
            })
                 .Where(obj => obj.Datos.is_potentially_hazardous_asteroid)
                 .Select(obj => new NasaAsteroids
                 {
                     Id = obj.Index + 1, // Sumar 1 para comenzar en 1 en lugar de 0
                     Name = obj.Datos.name,
                     Diametro = obj.Datos.mediaDiametro,
                     Velocidad = double.Parse(obj.Datos.close_approach_data.relative_velocity.kilometers_per_hour),
                     Fecha = DateTime.Parse(obj.Datos.close_approach_data.close_approach_date),
                     Planeta = obj.Datos.close_approach_data.orbiting_body
                 })
                 .ToList();


            var asteroids = asteroides.OrderByDescending(datos => datos.Diametro).Take(3).ToList();

            result.Data = new NasaModels()
            {
                CodigoResultado = 0,
                NasaAsteroids = asteroides,
                Mensaje = "El número de días válido es entre 1 y 7."
            };

            return result;
        }
    }
}
