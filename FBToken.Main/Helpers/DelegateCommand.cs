using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FBToken.Main.Helpers
{
    public class DelegateCommand : ICommand
    {
        //https://gist.github.com/bobbychopra/2895470

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public DelegateCommand(Action<object> execute) : this(execute, null)
        {
            
        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object param)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(param);
        }

        public void Execute(object param)
        {
            _execute(param);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
