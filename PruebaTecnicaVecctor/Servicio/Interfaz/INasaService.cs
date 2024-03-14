using Microsoft.EntityFrameworkCore.Metadata;
using PruebaTecnicaVecctor.Genericos;
using PruebaTecnicaVecctor.Models;

namespace PruebaTecnicaVecctor.Servicio.Interfaz
{
    public interface INasaService
    {
        ServiceResult<NasaModels> ObtenerJson(int? days);
    }
}
