using PruebaTecnicaVecctor.DTOs;

namespace PruebaTecnicaVecctor.Servicio.Interfaz
{
    public interface INasaApiService
    {
        DatoApiNasaDTO ObtenerDatos(string ahora, string hasta);
    }
}
