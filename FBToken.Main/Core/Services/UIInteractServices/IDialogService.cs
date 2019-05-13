namespace FBToken.Main.Core.Services.UIInteractServices
{
    public interface IDialogService
    {
        //Mở Dialog từ ViewModel trong MVVM: https://stackoverflow.com/questions/1619505/wpf-openfiledialog-with-the-mvvm-pattern?noredirect=1&lq=1
        string ShowSaveFileDialog(string initialFolderPath = null, string defaultFileName = null);
    }
}