using Microsoft.EntityFrameworkCore.Metadata;
using PruebaTecnicaVecctor.Genericos;

namespace PruebaTecnicaVecctor.Servicio.Interfaz
{
    public interface INasaService
    {
        ServiceResult<NasaModels> ObtenerJson(int? days);
    }
}
