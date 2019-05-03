using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using FBToken.Main.Core;
using FBToken.Main.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FBToken.Tests
{
    [TestFixture]
    public class FacebookTokenServiceTest
    {
        [Test]
        public async Task FacebookTokenService_RightUsernameAndPassword_ReturnToken()
        {
            //Arrange
            string email = "testemail@gmail.com";
            string password = "passoftest";

            string baseRequestUrl = @"https://b-graph.facebook.com/auth/login";
            string requestParamString =
                $"email={email}&password={password}&access_token=6628568379|c1e620fa708a1d5696fb991c1bde5662&method=post";

            string token = "token_123456789";

            FBGetTokenRequesterStub_Success requesterStub = new FBGetTokenRequesterStub_Success("GetTokenResponse_DataFiles\\GetTokenResponse_Content_Success.json");
            FacebookTokenService tokenServiceSUT = new FacebookTokenService(requesterStub);

            //Act
            UserTokenInfo tokenInfoResult = await tokenServiceSUT.GetTokenInfoAsync(email, password);

            //Assert
            Assert.AreEqual($"{baseRequestUrl}?{requestParamString}", requesterStub.CalledUrl);
            Assert.AreEqual(token, tokenInfoResult.AccessToken);
        }

        [Test]
        public async Task FacebookTokenService_RightUsernameAndPassword_CheckpointException()
        {
            //Arrange
            string email = "testemail@gmail.com";
            string password = "passoftest";

            string baseRequestUrl = @"https://b-graph.facebook.com/auth/login";
            string requestParamString =
                $"email={email}&password={password}&access_token=6628568379|c1e620fa708a1d5696fb991c1bde5662&method=post";

            FBGetTokenRequesterStub_Checkpoint requesterStub = new FBGetTokenRequesterStub_Checkpoint();
            FacebookTokenService tokenServiceSUT = new FacebookTokenService(requesterStub);

            Exception threwException = null;

            //Act
            try
            {
                UserTokenInfo tokenInfoResult = await tokenServiceSUT.GetTokenInfoAsync(email, password);
            }
            catch (Exception ex)
            {
                threwException = ex;
            }

            //Assert
            Assert.IsInstanceOf<FacebookUserCheckPointException>(threwException);
        }
    }

    public class FBGetTokenRequesterStub_Success : IWebRequester
    {
        //https://stackoverflow.com/questions/5311023/mocking-generic-method-call-for-any-given-type-parameter

        private string _responseDataFilePath;

        public FBGetTokenRequesterStub_Success(string responseResponseDataFilePath)
        {
            _responseDataFilePath = responseResponseDataFilePath;
        }

        public Task PostRequestAsync<T>(string endpoint, object data, string args = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetRequestAsync<T>(string endpoint, string args = null)
        {
            CalledUrl = $"{endpoint}?{args}";

            //https://stackoverflow.com/questions/6080596/how-can-i-load-this-file-into-an-nunit-test
            string praticeResponseResult = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, _responseDataFilePath));
            T responseResult = JsonConvert.DeserializeObject<T>(praticeResponseResult);
            return Task.FromResult(responseResult);
        }

        public string CalledUrl { get; set; }
    }

    public class FBGetTokenRequesterStub_Checkpoint : IWebRequester
    {
        public Task PostRequestAsync<T>(string endpoint, object data, string args = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetRequestAsync<T>(string endpoint, string args = null)
        {
            throw new FacebookUserCheckPointException();
        }
    }
}