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
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Время начала смены
        /// 
        /// </summary>
        TimeInterval shiftInterval { get; set; }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Список перерывов
        /// 
        /// </summary>
        List<TimeInterval> workBreaks { get; set; }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Подсчитывает количество утренних, дневных и ночных часов работы из заданных данных.
        /// 
        /// </summary>
        /// <example>
        /// Как использовать эту функцию.
        /// <code>
        /// 
        /// // Где-то ранее
        /// // ITimeCalculationService service = ...;
        /// // service.shiftInterval = new TimeInterval(..., ...);
        /// // service.workBreaks = ...;
        /// 
        /// WorkDuration result = service.Calculate()
        /// 
        /// </code>
        /// </example>
        /// <returns>Количество часов</returns>
        WorkDuration Calculate();
    }
}
