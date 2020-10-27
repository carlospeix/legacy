using NUnit.Framework;
using Legacy.Tests.Helpers;

namespace Legacy.Tests
{
    [TestFixture]
    public class LegacyEndToEndTests
    {
        ApplicationRunner application;

        [Test]
        public void CanStartLegacyUi()
        {
            application.StartLegacyUi();
        }

        [Test]
        public void CanRequestResultForQuilmes()
        {
            application.StartLegacyUi();
            application.RequestWeatherFor("Quilmes");
            
            var result = application.GetResultText();

            Assert.Contains(result, new string[] { "SI!!!", "No, mantenimiento!" });
        }

        [SetUp]
        public void Setup()
        {
            application = new ApplicationRunner();
        }

        [TearDown]
        public void TearDown()
        {
            application.Stop();
        }
    }
}