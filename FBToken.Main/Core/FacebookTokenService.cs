using System.Threading.Tasks;
using FBToken.Main.Models;

namespace FBToken.Main.Core
{
    public class FacebookTokenService : IFacebookTokenService
    {
        private IWebRequester _requester;

        public FacebookTokenService(IWebRequester requester)
        {
            _requester = requester;
        }

        public async Task<UserTokenInfo> GetTokenInfoAsync(string email, string password)
        {
            string baseUrl = @"https://b-graph.facebook.com/auth/login";
            string paramString =
                $"email={email}&password={password}&access_token=6628568379|c1e620fa708a1d5696fb991c1bde5662&method=post";
            var result = await _requester.GetRequestAsync<dynamic>(baseUrl,paramString);

            if (result == null)
            {
                return new UserTokenInfo();
            }

            var tokenInfo = new UserTokenInfo()
            {
                SessionKey = result.session_key,
                Uid = result.uid,
                Secret = result.secret,
                AccessToken = result.access_token,
                MachineId = result.machine_id,
                Confirmed = result.confirmed,
                Identifier = result.identifier
            };
            return tokenInfo;
        }
    }
}