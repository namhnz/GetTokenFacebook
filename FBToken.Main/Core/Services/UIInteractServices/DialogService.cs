using System;
using System.Globalization;
using Microsoft.Win32;

namespace FBToken.Main.Core.Services.UIInteractServices
{
    public class DialogService : IDialogService
    {
        //https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/dd459587(v%3Dvs.95)
        //https://www.wpf-tutorial.com/dialogs/the-savefiledialog/
        public string ShowSaveFileDialog(string initialFolderPath, string defaultFileName)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|Data Files (*.dat)|*.dat|All Files (*.*)|*.*",
                FileName = defaultFileName ??
                           $"GT2-Token-{DateTime.Now.ToString("yyyy-MM-dd_HH-mm", CultureInfo.InvariantCulture)}.txt",
                InitialDirectory = initialFolderPath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };


            var result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
