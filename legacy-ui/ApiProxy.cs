using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Legacy.UI
{
    public class ApiProxy
    {
        private const string API_URL = "https://api.openweathermap.org/data/2.5/forecast";
        private readonly string _appId;

        public ApiProxy(string appId)
        {
            _appId = appId;
        }

        public virtual dynamic GetData(string location)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = client.GetAsync($"?q={location}&units=metric&appid={_appId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<dynamic>(content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ApplicationException($"Ciudad '{location}' no encontrada");
                }
                else
                {
                    throw new ApplicationException($"{response.StatusCode} ({response.ReasonPhrase})");
                }
            }
        }
    }
}
