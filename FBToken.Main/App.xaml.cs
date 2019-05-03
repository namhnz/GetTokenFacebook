using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

//Nguồn: 
//https://www.facebook.com/pagevynghia/posts/2452869638282083
//https://www.siblog.net/2019/02/api-get-token-facebook-full-quyen-khong-checkpoint.html?fbclid=IwAR1eZVCBaKBWp-T3WmODC9Wh7ccbGGf6uZpcnWG1cMP46HMD5f2XKcm8oug

namespace FBToken.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                LogUnhandledException((Exception)args.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
            Dispatcher.UnhandledException += (sender, args) =>
                LogUnhandledException(args.Exception, "Dispatcher.UnhandledException");
            TaskScheduler.UnobservedTaskException += (sender, args) =>
                LogUnhandledException(args.Exception, "TaskScheduler.UnobservedTaskException");
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            string message = $"Unhandled exception ({source})";
            try
            {
                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in App Startup's LogUnhandledException: " + ex);
            }
            finally
            {
                Debug.WriteLine(message + ": " + exception);
            }
        }
    }
}
