using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.Model;
using TestApp.Service;

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

        DateTime _shiftStartTime = DateTime.Now;
        public DateTime shiftStartTime
        {
            get
            {
                return _shiftStartTime;
            }
            set
            {
                if(_shiftStartTime == value)
                {
                    return;
                }

                _shiftStartTime = value;
                RaisePropertyChanged("shiftStartTime");
            }
        }

        DateTime _shiftEndTime = DateTime.Now;
        public DateTime shiftEndTime
        {
            get
            {
                return _shiftEndTime;
            }
            set
            {
                if (_shiftEndTime == value)
                {
                    return;
                }

                _shiftEndTime = value;
                RaisePropertyChanged("shiftEndTime");
            }
        }

        DateTime _morningHours = new DateTime();
        public DateTime morningHours
        {
            get
            {
                return _morningHours;
            }
            set
            {
                if(_morningHours == value)
                {
                    return;
                }

                _morningHours = value;
                RaisePropertyChanged("morningHours");
            }
        }

        DateTime _dayHours = new DateTime();
        public DateTime dayHours
        {
            get
            {
                return _dayHours;
            }
            set
            {
                if (_dayHours == value)
                {
                    return;
                }

                _dayHours = value;
                RaisePropertyChanged("dayHours");
            }
        }

        DateTime _eveningHours = new DateTime();
        public DateTime eveningHours
        {
            get
            {
                return _eveningHours;
            }
            set
            {
                if (_eveningHours == value)
                {
                    return;
                }

                _eveningHours = value;
                RaisePropertyChanged("eveningHours");
            }
        }

        private ICommand _calculateCommand;
        public ICommand calculateCommand
        {
            get
            {
                return _calculateCommand;
            }
            set
            {
                if(_calculateCommand == value)
                {
                    return;
                }

                _calculateCommand = value;
                RaisePropertyChanged("calculateCommand");
            }
        }

        private ITimeCalculationService timeService;

        public CalculationViewModel(ITimeCalculationService timeService)
        {
            if (timeService == null)
                throw new ArgumentNullException("timeService");

            this.timeService = timeService;

            calculateCommand = new Command(() =>
            {
                var shiftInterval =
                    new TimeInterval(
                        new TimeOfDay((uint)shiftStartTime.Hour, (uint)shiftStartTime.Minute, (uint)shiftStartTime.Second),
                        new TimeOfDay((uint)shiftEndTime.Hour, (uint)shiftEndTime.Minute, (uint)shiftEndTime.Second));

                timeService.shiftInterval = shiftInterval;

                var result = timeService.Calculate();

                morningHours = new DateTime(1, 1, 1, result.morningHours.Hours, result.morningHours.Minutes, result.morningHours.Seconds);
                dayHours = new DateTime(1, 1, 1, result.dayHours.Hours, result.dayHours.Minutes, result.dayHours.Seconds);
                eveningHours = new DateTime(1, 1, 1, result.eveningHours.Hours, result.eveningHours.Minutes, result.eveningHours.Seconds);
            });
        }
    }
}
