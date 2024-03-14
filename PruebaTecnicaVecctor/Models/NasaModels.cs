using Newtonsoft.Json;
using PruebaTecnicaVecctor.DTOs;
using PruebaTecnicaVecctor.Entidad;

namespace PruebaTecnicaVecctor.Models
{
    public class NasaModels
    {
        [JsonProperty("CodigoResultado")]
        public int CodigoResultado { get; set; }

        [JsonProperty("Mensaje")]
        public string Mensaje { get; set; }

        [JsonProperty("NasaApi")]
        public List<NasaAsteroids> NasaAsteroids { get; set; }
    }
}
