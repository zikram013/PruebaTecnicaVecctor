using Moq;
using PruebaTecnicaVecctor.DTOs;
using PruebaTecnicaVecctor.Servicio;
using PruebaTecnicaVecctor.Servicio.Interfaz;

namespace PruebaUnitariaTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            var mockApiService = new Mock<INasaApiService>();
            mockApiService.Setup(api => api.ObtenerDatos(It.IsAny<string>(), It.IsAny<string>()))
             .Returns(new List<DatoApiNasaDTO>());
            var servicio = new NasaService(mockApiService.Object);
            var resultado = servicio.ObtenerJson(null);
            Assert.AreEqual(1, resultado.Data.CodigoResultado);
        }


        [TestMethod]
        public void TestFechaMal()
        {

            var mockApiService = new Mock<INasaApiService>();
            mockApiService.Setup(api => api.ObtenerDatos(It.IsAny<string>(), It.IsAny<string>()))
             .Returns(new List<DatoApiNasaDTO>());
            var servicio = new NasaService(mockApiService.Object);
            var resultado = servicio.ObtenerJson(8);
            Assert.AreEqual(1, resultado.Data.CodigoResultado);
        }

        [TestMethod]
        public void TestFechaBien()
        {

            var mockApiService = new Mock<INasaApiService>();
            mockApiService.Setup(api => api.ObtenerDatos(It.IsAny<string>(), It.IsAny<string>()))
             .Returns(new List<DatoApiNasaDTO>());
            var servicio = new NasaService(mockApiService.Object);
            var resultado = servicio.ObtenerJson(6);
            Assert.AreEqual(0, resultado.Data.CodigoResultado);
        }

        [TestMethod]
        public void TestAsteroideNoEncontrado()
        {

            var mockApiService = new Mock<INasaApiService>();
            mockApiService.Setup(api => api.ObtenerDatos(It.IsAny<string>(), It.IsAny<string>()))
             .Returns(new List<DatoApiNasaDTO>());
            var servicio = new NasaService(mockApiService.Object);
            var resultado = servicio.ObtenerJson(3);
            Assert.AreEqual("No se han encontrado satelites para ese rango de fechas", resultado.Data.Mensaje);
        }


        [TestMethod]
        public void TestAsteroideEncontrado()
        {

            var mockApiService = new Mock<INasaApiService>();
            mockApiService.Setup(api => api.ObtenerDatos(It.IsAny<string>(), It.IsAny<string>()))
             .Returns(new List<DatoApiNasaDTO>());
            var servicio = new NasaService(mockApiService.Object);
            var resultado = servicio.ObtenerJson(1);
            Assert.IsNotNull(resultado.Data.NasaAsteroids);
        }
    }
}