using Legacy.UI;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Legacy.Tests
{
    [TestFixture]
    public class LegacyServiceTests
    {
        [Test]
        public void SePuedeNavegarConSudeste()
        {
            var jsonConSudeste = JsonConvert.DeserializeObject<dynamic>(@"{
                'list': [ { 'wind': { 'deg': 170 } } ]
            }");

            var service = Service.WithProxy(new FakeApiProxy(jsonConSudeste));

            Assert.That(service.Navegamos("irrelevante"), Is.True);
        }

        [Test]
        public void NoSePuedeNavegarConNorte()
        {
            var jsonConNorte = JsonConvert.DeserializeObject<dynamic>(@"{
                'list': [ { 'wind': { 'deg': 0 } } ]
            }");

            var service = Service.WithProxy(new FakeApiProxy(jsonConNorte));

            Assert.That(service.Navegamos("irrelevante"), Is.False);
        }
    }
}
