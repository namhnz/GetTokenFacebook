using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FBToken.Main.Models;
using Microsoft.CSharp.RuntimeBinder;

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
            var result = await _requester.GetRequestAsync<dynamic>(baseUrl, paramString);

            //https://stackoverflow.com/questions/2998954/test-if-a-property-is-available-on-a-dynamic-variable
            try
            {
                //Khối này được sử dụng để kiểm tra kết quả trả về có phải lỗi hay không
                if (result.error.code == 405)
                {
                    throw new FacebookUserCheckPointException(result.error.message.ToString());
                }

                if (result.error.code == 401)
                {
                    throw new FacebookUserInvalidUsernameOrPasswordException(result.error.message.ToString());
                }
            }
            catch (RuntimeBinderException ex)
            {
            }

            try
            {
                //Khối này được sử dụng để kiểm tra kết quả trả về có là kết quả chứa token hay không
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
            catch (RuntimeBinderException ex)
            {
            }

            if (result == null)
            {
                //Khi không có kết quả trả về từ server
                return null;
            }
            else
            {
                //Có kết quả trả về từ server nhưng không nằm trong các trường hợp error và token info ở trên
                return new UserTokenInfo();
            }
        }
    }
}