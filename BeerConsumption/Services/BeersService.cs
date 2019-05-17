using BeerConsumption.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace BeerConsumption.Services
{
    public class BeersService
    {
        private static readonly HttpClient _client;
        private const string _contentType = "application/json";

        static BeersService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));
        }

        public BeersService(string baseUrl)
        {
            _client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<IEnumerable<Beer>> Get(string endpoint)
        {
            var jsonAsString = await _client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<List<Beer>>(jsonAsString);
        }

        public async Task<bool> Post(BeerCreation beer, string endpoint)
        {
            var json = JsonConvert.SerializeObject(beer);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);

            var response = await _client.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }

            return false;
        }


        public async Task<Beer> Get(string enpoint, int id)
        {
            var jsonAsString = await _client.GetStringAsync(enpoint);
            return JsonConvert.DeserializeObject<Beer>(jsonAsString);
        }

        public async Task<bool> Pit(BeerCreation beer, string endpoint)
        {
            var json = JsonConvert.SerializeObject(beer);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);

            var response = await _client.PutAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }

            return false;
        }

        public async Task<bool> Delete(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }

            return false;
        }

    }
}