using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using FBToken.Main.Core.Factories;
using FBToken.Main.Core.Repository.Settings;
using FBToken.Main.Core.Services.TokenServices;
using FBToken.Main.Core.Services.UIInteractServices;
using FBToken.Main.Helpers;
using FBToken.Main.Models;

namespace FBToken.Main.ViewModels
{
    //How to build: https://techcommunity.microsoft.com/t5/Windows-Dev-AppConsult/Using-XAML-Islands-on-Windows-10-19H1-fixing-the-quot/ba-p/376330
    public class MainWindowViewModel : IViewModel
    {
        private readonly IFacebookTokenService _fbTokenService;
        private readonly ISettingsRepos _settingsRepos;
        private readonly IDialogService _dialogService;

        #region Phần dữ liệu người dùng nhập vào

        private string _email;

        public string UserEmail
        {
            get { return _email; }
            set
            {
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
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    GetTokenCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Loading indicator

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

        #endregion

        #region Phần kết quả sau khi gửi yêu cầu lấy tokem, gồm thông báo lỗi hoặc token

        private string _error;

        public string ErrorMsg
        {
            get { return _error; }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _fbToken;

        public string FBToken
        {
            get { return _fbToken; }
            set
            {
                if (_fbToken != value)
                {
                    _fbToken = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Các commands cho các buttons

        public DelegateCommand GetTokenCommand { get; private set; }
        public DelegateCommand SaveTokenCommand { get; private set; }

        #endregion

        public MainWindowViewModel()
        {
            _dialogService = DialogServiceFactory.GetDialogServiceInstance();
            _fbTokenService = FacebookServiceFactory.GetFacebookTokenServiceInstance();
            _settingsRepos = SettingsCreatorFactory.GetSetingsInstance();

            GetTokenCommand = new DelegateCommand(async _ =>
                {
                    //Reset result
                    ErrorMsg = null;
                    FBToken = null;
                    //End of - Reset result

                    //Open loading indicator
                    IsGettingData = true;

                    Debug.WriteLine($"Email: {UserEmail} - Password: {UserPassword}");

                    string lastEmail = _settingsRepos.LastLoggedInEmail;
                    if (UserEmail != lastEmail)
                    {
                        //Lưu lại email đã dùng để lấy token mới
                        _settingsRepos.LastLoggedInEmail = UserEmail;
                    }

                    //Request new Facebook token
                    var getTokenResult = await _fbTokenService.GetTokenInfoAsyncByLocMai(UserEmail, UserPassword);
                    if (!getTokenResult.Success)
                    {
                        ErrorMsg = getTokenResult.Message;
                        Debug.WriteLine(ErrorMsg);
                    }
                    else
                    {
                        FBToken = getTokenResult.UserTokenInfo.AccessToken;
                        Debug.WriteLine($"Token: {FBToken}");
                    }

                    //Remove loading indicator
                    IsGettingData = false;
                },
                _ => !string.IsNullOrEmpty(UserEmail) && !string.IsNullOrEmpty(UserPassword) && !IsGettingData);

            SaveTokenCommand = new DelegateCommand(_ =>
            {
                //Cách lưu file text: https://stackoverflow.com/questions/45276878/creating-a-txt-file-and-write-to-it

                string filePath =
                    _dialogService.ShowSaveFileDialog();

                if (filePath == null) return;
                using (var tw = new StreamWriter(filePath, false))
                {
                    try
                    {
                        tw.Write(FBToken); //Thực hiện ghi file
                        Debug.WriteLine($"Token was written to file: {filePath}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            });
        }
    }
}