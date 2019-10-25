using Microsoft.Win32;

namespace FBToken.Main.Core.Services.UIInteractServices
{
    public class DialogService : IDialogService
    {
        //https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/dd459587(v%3Dvs.95)
        //https://www.wpf-tutorial.com/dialogs/the-savefiledialog/
        public string ShowSaveFileDialog(string initialFolderPath = null, string defaultFileName = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            //Cài đặt SaveFileDialog
            dialog.Filter = "Text Files (*.txt)|*.txt|Data Files (*.dat)|*.dat|All Files (*.*)|*.*";
            dialog.FileName = defaultFileName; //Tên file mặc định muốn lưu

            if (initialFolderPath!=null)
            {
                dialog.InitialDirectory = initialFolderPath;
            }
            var result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
