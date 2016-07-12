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

        public TimeInterval(int startHours, int endHours)
            : this(new TimeSpan(startHours, 0, 0), new TimeSpan(endHours, 0, 0))
        {

        }

        /// <summary>
        /// Разность двух интервалов
        /// </summary>
        /// <param name="l">Уменьшаемое</param>
        /// <param name="r">Вычитаемое</param>
        /// <returns>Возвращает интервал(-ы) который включает в себя интервал l, но не включают интервал r</returns>
        public static List<TimeInterval> operator -(TimeInterval l, TimeInterval r)
        {
            bool isStartIn = l.GetIsTimeInInterval(r.start);
            bool isEndIn = l.GetIsTimeInInterval(r.end);

            if (isStartIn && !isEndIn)
            {
                return new List<TimeInterval>() { new TimeInterval(l.start, r.start) };
            }
            if (!isStartIn && isEndIn)
            {
                return new List<TimeInterval>() { new TimeInterval(r.end, l.end) };
            }
            if (isStartIn && isEndIn)
            {
                return new List<TimeInterval>() { new TimeInterval(l.start, r.start), new TimeInterval(r.end, l.end) };
            }

            return new List<TimeInterval>() { l };
        }

        /// <summary>
        /// Пересечение двух интервалов
        /// </summary>
        /// <param name="l">Первый интервал</param>
        /// <param name="r">Второй интервал</param>
        /// <returns>Возвращает интервал который попадает в первый и второй интервалы одновременно</returns>
        public static TimeInterval operator *(TimeInterval l, TimeInterval r)
        {
            bool isStartIn = l.GetIsTimeInInterval(r.start);
            bool isEndIn = l.GetIsTimeInInterval(r.end);

            if (isStartIn && !isEndIn)
            {
                return new TimeInterval(r.start, l.end);
            }
            if (!isStartIn && isEndIn)
            {
                return new TimeInterval(l.start, r.end);
            }
            if (!isStartIn && !isEndIn)
            {
                if (l.start > r.start && l.end < r.end)
                {
                    return r * l;
                }
                else
                {
                    return new TimeInterval();
                }
            }
            if (isStartIn && isEndIn)
            {
                return new TimeInterval(r.start, r.end);
            }

            return new TimeInterval();
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
