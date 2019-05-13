﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using FBToken.Main.Converters;
using FBToken.Main.ViewModels;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using UWPXaml = Windows.UI.Xaml;
using UWPControls = Windows.UI.Xaml.Controls;

namespace FBToken.Main.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
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
                        MainGetTokenContent.Visibility = System.Windows.Visibility.Visible;
                    }
                    else //Nếu browser đang đóng thì mở browser mới
                    {
                        BrowseFacebookView browserView = new BrowseFacebookView();
                        BrowseFacebookContent.Children.Add(browserView);

                        openCloseButton.Content = closeBrowseFacebookViewString;
                        //Ẩn phần lấy token đi
                        MainGetTokenContent.Visibility = System.Windows.Visibility.Hidden;
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
                getTokenButton.HorizontalAlignment = UWPXaml.HorizontalAlignment.Center;
                getTokenButton.Margin = new UWPXaml.Thickness(10);
                getTokenButton.SetBinding(UWPControls.Button.CommandProperty, new UWPXaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new UWPXaml.PropertyPath("GetTokenCommand")
                });

                UWPControls.PasswordBox passwordBox = new UWPControls.PasswordBox();
                passwordBox.PlaceholderText = "Nhập password";
                passwordBox.Width = 300;
                passwordBox.Margin = new UWPXaml.Thickness(10);
                passwordBox.PasswordChanged += (o, args) => { _viewModel.UserPassword = passwordBox.Password; };

                //Thêm tooltip cho password TextBox
                UWPControls.ToolTip passwordToolTip = new UWPControls.ToolTip();
                passwordToolTip.Content =
                    "Để giảm thiểu khả năng checkpoint và tăng độ bảo mật, các bạn nên dùng Mật khẩu ứng dụng. Để lấy Mật khẩu ứng dụng: Cài đặt > Bảo mật và đăng nhập > Xác thực 2 yếu tố > Mật khẩu ứng dụng";
                UWPControls.ToolTipService.SetToolTip(passwordBox, passwordToolTip);

                UWPControls.TextBox emailTextBox = new UWPControls.TextBox();
                emailTextBox.PlaceholderText = "Nhập email";
                emailTextBox.IsSpellCheckEnabled = false;
                emailTextBox.Width = 300;
                emailTextBox.Margin = new UWPXaml.Thickness(10);
                //https://github.com/Microsoft/XamlIslandBlogPostSample/blob/master/WpfApp1/BindingPage.xaml.cs
                emailTextBox.SetBinding(UWPControls.TextBox.TextProperty, new UWPXaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new UWPXaml.PropertyPath("UserEmail"),
                    UpdateSourceTrigger = UWPXaml.Data.UpdateSourceTrigger.PropertyChanged,
                    Mode = UWPXaml.Data.BindingMode.TwoWay
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
                fbTokenTextBox.Margin = new UWPXaml.Thickness(10);
                fbTokenTextBox.BorderThickness = new UWPXaml.Thickness(0);
                fbTokenTextBox.Background = new UWPXaml.Media.SolidColorBrush(Windows.UI.Colors.Transparent);
                fbTokenTextBox.IsReadOnly = true;
                fbTokenTextBox.TextWrapping = UWPXaml.TextWrapping.Wrap;
                fbTokenTextBox.SetBinding(UWPControls.TextBox.TextProperty, new UWPXaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new UWPXaml.PropertyPath("FBToken"),
                    Mode = UWPXaml.Data.BindingMode.OneWay
                });

                UWPControls.Button copyTokenButton = new UWPControls.Button();
                copyTokenButton.Width = 150;
                copyTokenButton.Margin = new UWPXaml.Thickness(0, 0, 5, 0);
                copyTokenButton.Content = "Sao chép Token";
                copyTokenButton.Click += (o, args) => { Clipboard.SetText(_viewModel.FBToken ?? string.Empty); };

                UWPControls.Button saveTokenButton = new UWPControls.Button();
                saveTokenButton.Width = 120;
                saveTokenButton.Content = "Lưu Token";
                saveTokenButton.SetBinding(UWPControls.Button.CommandProperty, new UWPXaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new UWPXaml.PropertyPath("SaveTokenCommand")
                });

                UWPControls.StackPanel successCommandButtonsStackPanel = new UWPControls.StackPanel();
                successCommandButtonsStackPanel.Orientation = UWPControls.Orientation.Horizontal;
                successCommandButtonsStackPanel.Margin = new UWPXaml.Thickness(10);
                successCommandButtonsStackPanel.HorizontalAlignment = UWPXaml.HorizontalAlignment.Center;
                successCommandButtonsStackPanel.Children.Add(copyTokenButton);
                successCommandButtonsStackPanel.Children.Add(saveTokenButton);


                UWPControls.StackPanel successStackPanel = new UWPControls.StackPanel();

                successStackPanel.Children.Add(fbTokenTextBox);
                successStackPanel.Children.Add(successCommandButtonsStackPanel);

                successStackPanel.SetBinding(UWPControls.StackPanel.VisibilityProperty,
                    new UWPXaml.Data.Binding()
                    {
                        Source = _viewModel,
                        Path = new UWPXaml.PropertyPath("FBToken"),
                        Converter = new NullTovisibilityConverter()
                    });

                //Dùng cho hiển thị lỗi
                UWPControls.StackPanel errorStackPanel = new UWPControls.StackPanel();

                UWPControls.TextBlock errorMsgTextBlock = new UWPControls.TextBlock();
                errorMsgTextBlock.Width = 300;
                errorMsgTextBlock.TextWrapping = UWPXaml.TextWrapping.WrapWholeWords;
                errorMsgTextBlock.Foreground = new UWPXaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                errorMsgTextBlock.SetBinding(UWPControls.TextBlock.TextProperty, new UWPXaml.Data.Binding()
                {
                    Source = _viewModel,
                    Path = new UWPXaml.PropertyPath("ErrorMsg")
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
                isGettingDataProgressRing.SetBinding(UWPControls.ProgressRing.IsActiveProperty,
                    new UWPXaml.Data.Binding()
                    {
                        Source = _viewModel,
                        Path = new UWPXaml.PropertyPath("IsGettingData")
                    });
            }
        }
    }
}