using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FBToken.Main.Ultis;

namespace FBToken.Main.TokenHandler
{
    public class TokenGetter
    {
        public static async Task<HttpResponseMessage> RequestNew(string email, string password)
        {
            var sim = RandomNumberGenerator.NumberCalculate(EToInt("2e4"), EToInt("4e4")).ToString();
            var deviceID = Guid.NewGuid().ToString();
            var adID = Guid.NewGuid().ToString();
            var formData = new Dictionary<string, string>()
            {
                {"adid", adID},
                {"format", "json"},
                {"device_id", deviceID},
                {"email", email},
                {"password", password},
                {"cpl", "true"},
                {"family_device_id", deviceID},
                {"credentials_type", "device_based_login_password"},
                {"generate_session_cookies", "1"},
                {"error_detail_type", "button_with_disabled"},
                {"source", "device_based_login"},
                {"machine_id", RandomStringGenerator.StringCalculate(24)},
                {"meta_inf_fbmeta", ""},
                {"advertiser_id", adID},
                {"currently_logged_in_userid", "0"},
                {"locale", "en_US"},
                {"client_country_code", "US"},
                {"method", "auth.login"},
                {"fb_api_req_friendly_name", "authenticate"},
                {"fb_api_caller_class", "com.facebook.account.login.protocol.Fb4aAuthHandler"},
                {"api_key", "882a8490361da98702bf97a021ddc14d"}
            };
            formData.Add("sig", GetSig(SortDictionary.Sort(formData)));

            using (var client = new HttpClient())
            {
                using (var request =
                    new HttpRequestMessage(HttpMethod.Post, "https://b-api.facebook.com/method/auth.login"))
                {
                    request.Headers.TryAddWithoutValidation("x-fb-connection-bandwidth",
                        RandomNumberGenerator.NumberCalculate(EToInt("2e7"), EToInt("3e7")).ToString());
                    request.Headers.TryAddWithoutValidation("x-fb-sim-hni", sim);
                    request.Headers.TryAddWithoutValidation("x-fb-net-hni", sim);
                    request.Headers.TryAddWithoutValidation("x-fb-connection-quality", "EXCELLENT");
                    request.Headers.TryAddWithoutValidation("x-fb-connection-type",
                        "cell.CTRadioAccessTechnologyHSDPA");
                    request.Headers.TryAddWithoutValidation("user-agent",
                        "Dalvik/1.6.0 (Linux; U; Android 4.4.2; NX55 Build/KOT5506) [FBAN/FB4A;FBAV/106.0.0.26.68;FBBV/45904160;FBDM/{density=3.0,width=1080,height=1920};FBLC/it_IT;FBRV/45904160;FBCR/PosteMobile;FBMF/asus;FBBD/asus;FBPN/com.facebook.katana;FBDV/ASUS_Z00AD;FBSV/5.0;FBOP/1;FBCA/x86:armeabi-v7a;]");
                    request.Headers.TryAddWithoutValidation("content-type", "application/x-www-form-urlencoded");
                    request.Headers.TryAddWithoutValidation("x-fb-http-engine", "Liger");

                    request.Content = new StringContent(QueryString.Stringify(formData), Encoding.UTF8,
                        "application/x-www-form-urlencoded");

//                    using (var response = await client.SendAsync(request).ConfigureAwait(false))
//                    {
//                        //return await response.Content.ReadAsStringAsync();
//                        return response;
//                    }
                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    return response;
                }
            }
        }

        private static int HexToInt(string hexValue)
        {
            //https://stackoverflow.com/questions/9820165/convert-hexadecimal-string-to-its-numerical-values-in-c-sharp

            if (hexValue.StartsWith("0x"))
            {
                return Convert.ToInt32(hexValue, 16);
            }
            else
            {
                return int.Parse(hexValue, NumberStyles.HexNumber);
            }
        }

        private static int EToInt(string eValue)
        {
            eValue = eValue.ToLower();
            if (eValue.Contains("e"))
            {
                var numAndTenPowByPair = eValue.Split(new[] {'e'}, StringSplitOptions.RemoveEmptyEntries);
                if (numAndTenPowByPair.Length == 2)
                {
                    return (int) (int.Parse(numAndTenPowByPair[0]) * Math.Pow(10, int.Parse(numAndTenPowByPair[1])));
                }
            }

            return 0;
        }

        private static string GetSig(Dictionary<string, string> formData)
        {
            var sig = "";
            foreach (var key in formData.Keys)
            {
                sig += $"{key}={formData[key]}";
            }

            sig = Md5Crypto.HashCalculate(sig + "62f8ce9f74b12f84c123cc23437a4a32");
            return sig;
        }
    }
}