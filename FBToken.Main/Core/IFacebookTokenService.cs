using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBToken.Main.Models;

namespace FBToken.Main.Core
{
    public interface IFacebookTokenService
    {
        Task<UserTokenInfo> GetTokenInfoAsync(string email, string password);
        Task<UserTokenInfo> NewGetTokenInfoAsync(string email, string password);
    }
}
