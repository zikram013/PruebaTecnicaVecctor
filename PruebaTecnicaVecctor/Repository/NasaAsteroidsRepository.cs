using PruebaTecnicaVecctor.ContextoBBDD;
using PruebaTecnicaVecctor.Entidad;
using PruebaTecnicaVecctor.Repository.Interfaz;

namespace PruebaTecnicaVecctor.Repository
{
    public class NasaAsteroidsRepository : Repository<NasaAsteroids>, INasaAsteroidsRepository
    {
        /*
 Al no existir operaciones especiales, no hace falta definir métodos porque ya se encuentran
en el repository generico
 */
        public NasaAsteroidsRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
