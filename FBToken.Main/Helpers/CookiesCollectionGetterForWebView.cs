using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace FBToken.Main.Helpers
{
    //https://stackoverflow.com/questions/36016076/uwp-get-cookie-from-website

    public class CookiesCollectionGetterForWebView
    {
        public static HttpCookieCollection GetBrowserCookies(Uri targetUri)
        {
            var httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            var cookieManager = httpBaseProtocolFilter.CookieManager;
            var cookiesCollection = cookieManager.GetCookies(targetUri);

            return cookiesCollection;
        }

        public static void GetBrowserCookiesAndWriteLog(Uri targetUri, string message)
        {
            var cookiesSaved = GetBrowserCookies(targetUri);
            foreach (var cookie in cookiesSaved)
            {
                Debug.WriteLine($"{message} - Cookie cho domain {cookie.Domain}: {cookie.Value}");
            }
        }
    }
}
