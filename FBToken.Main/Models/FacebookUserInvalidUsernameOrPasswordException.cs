using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Models
{
    public class FacebookUserInvalidUsernameOrPasswordException : Exception
    {
        public object ResponseContent { get; }

        public FacebookUserInvalidUsernameOrPasswordException()
        {
            
        }

        public FacebookUserInvalidUsernameOrPasswordException(string message) : base(message)
        {
            
        }

        public FacebookUserInvalidUsernameOrPasswordException(string message, object responseContent) : base(message)
        {
            ResponseContent = responseContent;
        }
    }
}
