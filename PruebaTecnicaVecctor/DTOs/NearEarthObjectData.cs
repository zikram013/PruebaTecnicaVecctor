using Newtonsoft.Json;

namespace PruebaTecnicaVecctor.DTOs
{
    public class NearEarthObjectData
    {
        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardous { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; }

        [JsonProperty("close_approach_data")]
        public List<CloseApproachDatum> CloseApproachData { get; set; }
    }
}
