using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaVecctor.Entidad
{
    public class NasaAsteroids
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Esta anotación hace que el ID sea autoincremental
        public int Id { get; set; }
        public string Name { get; set; }
        public double Diametro { get; set; }
        public double Velocidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Planeta { get; set; }
    }
}
