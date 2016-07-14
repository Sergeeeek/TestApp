using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp.ViewModel
{
    public class Command : ICommand
    {
        private Action actionExecute;
        private Func<bool> actionCanExecute;

        public Command(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            actionExecute = execute;
            actionCanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return actionCanExecute == null ? true : actionCanExecute();
        }

        public void Execute(object parameter)
        {
            actionExecute();
        }
    }
}
