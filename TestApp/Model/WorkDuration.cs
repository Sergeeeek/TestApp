using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct WorkDuration
    {
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// Кол-во утренних часов
        /// </summary>
        public TimeSpan morningHours { get; set; }
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// Кол-во дневных часов
        /// </summary>
        public TimeSpan dayHours { get; set; }
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// Кол-во вечерних часов
        /// Количество вечерних часов
        /// </summary>
        public TimeSpan eveningHours { get; set; }
    }
}
