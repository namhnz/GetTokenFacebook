using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using FBToken.Main.Converters;
using FBToken.Main.ViewModels;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using TextWrapping = Windows.UI.Xaml.TextWrapping;
using UWPControls = Windows.UI.Xaml.Controls;
using Visibility = System.Windows.Visibility;
using Window = System.Windows.Window;

namespace FBToken.Main.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private bool _isBrowserOpenning;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            this.DataContext = _viewModel;

            ModifyUWPComponents();
        }

        public void ModifyUWPComponents()
        {
            UserInputStackPanel.ChildChanged += UserInputStackPanelOnChildChanged;
            IsGettingDataProgressRing.ChildChanged += IsGettingDataProgressRingOnChildChanged;
            ResultStackPanel.ChildChanged += ResultStackPanelOnChildChanged;

            OpenCloseBrowserButton.ChildChanged += OpenCloseBrowserButtonOnChildChanged;
        }

        private void OpenCloseBrowserButtonOnChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost host && host.Child is UWPControls.Button openCloseButton)
            {
                string openBrowseFacebookViewString = "Đăng nhập bằng trình duyệt";
                string closeBrowseFacebookViewString = "Đóng trình duyệt";

                openCloseButton.Content = openBrowseFacebookViewString;

                openCloseButton.Click += (o, args) =>
                {
                    if (_isBrowserOpenning) //Nếu browser đang mở thì đóng lại
                    {
                        BrowseFacebookContent.Children.RemoveAt(0);

                        openCloseButton.Content = openBrowseFacebookViewString;
                        //Hiển thị lại phần lấy token
                        MainGetTokenContent.Visibility = Visibility.Visible;
                    }
                    else //Nếu browser đang đóng thì mở browser mới
                    {
                        BrowseFacebookView browserView = new BrowseFacebookView();
                        BrowseFacebookContent.Children.Add(browserView);

                        openCloseButton.Content = closeBrowseFacebookViewString;
                        //Ẩn phần lấy token đi
                        MainGetTokenContent.Visibility = Visibility.Hidden;
                    }

                    _isBrowserOpenning = !_isBrowserOpenning;
                };
            }
        }

        private void UserInputStackPanelOnChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost host && host.Child is UWPControls.StackPanel inputStackPanel)
            {
                UWPControls.Button getTokenButton = new UWPControls.Button();
                getTokenButton.Content = "Lấy Facebook Token";
                getTokenButton.Width = 300;
                getTokenButton.Margin = new Windows.UI.Xaml.Thickness(10);
                getTokenButton.SetBinding(UWPControls.Button.CommandProperty, new Windows.UI.Xaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new Windows.UI.Xaml.PropertyPath("GetTokenCommand")
                });

                UWPControls.PasswordBox passwordBox = new UWPControls.PasswordBox();
                passwordBox.PlaceholderText = "Nhập password";
                passwordBox.Width = 300;
                passwordBox.Margin = new Windows.UI.Xaml.Thickness(10);
                passwordBox.PasswordChanged += (o, args) => { _viewModel.UserPassword = passwordBox.Password; };

                UWPControls.TextBox emailTextBox = new UWPControls.TextBox();
                emailTextBox.PlaceholderText = "Nhập email";
                emailTextBox.IsSpellCheckEnabled = false;
                emailTextBox.Width = 300;
                emailTextBox.Margin = new Windows.UI.Xaml.Thickness(10);
                //https://github.com/Microsoft/XamlIslandBlogPostSample/blob/master/WpfApp1/BindingPage.xaml.cs
                emailTextBox.SetBinding(UWPControls.TextBox.TextProperty, new Windows.UI.Xaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new Windows.UI.Xaml.PropertyPath("UserEmail"),
                    UpdateSourceTrigger = Windows.UI.Xaml.Data.UpdateSourceTrigger.PropertyChanged,
                    Mode = Windows.UI.Xaml.Data.BindingMode.TwoWay
                });

                
                inputStackPanel.Children.Add(emailTextBox);
                inputStackPanel.Children.Add(passwordBox);
                inputStackPanel.Children.Add(getTokenButton);

            }
        }

        private void ResultStackPanelOnChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost host && host.Child is UWPControls.StackPanel resultStackPanel)
            {
                //Dùng cho hiển thị Token

                UWPControls.TextBox fbTokenTextBox = new UWPControls.TextBox();
                fbTokenTextBox.Width = 300;
                fbTokenTextBox.Margin = new Windows.UI.Xaml.Thickness(10);
                fbTokenTextBox.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                fbTokenTextBox.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
                fbTokenTextBox.IsReadOnly = true;
                fbTokenTextBox.TextWrapping = TextWrapping.Wrap;
                fbTokenTextBox.SetBinding(UWPControls.TextBox.TextProperty, new Windows.UI.Xaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new Windows.UI.Xaml.PropertyPath("FBToken"),
                    Mode = Windows.UI.Xaml.Data.BindingMode.OneWay
                });

                UWPControls.Button copyTokenButton = new UWPControls.Button();
                copyTokenButton.Width = 300;
                copyTokenButton.Margin = new Windows.UI.Xaml.Thickness(10);
                copyTokenButton.Content = "Sao chép Token";
                copyTokenButton.Click += (o, args) => { Clipboard.SetText(_viewModel.FBToken ?? string.Empty); };

                UWPControls.StackPanel successStackPanel = new UWPControls.StackPanel();

                successStackPanel.Children.Add(fbTokenTextBox);
                successStackPanel.Children.Add(copyTokenButton);

                successStackPanel.SetBinding(UWPControls.StackPanel.VisibilityProperty, new Windows.UI.Xaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new Windows.UI.Xaml.PropertyPath("FBToken"),
                    Converter = new NullTovisibilityConverter()
                });

                //Dùng cho hiển thị lỗi
                UWPControls.StackPanel errorStackPanel = new UWPControls.StackPanel();

                UWPControls.TextBlock errorMsgTextBlock = new UWPControls.TextBlock();
                errorMsgTextBlock.Width = 300;
                errorMsgTextBlock.TextWrapping = TextWrapping.WrapWholeWords;
                errorMsgTextBlock.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                errorMsgTextBlock.SetBinding(UWPControls.TextBlock.TextProperty, new Windows.UI.Xaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new Windows.UI.Xaml.PropertyPath("ErrorMsg")
                });

                errorStackPanel.Children.Add(errorMsgTextBlock);

                //Thêm cả 2 panel success và error vào panel chính
                resultStackPanel.Children.Add(errorStackPanel);
                resultStackPanel.Children.Add(successStackPanel);
            }
        }


        private void IsGettingDataProgressRingOnChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost host && host.Child is UWPControls.ProgressRing isGettingDataProgressRing)
            {
                isGettingDataProgressRing.SetBinding(UWPControls.ProgressRing.IsActiveProperty, new Windows.UI.Xaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new Windows.UI.Xaml.PropertyPath("IsGettingData")
                });
            }
        }


    }
}
