using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.UI
{
    public class Service
    {
        private const string API_URL = "https://api.openweathermap.org/data/2.5/forecast";

        public bool Navegamos(string location)
        {
            var appId = Properties.Settings.Default.OpenWeatherAppId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = client.GetAsync($"?q={location}&units=metric&appid={appId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var wd = JsonConvert.DeserializeObject<dynamic>(content);
                    return Compute(wd);
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
