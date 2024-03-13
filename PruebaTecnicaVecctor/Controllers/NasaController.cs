using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaVecctor.Servicio.Interfaz;

namespace PruebaTecnicaVecctor.Controllers
{
    [ApiController]
    [Route("NasaController")]
    public class NasaController:ControllerBase
    {
        private readonly ILogger<NasaController> _logger;
        private readonly INasaService _nasaService;

        public NasaController (ILogger<NasaController> logger, INasaService nasaService)
        {
            _logger = logger;
            _nasaService = nasaService;
        }   

        [HttpGet]
        public IActionResult GetAsteroids([FromQuery] int? days)
        {

            var result = _nasaService.ObtenerJson(days);   

            return Ok();
        }
    }
}
