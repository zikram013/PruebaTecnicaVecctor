using Microsoft.EntityFrameworkCore;
using PruebaTecnicaVecctor.Entidad;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace PruebaTecnicaVecctor.ContextoBBDD
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        //Agregamos los modelos en esta parte
        public DbSet<NasaAsteroids> NasaAsteroids { get; set; }
    }
}
