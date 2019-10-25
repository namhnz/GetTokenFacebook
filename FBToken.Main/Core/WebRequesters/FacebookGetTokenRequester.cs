using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FBToken.Main.Core.WebRequester
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

        

        public async Task<T> GetRequestAsync<T>(string endpoint = FBTokenAPIUrlBase, string args = null)
        {
            string requestUrlString = args != null ? $"{endpoint}?{args}" : endpoint;
            var response = await _httpClient.GetAsync(requestUrlString);
            var resultString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(resultString);
        }

        public Task<T> PostRequestAsync<T>(string endpoint, object data, string args = null)
        {
            throw new NotImplementedException();
        }

        private StringContent GetPayload(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}