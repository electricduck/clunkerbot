using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClunkerBot.Data;

namespace ClunkerBot.Services
{
    public class WolframApiService
    {
        public async Task<string> QueryApiAsync(string query)
        {
            var appId = AppSettings.ApiKeys_WolframAlpha;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://api.wolframalpha.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"/v2/query?input={query}&appid={appId}&format=plaintext");

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}