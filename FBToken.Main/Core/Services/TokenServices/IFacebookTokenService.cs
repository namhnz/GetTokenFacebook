using System.Threading.Tasks;
using FBToken.Main.Models;

namespace FBToken.Main.Core.Services.TokenServices
{
    public interface IFacebookTokenService
    {
        Task<UserTokenInfo> GetTokenInfoAsync(string email, string password);
        Task<UserTokenInfo> GetTokenInfoAsyncUsingPostMethod(string email, string password);
    }
}
