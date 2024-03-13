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
                    DatoApiNasaDTO = null,
                    Mensaje = "Es obligatorio especificar el número de días. Entre 1 y 7."
                };
                return result;
            }

            if (days > 7) 
            {
                result.Data = new NasaModels()
                {
                    CodigoResultado = 1,
                    DatoApiNasaDTO = null,
                    Mensaje = "El número de días válido es entre 1 y 7."
                };
                return result;
            }


            return result;
        }
    }
}
