namespace Legacy.UI
{
    public class Service
    {
        public static Service WithDefaultProxy() => new Service(new ApiProxy(Properties.Settings.Default.OpenWeatherAppId));
        public static Service WithProxy(ApiProxy proxy) => new Service(proxy);

        private readonly ApiProxy _apiClient;

        private Service(ApiProxy apiClient)
        {
            _apiClient = apiClient;
        }

        public bool Navegamos(string location)
        {
            var wd = _apiClient.GetData(location);
            return Compute(wd);
        }

        private bool Compute(dynamic wd)
        {
            foreach (var data in wd.list)
            {
                if (data.wind.deg >= 90 && data.wind.deg <= 180)
                    return true;
            }
            return false;
        }
    }
}
