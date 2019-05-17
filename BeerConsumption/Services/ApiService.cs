using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BeerConsumption.Services
{
    public class ApiService<T, M>: IApiService<T, M>
    {
        private static readonly HttpClient _client;
        private const string _contentType = "application/json";
        private const string _baseUrl = @"http://localhost:8000/api/";

        static ApiService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<T>> GetAll(string endpoint)
        {
            var jsonAsString = await _client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<List<T>>(jsonAsString);
        }

        public async Task<T> Get(string enpoint)
        {
            var jsonAsString = await _client.GetStringAsync(enpoint);
            return JsonConvert.DeserializeObject<T>(jsonAsString);
        }

        public async Task<bool> Post(M beer, string endpoint)
        {
            var response = await _client.PostAsync(endpoint, GetRequestContent(beer));
            return await GetResultFromResponse(response);
        }

        public async Task<bool> Put(M beer, string endpoint)
        {
            var response = await _client.PutAsync(endpoint, GetRequestContent(beer));
            return await GetResultFromResponse(response);
        }

        public async Task<bool> Delete(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);
            return await GetResultFromResponse(response);
        }

        private StringContent GetRequestContent(M beer)
        {
            var json = JsonConvert.SerializeObject(beer);
            var content =  new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
            return content;
        }

        private async Task<bool> GetResultFromResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }

            return false;
        }
    }
}