namespace FBToken.Main.Core
{
    public interface IDialogService
    {
        //https://stackoverflow.com/questions/1619505/wpf-openfiledialog-with-the-mvvm-pattern?noredirect=1&lq=1
        string ShowSaveFileDialog(string initialFolderPath = null);
    }
}