using System.Threading.Tasks;

namespace FBToken.Main.Core
{
    public interface IWebRequester
    {
        Task PostRequestAsync<T>(string endpoint, object data, string args = null);
        Task<T> GetRequestAsync<T>(string endpoint, string args = null);
        Task<T> NewPostRequestAsync<T>(string endpoint, object data, string args = null);
    }
}