using FBToken.Main.Core.Services.UIInteractServices;

namespace FBToken.Main.Core.Factories
{
    public class DialogServiceFactory
    {
        public static IDialogService GetDialogServiceInstance()
        {
            IDialogService dialogService = new DialogService();
            return dialogService;
        }
    }
}
