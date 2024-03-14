using Microsoft.VisualStudio.TestTools.UnitTesting;
using PruebaTecnicaVecctor.Servicio;

namespace PruebaTecnicaTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestFechaNull()
        {

            var servicio = new NasaService();
            var resultado = servicio.ObtenerJson(null);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, resultado.Data.CodigoResultado);
        }
        [TestMethod]
        public void TestFechaMal()
        {

            var servicio = new NasaService();
            var resultado = servicio.ObtenerJson(8);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, resultado.Data.CodigoResultado);
        }

        [TestMethod]
        public void TestFechaBien()
        {

            var servicio = new NasaService();
            var resultado = servicio.ObtenerJson(8);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, resultado.Data.CodigoResultado);
        }

        [TestMethod]
        public void TestAsteroideNoEncontrado()
        {

            var servicio = new NasaService();
            var resultado = servicio.ObtenerJson(8);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("No se han encontrado satelites potencialmente peligrosos", resultado.Data.Mensaje);
        }


        [TestMethod]
        public void TestAsteroideEncontrado()
        {

            var servicio = new NasaService();
            var resultado = servicio.ObtenerJson(8);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(resultado.Data.NasaAsteroids);
        }
    }
}