using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using FBToken.Main.Helpers;
using FBToken.Main.ViewModels;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using UWPControls = Windows.UI.Xaml.Controls;
using Toolkit = Microsoft.Toolkit.Wpf.UI.Controls;

namespace FBToken.Main.Views
{
    /// <summary>
    /// Interaction logic for BrowseFacebookView.xaml
    /// </summary>
    public partial class BrowseFacebookView : UserControl
    {
        private BrowseFacebookViewModel _viewModel;

        public BrowseFacebookView()
        {
            InitializeComponent();
            _viewModel = new BrowseFacebookViewModel();
            this.DataContext = _viewModel;

            SetupOtherThings();
        }

        private void SetupOtherThings()
        {
            //Có nên tiến hành clear cookies hay không?
            //Do khi mở WebView thì facebook vẫn lưu trạng thái đăng nhập trước đó

            MainWebView.NavigationCompleted += MainWebViewOnNavigationCompleted;
        }

        private void MainWebViewOnNavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                WebsiteTitle.Text = MainWebView.DocumentTitle;
            }
            else
            {
                WebsiteTitle.Text = e.WebErrorStatus.ToString();
            }

            CookiesCollectionManagerForWebView.GetBrowserCookiesAndWriteLog(new Uri("https://facebook.com/"), "WebView");
        }
    }
}