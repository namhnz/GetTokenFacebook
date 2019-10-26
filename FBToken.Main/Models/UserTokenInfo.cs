using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Models
{
    public class UserTokenInfo
    {
        public string SessionKey { get; set; }
        public string Uid { get; set; }
        public string Secret { get; set; }
        public string AccessToken { get; set; }
        public bool Confirmed { get; set; }
        public string Identifier { get; set; }
    }
}
