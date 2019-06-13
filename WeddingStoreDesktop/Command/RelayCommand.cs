using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeddingStoreDesktop.Command
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExcute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExcute, Action<T> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _canExcute = canExcute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExcute == null ? true : _canExcute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
