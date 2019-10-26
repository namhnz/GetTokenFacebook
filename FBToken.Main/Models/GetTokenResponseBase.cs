namespace FBToken.Main.Models
{
    public class GetTokenResponseBase : ResponseBase
    {
        public UserTokenInfo UserTokenInfo { get; private set; }

        private GetTokenResponseBase(bool success, string message, UserTokenInfo userTokenInfo) : base(success, message)
        {
            UserTokenInfo = userTokenInfo;
        }
        public GetTokenResponseBase(string message) : this(false, message, null)
        {
        }

        public GetTokenResponseBase(UserTokenInfo userTokenInfo) : this(true, null, userTokenInfo)
        {
        }
    }
}