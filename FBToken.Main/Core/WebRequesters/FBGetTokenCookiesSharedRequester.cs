﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Newtonsoft.Json;

namespace FBToken.Main.Core.WebRequester
{
    #region Thông tin về implementation này

    //Class này được sử dụng làm requester do nó sử dụng Windows.Web.HttpClient, do đó nó chia sẻ cookies với WebView
    //Mọi thông tin về class này xem tại đây:
    //1. https://www.google.com/search?rlz=1C1CHBF_enVN844VN844&ei=TGTOXJHYBIah-Qbg9pq4Cg&q=get+cookie+from+webview+and+pass+it+to+httpclient+uwp&oq=get+cookie+from+webview+and+pass+it+to+httpclient+uwp&gs_l=psy-ab.3..35i39.26466.27504..27743...0.0..0.118.235.0j2......0....1..gws-wiz.o1CcY3Xjnew
    //2. https://blog.kloud.com.au/2015/07/15/sharing-http-sessions-between-webview-requests-and-httpclient-on-windows-phone/
    //3. http://blog.rajenki.com/2015/01/winrt-shared-cookies-between-webview-and-httpclient/

    #endregion

    public class FBGetTokenCookiesSharedRequester : IWebRequester
    {
        private HttpClient _httpClient;

        public FBGetTokenCookiesSharedRequester()
        {
            _httpClient = new HttpClient();

            GetCookiesUsedByHttpClient();
        }


        public async Task<T> GetRequestAsync<T>(string endpoint, string args = null)
        {
            string requestUrlString = args != null ? $"{endpoint}?{args}" : endpoint;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUrlString));
            var result = await _httpClient.SendRequestAsync(request);
            var resultContent = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(resultContent);
        }

        public async Task<T> PostRequestAsync<T>(string endpoint, object data, string args = null)
        {
            //Cách thêm data dạng form vào POST request: https://social.msdn.microsoft.com/Forums/windows/en-US/e8091bfc-8975-4a52-a353-716401f73846/u81send-file-by-post-using-windowswebhttphttpclient?forum=wpdevelop

            string requestUrlString = args != null ? $"{endpoint}?{args}" : endpoint;

            var formData = data as IEnumerable<KeyValuePair<string, string>>;

            HttpResponseMessage requestResult;
            if (formData != null)
            {
                HttpFormUrlEncodedContent form = new HttpFormUrlEncodedContent(formData);
                requestResult = await _httpClient.PostAsync(new Uri(requestUrlString), form);
            }
            else
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(requestUrlString));
                requestResult = await _httpClient.SendRequestAsync(request);
            }

            var resultContent = await requestResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resultContent);
        }

        private void GetCookiesUsedByHttpClient()
        {
            var filter = new HttpBaseProtocolFilter {AllowAutoRedirect = true};
            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.Default;
            filter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.Default;

            //Lấy và log cookie facebook
            var cookieManager = filter.CookieManager;
            var cookiesSaved = cookieManager.GetCookies(new Uri("https://facebook.com/"));
            foreach (var cookie in cookiesSaved)
            {
                Debug.WriteLine("Cookie sẽ được sử dụng bởi Windows.Web.Http.HttpClient:");
                Debug.WriteLine("Name: " + cookie.Name);
                Debug.WriteLine("Where: " + cookie.Domain + cookie.Path);
                Debug.WriteLine("Value: " + cookie.Value);
            }
        }
    }
}