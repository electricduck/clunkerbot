using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClunkerBot.Data;

namespace ClunkerBot.Services
{
    public class OwnApiService
    {
        public async Task<string> QueryApiAsync(string path, string query)
        {
            var appKey = AppSettings.ApiKeys_OpenWeatherMap;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/" + path);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("?appid=" + appKey + "&" + query);

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}