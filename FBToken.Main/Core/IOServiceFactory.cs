using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBToken.Main.Core
{
    public class IOServiceFactory
    {
        public static IDialogService GetDialogServiceInstance()
        {
            IDialogService dialogService = new DialogService();
            return dialogService;
        }
    }
}
