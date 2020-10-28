using Legacy.UI;

namespace Legacy.Tests
{
    internal class FakeApiProxy : ApiProxy
    {
        private readonly dynamic _jsonData;

        public FakeApiProxy(dynamic jsonData) : base(null)
        {
            _jsonData = jsonData;
        }

        public override dynamic GetData(string location) => _jsonData;
    }
}