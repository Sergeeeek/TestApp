using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.ViewModel
{
    public class CalculationViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Расчёт";
            }
        }


    }
}
