using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FBToken.Main.Models;
using Newtonsoft.Json;

namespace FBToken.Main.Core
{
    public class FacebookGetTokenRequester : IWebRequester
    {
        //https://piotrgankiewicz.com/2017/02/06/accessing-facebook-api-using-c/

        private const string FBTokenAPIUrlBase = @"https://b-graph.facebook.com/auth/login";

        private readonly HttpClient _httpClient;

        public FacebookGetTokenRequester()
        {
            _httpClient = new HttpClient();
        }

        public async Task PostRequestAsync<T>(string endpoint = FBTokenAPIUrlBase, object data = null, string args = null)
        {
            var payload = GetPayload(data);
            await _httpClient.PostAsync($"{endpoint}?{args}", payload);
        }

        public async Task<T> GetRequestAsync<T>(string endpoint = FBTokenAPIUrlBase, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?{args}");
            var resultString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(resultString);
        }

        private StringContent GetPayload(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}