namespace FBToken.Main.TokenHandler
{
    public class TokenGetterTest
    {
        public static string GetSuccessResponse()
        {
            string response =
                "{\"session_key\":\"5.bPddj9uNqQDvQA.1567394649.24-100011632516800\",\"uid\":100011632516800,\"secret\":\"d0cbd958c884b19ff3bbea94ad7f65b3\",\"access_token\":\"EAAAAUaZA8jlABAP7Ac0xGHO4DxsflK0wlbWRo37sZA3tSDpyq6XxpZAY7fqrLFbNIMrQkYZBhDW296mpdY1Sj1xlaZCNRUT3bJSnUgRLpkcwQ8kT00ZBo0PXAGWnHCqt1QZAzLdeaPzxbr0uUG02RHTC77ZB7uUuehinaZCeIrXpiTGyMdRoQLLN8\",\"machine_id\":\"WYtsXQWUqZkIfnL0Rg6xgnPT\",\"session_cookies\":[{\"name\":\"c_user\",\"value\":\"100011632516800\",\"expires\":\"Tue, 01 Sep 2020 03:24:09 GMT\",\"expires_timestamp\":1598930649,\"domain\":\".facebook.com\",\"path\":\"/\",\"secure\":true},{\"name\":\"xs\",\"value\":\"24:bPddj9uNqQDvQA:2:1567394649:18175:6254\",\"expires\":\"Tue, 01 Sep 2020 03:24:09 GMT\",\"expires_timestamp\":1598930649,\"domain\":\".facebook.com\",\"path\":\"/\",\"secure\":true,\"httponly\":true},{\"name\":\"fr\",\"value\":\"3OrtxXqvmiVACaYbb.AWUbe8XVx0IRTm7hIPVOn9KZ_4Y.BdbItZ..AAA.0.0.BdbItZ.AWWWhp69\",\"expires\":\"Tue, 01 Sep 2020 03:24:07 GMT\",\"expires_timestamp\":1598930647,\"domain\":\".facebook.com\",\"path\":\"/\",\"secure\":true,\"httponly\":true},{\"name\":\"datr\",\"value\":\"WYtsXQWUqZkIfnL0Rg6xgnPT\",\"expires\":\"Wed, 01 Sep 2021 03:24:09 GMT\",\"expires_timestamp\":1630466649,\"domain\":\".facebook.com\",\"path\":\"/\",\"secure\":true,\"httponly\":true}],\"confirmed\":true,\"identifier\":\"namqhong@outlook.com\",\"user_storage_key\":\"356af9c141ca1f44ca9891b54c6e822b3fc26f91e8d88aad35c1fd3a8a69280b\"}";
            return response;
        }

        public static string GetInvalidPasswordResponse()
        {
            string response =
                "{\"error_code\":401,\"error_msg\":\"Invalid username or password (401)\",\"error_data\":\"{\\\"error_title\\\":\\\"Incorrect Password\\\",\\\"error_message\\\":\\\"The password you entered is incorrect. Please try again.\\\"}\",\"request_args\":[{\"key\":\"method\",\"value\":\"auth.login\"},{\"key\":\"adid\",\"value\":\"f08b436f-366c-401f-9a22-0468f278c290\"},{\"key\":\"format\",\"value\":\"json\"},{\"key\":\"device_id\",\"value\":\"ad323d0f-1f3c-4803-9ac0-ec5a630415fa\"},{\"key\":\"email\",\"value\":\"namqhong@outlook.com\"},{\"key\":\"password\",\"value\":\"--sanitized--\"},{\"key\":\"cpl\",\"value\":\"true\"},{\"key\":\"family_device_id\",\"value\":\"ad323d0f-1f3c-4803-9ac0-ec5a630415fa\"},{\"key\":\"credentials_type\",\"value\":\"device_based_login_password\"},{\"key\":\"generate_session_cookies\",\"value\":\"1\"},{\"key\":\"error_detail_type\",\"value\":\"button_with_disabled\"},{\"key\":\"source\",\"value\":\"device_based_login\"},{\"key\":\"machine_id\",\"value\":\"m9h2khu69eft2vi0v26og1d8\"},{\"key\":\"meta_inf_fbmeta\",\"value\":\"\"},{\"key\":\"advertiser_id\",\"value\":\"f08b436f-366c-401f-9a22-0468f278c290\"},{\"key\":\"currently_logged_in_userid\",\"value\":\"0\"},{\"key\":\"locale\",\"value\":\"en_US\"},{\"key\":\"client_country_code\",\"value\":\"US\"},{\"key\":\"fb_api_req_friendly_name\",\"value\":\"authenticate\"},{\"key\":\"fb_api_caller_class\",\"value\":\"com.facebook.account.login.protocol.Fb4aAuthHandler\"},{\"key\":\"api_key\",\"value\":\"882a8490361da98702bf97a021ddc14d\"},{\"key\":\"sig\",\"value\":\"df6cd2f73e3cd9ee6152918b20da7bd9\"}]}";
            return response;
        }
    }
}
