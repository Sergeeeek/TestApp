using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Service
{
    public interface ITimeCalculationService
    {
        /// <summary>
        /// Время начала смены
        /// </summary>
        TimeInterval shiftInterval { get; set; }

        /// <summary>
        /// Список перерывов
        /// </summary>
        List<TimeInterval> workBreaks { get; set; }

        /// <summary>
        /// Подсчитывает количество утренних, дневных и ночных часов работы из заданных данных
        /// </summary>
        /// <example>
        /// //ITimeCalculationService service;
        /// service.shiftStart = new TimeSpan()
        /// </example>
        /// <returns>Количество часов</returns>
        WorkDuration Calculate();
    }
}
