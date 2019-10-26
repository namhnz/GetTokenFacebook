using FBToken.Main.Core.Services.TokenServices;

namespace FBToken.Main.Core.Factories
{
    public class FacebookServiceFactory
    {
        public static IFacebookTokenService GetFacebookTokenServiceInstance()
        {
            IFacebookTokenService fbTokenService = new FacebookTokenService();
            return fbTokenService;
        }
    }
}
