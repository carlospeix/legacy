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
            Assert.AreEqual("[resultado]", application.GetResultText());
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

        //public void CanStartLegacyUi2()
        //{
        //    var jsonConSudeste = @"{
        //        'list': [ { 'wind': { 'deg': 135 } } ]
        //    }";
        //    var runner = new ApplicationRunner(new Service(new FakeApiClient(jsonConSudeste)));

        //    runner.ForecastFor("Quilmes");
        //    runner.Shows("SI!!!");
        //}
    }
}