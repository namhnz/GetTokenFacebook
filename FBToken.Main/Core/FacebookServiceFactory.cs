using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Core
{
    public class FacebookServiceFactory
    {
        public static IFacebookTokenService GetFacebookTokenServiceInstance()
        {
            IWebRequester requester = new FacebookGetTokenRequester();
            IFacebookTokenService fbTokenService = new FacebookTokenService(requester);
            return fbTokenService;
        }

        public static IFacebookTokenService GetFacebookTokenWithCookiesSharedServiceInstance()
        {
            IWebRequester requester = new FBGetTokenCookiesSharedRequester();
            IFacebookTokenService fbTokenService = new FacebookTokenService(requester);
            return fbTokenService;
        }
    }
}
