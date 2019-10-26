using System.Threading.Tasks;
using FBToken.Main.Models;

namespace FBToken.Main.Core.Services.TokenServices
{
    public interface IFacebookTokenService
    {
        
        Task<GetTokenResponseBase> GetTokenInfoAsyncByLocMai(string email, string password);
    }
}
