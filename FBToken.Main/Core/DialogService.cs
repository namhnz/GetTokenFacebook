using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FBToken.Main.Core
{
    public class DialogService : IDialogService
    {
        //https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/dd459587(v%3Dvs.95)
        //https://www.wpf-tutorial.com/dialogs/the-savefiledialog/
        public string ShowSaveFileDialog(string initialFolderPath = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text Files (*.txt)|*.txt|Data Files (*.dat)|*.dat|All Files (*.*)|*.*";
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
