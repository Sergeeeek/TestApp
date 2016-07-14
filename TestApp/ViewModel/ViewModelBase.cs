using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Событие изменения свойства. Имплементация <see cref="INotifyPropertyChanged.PropertyChanged"/>.
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Вызывает событие <see cref="PropertyChanged"/> для параметра с именем <paramref name="propName"/>.
        /// 
        /// </summary>
        /// <param name="propName">Имя изменившегося параметра.</param>
        protected void RaisePropertyChanged(string propName)
        {
            var handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
