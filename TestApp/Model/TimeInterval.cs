using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct TimeInterval
    {
        public TimeSpan start { get; }
        public TimeSpan end { get; }

        public TimeSpan length
        {
            get
            {
                return end - start;
            }
        }

        public TimeInterval(TimeSpan start, TimeSpan end)
        {
            var newStart = start - new TimeSpan(start.Days * 24, 0, 0);
            var newEnd = end - new TimeSpan(end.Days * 24, 0, 0);

            this.start = start;
            this.end = end < start ? end + new TimeSpan(24, 0, 0) : end;
        }

        /// <summary>
        /// Проверка попадает ли время в интервал
        /// </summary>
        /// <param name="end">Конец интервала</param>
        /// <returns>true если время попадает в интервал, иначе false</returns>
        public bool GetIsTimeInInterval(TimeSpan time)
        {
            var normTime = NormalizeInputTime(time);

            return normTime >= start && normTime < end;
        }

        public TimeSpan GetTimeTillEnd(TimeSpan time)
        {
            var normTime = NormalizeInputTime(time);

            return end - normTime;
        }

        private TimeSpan NormalizeInputTime(TimeSpan time)
        {
            var newTime = time - new TimeSpan(time.Days * 24, 0, 0);

            if (start.Hours > end.Hours && newTime < start)
            {
                newTime = time + new TimeSpan(24, 0, 0);
            }

            return newTime;
        }
    }
}
