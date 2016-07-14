using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Service;

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

        private ITimeCalculationService timeService;

        public SettingsViewModel(ITimeCalculationService timeService)
        {
            if (timeService == null)
                throw new ArgumentNullException("timeService");

            this.timeService = timeService;
        }
    }
}
