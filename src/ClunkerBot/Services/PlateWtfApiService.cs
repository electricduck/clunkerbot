using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClunkerBot.Data;

namespace ClunkerBot.Services
{
    public class PlateWtfApiService
    {
        public async Task<string> QueryApiAsync(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{AppSettings.Endpoints_PlateWtf}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/{path}");

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }

        public async Task<string> GetPlate(string plate, string country = "any")
        {
            return await QueryApiAsync($"plate/{country}/{plate}");
        }
    }
}