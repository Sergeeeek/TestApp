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

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Создаёт новую команду
        /// </summary>
        /// <param name="execute">Функция которая будет выполнятся при вызове команды</param>
        /// <param name="canExecute">Функция которая </param>
        public Command(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            actionExecute = execute;
            actionCanExecute = canExecute;
        }

        /// <summary>
        /// Может сработать только при вызове <see cref="Execute(object)"/>
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Вызывает <see cref="Func&lt;bool&gt;"/> переданный в конструкторе <see cref="Command(Action, Func&lt;bool&gt;)"/>
        /// 
        /// </summary>
        /// <param name="parameter">Параметр для <see cref="Func&lt;bool&gt;"/> Игнорируется</param>
        /// <returns>Может ли команда исполняться</returns>
        public bool CanExecute(object parameter)
        {
            return actionCanExecute == null ? true : actionCanExecute();
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Выполнить команду.
        /// Вызывает <see cref="Action"/> переданный этой команде в конструкторе <see cref="Command(Action, Func&lt;bool&gt;)"/>
        ///
        /// </summary>
        /// <param name="parameter">Параметр для <see cref="Action"/>. Игнорируется.</param>
        public void Execute(object parameter)
        {
            bool canExecute = true;

            if(actionCanExecute != null)
            {
                canExecute = actionCanExecute();
                if(CanExecuteChanged != null)
                {
                    var handler = CanExecuteChanged;
                    handler(this, new EventArgs());
                }
            }
            if(canExecute)
            {
                actionExecute();
            }
        }
    }
}
