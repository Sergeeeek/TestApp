using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace TestApp.ViewModel
{
    public class SettingsViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Настройки";
            }
        }


    }
}
