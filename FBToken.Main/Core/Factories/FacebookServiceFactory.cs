using FBToken.Main.Core.Services.TokenServices;
using FBToken.Main.Core.WebRequester;

namespace FBToken.Main.Core.Factories
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
