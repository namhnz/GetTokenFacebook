using System.Threading.Tasks;

namespace FBToken.Main.Core.WebRequester
{
    public interface IWebRequester
    {
        Task<T> GetRequestAsync<T>(string endpoint, string args = null);
        Task<T> PostRequestAsync<T>(string endpoint, object data, string args = null);
    }
}