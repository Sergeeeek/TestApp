using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestApp.Service;
using System.Collections.Specialized;
using TestApp.Model;

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

        #region Binding properties

        private ObservableCollection<DateTime> _breakStarts = new ObservableCollection<DateTime>()
        {
            new DateTime(),
            new DateTime(),
            new DateTime()
        };
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Массив начал перерывов.
        /// 
        /// </summary>
        public ObservableCollection<DateTime> breakStarts
        {
            get
            {
                return _breakStarts;
            }
            set
            {
                if(_breakStarts == value)
                {
                    return;
                }

                _breakStarts = value;
                RaisePropertyChanged("breakStarts");
            }
        }

        private ObservableCollection<DateTime> _breakEnds = new ObservableCollection<DateTime>()
        {
            new DateTime(),
            new DateTime(),
            new DateTime()
        };
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Массив концов перерывов.
        /// 
        /// </summary>
        public ObservableCollection<DateTime> breakEnds
        {
            get
            {
                return _breakEnds;
            }
            set
            {
                if (_breakEnds == value)
                {
                    return;
                }

                _breakEnds = value;
                RaisePropertyChanged("breakEnds");
            }
        }

        private ObservableCollection<bool> _breakEnabled = new ObservableCollection<bool>()
        {
            false,
            false,
            false
        };
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Массив состояний перерывов (включены или выключены)
        /// 
        /// </summary>
        public ObservableCollection<bool> breakEnabled
        {
            get
            {
                return _breakEnabled;
            }
            set
            {
                if(_breakEnabled == value)
                {
                    return;
                }

                _breakEnabled = value;
                RaisePropertyChanged("breakEnabled");
            }
        }

        #endregion

        private ITimeCalculationService timeService;

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// </summary>
        /// <param name="timeService">Сервис расчётов.</param>
        public SettingsViewModel(ITimeCalculationService timeService)
        {
            if (timeService == null)
                throw new ArgumentNullException("timeService");

            this.timeService = timeService;

            // При любом изменении данных обновляем сервис
            _breakStarts.CollectionChanged += processCollectionChangedEvent;
            _breakEnds.CollectionChanged += processCollectionChangedEvent;
            _breakEnabled.CollectionChanged += processCollectionChangedEvent;
        }

        private void processCollectionChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            var resultList = new List<TimeInterval>();

            for(int i = 0; i < breakEnabled.Count; i++)
            {
                if(breakEnabled[i])
                {
                    var start = breakStarts[i];
                    var end = breakEnds[i];
                    resultList.Add(
                        new TimeInterval(
                            new TimeOfDay((uint)start.Hour, (uint)start.Minute, (uint)start.Second),
                            new TimeOfDay((uint)end.Hour,   (uint)end.Minute,   (uint)end.Second)));
                }
            }

            timeService.workBreaks = resultList;
        }
    }
}
