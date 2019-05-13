using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FBToken.Main.Core;
using FBToken.Main.Helpers;
using FBToken.Main.Models;

namespace FBToken.Main.ViewModels
{
    public class MainWindowViewModel : IViewModel
    {
//        private const string SampleToken =
//            @"EAAAAAYsX7TsBAIfpLu5DzPld7e0Gj3hYZBN9k0AwjmoZCOtwZA4avQjBugGsQ1iEAOlHdPqUFvZAUPC0zzHN517ZB0fx9HY281WFAHnjL22De3gtAzwhD3AoUfu62yAA37D7uV97JpvosCO99vQuaUJ3pqNii2m4QZCjvyOdkBhNqzYYaNiJ9NydR5SzxEYMrUYw";

        private IFacebookTokenService _fbTokenService;
        private ISettingsRepos _settingsRepos;
        private IDialogService _dialogService;

        private string _error;

        public string ErrorMsg
        {
            get { return _error; }
            set {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _email;

        public string UserEmail
        {
            get { return _email; }
            set {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                    GetTokenCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _password;

        public string UserPassword
        {
            get { return _password; }
            set {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    GetTokenCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isGettingData;

        public bool IsGettingData
        {
            get { return _isGettingData; }
            set
            {
                if (_isGettingData != value)
                {
                    _isGettingData = value;
                    OnPropertyChanged();
                    GetTokenCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _fbToken;

        public string FBToken
        {
            get { return _fbToken; }
            set {
                if (_fbToken != value)
                {
                    _fbToken = value;
                    OnPropertyChanged();
                }
            }
        }


        public DelegateCommand GetTokenCommand { get; private set; }
        public DelegateCommand SaveTokenCommand { get; private set; }

        public MainWindowViewModel()
        {
            _dialogService = IOServiceFactory.GetDialogServiceInstance();
            _fbTokenService = FacebookServiceFactory.GetFacebookTokenWithCookiesSharedServiceInstance();
            _settingsRepos = SettingsCreatorFactory.GetSetingsInstance();

            GetTokenCommand = new DelegateCommand(async _ =>
                {
                    //Reset result
                    ErrorMsg = null;
                    FBToken = null;
                    //End of - Reset result
                    IsGettingData = true;


                    Debug.WriteLine($"Email: {UserEmail}");
                    Debug.WriteLine($"Password: {UserPassword}");

                    try
                    {
                        //For testing
                        //await Task.Delay(1000);
                        //throw new Exception("Internet connection error.");
                        //FBToken = SampleToken;
                        //

                        string lastEmail = _settingsRepos.LastLoggedInEmail;
                        if (UserEmail != lastEmail)
                        {
                            ////Tương tự log out khỏi browser khi sử dụng tài khoản mới
                            //CookiesCollectionManagerForWebView.DeleteBrowserCookies(new Uri("https://facebook.com/"));
                            //Tính năng này hiện không hoạt động, do đó trước khi get token cho tài khoản mới thì cần
                            //logout tài khoản cũ và đăng nhập bằng tài khoản mới trên trình duyệt

                            //Lưu lại email đã dùng để lấy token mới
                            _settingsRepos.LastLoggedInEmail = UserEmail;
                        }

                        var tokenInfo = await _fbTokenService.GetTokenInfoAsync(UserEmail, UserPassword);
                        if (tokenInfo?.AccessToken == null)
                        {
                            throw new Exception("Đã có lỗi xảy ra, vui lòng thử lại.");
                        }

                        FBToken = tokenInfo.AccessToken;
                        

                        Debug.WriteLine($"Token: {FBToken}");

                    }
                    catch (HttpRequestException ex)
                    {
                        ErrorMsg = "Vui lòng kiểm tra kết nối Internet.";
                        Debug.WriteLine(ex.Message);
                    }
                    catch (FacebookUserCheckPointException ex)
                    {
                        ErrorMsg =
                            "Tài khoản Facebook bị Checkpoint, vui lòng đăng nhập bằng trình duyệt trên máy tính hoặc điện thoại.";
                        Debug.WriteLine(ex.Message);
                    }
                    catch (FacebookUserInvalidUsernameOrPasswordException ex)
                    {
                        ErrorMsg =
                            "Sai email hoặc password. Vui lòng kiểm tra lại thông tin.";
                        Debug.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.Message;
                        Debug.WriteLine(ex.Message);
                    }

                    
                    IsGettingData = false;
                },
                _ =>
                {
                    return !string.IsNullOrEmpty(UserEmail) && !string.IsNullOrEmpty(UserPassword) && !IsGettingData;
                });

            SaveTokenCommand = new DelegateCommand(_ =>
            {
                //Lưu file text: https://stackoverflow.com/questions/45276878/creating-a-txt-file-and-write-to-it
                string filePath = _dialogService.ShowSaveFileDialog();
                if (filePath != null)
                {
                    using (var tw = new StreamWriter(filePath, false))
                    {
                        tw.Write(FBToken);
                    }
                }
            });
        }
    }
}
