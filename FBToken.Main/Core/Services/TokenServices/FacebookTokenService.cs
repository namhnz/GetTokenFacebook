using System;
using System.Diagnostics;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using FBToken.Main.Models;
using FBToken.Main.TokenHandler;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;

namespace FBToken.Main.Core.Services.TokenServices
{
    public class FacebookTokenService : IFacebookTokenService
    {
        //https://stackoverflow.com/questions/14104865/how-can-i-use-debug-write-with-dynamic-data
        public async Task<GetTokenResponseBase> GetTokenInfoAsyncByLocMai(string email, string password)
        {
            try
            {
                var response = await TokenGetter.RequestNew(email, password);

                string result = await response.Content.ReadAsStringAsync();
                dynamic resultJson = JsonConvert.DeserializeObject(result);

                string resultString = resultJson.ToString() as string;
                Debug.WriteLine(resultString);

                try
                {
                    var tokenInfo = new UserTokenInfo()
                    {
                        AccessToken = resultJson["access_token"].ToString() as string,
                        SessionKey = resultJson["session_key"].ToString() as string,
                        Uid = resultJson["uid"].ToString() as string,
                        Secret = resultJson["secret"].ToString() as string,
                        Confirmed = bool.Parse(resultJson["confirmed"].ToString() as string),
                        Identifier = resultJson["identifier"].ToString() as string
                    };
                    return new GetTokenResponseBase(tokenInfo);
                }
                catch (RuntimeBinderException)
                {
                    dynamic errorData = JsonConvert.DeserializeObject(resultJson["error_data"].ToString());
                    return new GetTokenResponseBase(errorData["error_message"].ToString() as string);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new GetTokenResponseBase($"An error occurred when creating new token: {ex.Message}");
            }
            
        }
    }
}