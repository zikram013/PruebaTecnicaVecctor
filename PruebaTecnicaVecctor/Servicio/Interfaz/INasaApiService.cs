using PruebaTecnicaVecctor.DTOs;

namespace PruebaTecnicaVecctor.Servicio.Interfaz
{
    public interface INasaApiService
    {
        List<DatoApiNasaDTO> ObtenerDatos(string ahora, string hasta);
    }
}
