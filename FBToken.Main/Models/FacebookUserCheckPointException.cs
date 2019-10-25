using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Models
{
    public class FacebookUserCheckPointException : Exception
    {
        public FacebookUserCheckPointException()
        {
            
        }

        public FacebookUserCheckPointException(string message) : base(message)
        {
            
        }
        
    }
}
