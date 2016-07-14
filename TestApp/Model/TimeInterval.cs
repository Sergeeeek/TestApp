using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct TimeInterval
    {
        public TimeOfDay start { get; }
        public TimeOfDay end { get; }

        public TimeSpan length
        {
            get
            {
                return end - start;
            }
        }

        public static TimeInterval U = new TimeInterval(0, 24);
        public static TimeInterval O = new TimeInterval(0, 0);

        public TimeInterval(TimeOfDay start, TimeOfDay end)
        {
            this.start = start;
            this.end = end;
        }

        public TimeInterval(uint startHours, uint endHours)
            : this(new TimeOfDay(startHours), new TimeOfDay(endHours))
        {

        }

        public static List<TimeInterval> operator +(TimeInterval l, TimeInterval r)
        {
            if (l == r)
            {
                return new List<TimeInterval>() { l };
            }
            if (l == U || r == U)
            {
                return new List<TimeInterval>() { U };
            }
            if (l == O)
            {
                return new List<TimeInterval>() { r };
            }
            if (r == O)
            {
                return new List<TimeInterval>() { l };
            }

            bool isStartIn = l.GetIsTimeInInterval(r.start);
            bool isEndIn = l.GetIsTimeInInterval(r.end);

            bool lWrapsAround = l.start > l.end;
            bool rWrapsAround = r.start > r.end;

            bool bothWrapAround = lWrapsAround && rWrapsAround;
            bool bothDontWrapAround = !lWrapsAround && !rWrapsAround;

            if (isStartIn && !isEndIn)
            {
                return new List<TimeInterval>() { new TimeInterval(l.start, r.end) };
            }
            if (!isStartIn && isEndIn)
            {
                return new List<TimeInterval>() { new TimeInterval(r.start, l.end) };
            }
            if (isStartIn && isEndIn)
            {
                if ((bothWrapAround && (r.start < l.end && r.end > l.start) || (r.start > l.start && r.end < l.end)) || (bothDontWrapAround && l.start < r.start && l.end > r.end))
                {
                    return new List<TimeInterval>() { l };
                }

                if (lWrapsAround && !rWrapsAround && ((r.start < l.start && r.end < l.start) || (r.start > l.end && r.end > r.end)))
                {
                    return new List<TimeInterval>() { l };
                }

                return new List<TimeInterval>() { U };
            }
            if (!isStartIn && !isEndIn)
            {
                if (bothWrapAround || bothDontWrapAround)
                {
                    return new List<TimeInterval>() { r };
                }

                if ((lWrapsAround || rWrapsAround) && l.start == r.end && r.start == l.end)
                {
                    return new List<TimeInterval>() { U };
                }

                return new List<TimeInterval>() { l, r };
            }

            return new List<TimeInterval>() { O };
        }

        /// <summary>
        /// Разность двух интервалов
        /// </summary>
        /// <param name="l">Уменьшаемое</param>
        /// <param name="r">Вычитаемое</param>
        /// <returns>Возвращает интервал(-ы) который включает в себя интервал l, но не включают интервал r</returns>
        public static List<TimeInterval> operator -(TimeInterval l, TimeInterval r)
        {
            return Invert(Invert(l) + r);
        }

        /// <summary>
        /// Пересечение двух интервалов
        /// </summary>
        /// <param name="l">Первый интервал</param>
        /// <param name="r">Второй интервал</param>
        /// <returns>Возвращает интервал который попадает в первый и второй интервалы одновременно</returns>
        public static List<TimeInterval> operator *(TimeInterval l, TimeInterval r)
        {
            return l - Invert(r);
        }

        public static bool operator ==(TimeInterval l, TimeInterval r)
        {
            return (l.start == r.start && l.end == r.end) || (l.length == new TimeSpan(0, 0, 0) && r.length == new TimeSpan(0, 0, 0));
        }

        public static bool operator !=(TimeInterval l, TimeInterval r)
        {
            return !(l == r);
        }

        public static TimeInterval Invert(TimeInterval that)
        {
            if (that == O)
            {
                return U;
            }
            if (that == U)
            {
                return O;
            }

            return new TimeInterval(that.end, that.start);
        }

        public static List<TimeInterval> Invert(List<TimeInterval> intervals)
        {
            if (intervals.Count == 1)
            {
                return new List<TimeInterval>() { Invert(intervals[0]) };
            }

            var sortedIntervals = intervals.OrderBy(x => x.start).ToList();

            for (int i = 0; i < sortedIntervals.Count - 1; i++)
            {
                var tempInt = sortedIntervals[0];
                var tempInt2 = sortedIntervals[1];

                if (tempInt.start > tempInt.end && tempInt2.start > tempInt2.end)
                {
                    sortedIntervals[i] = new TimeInterval(tempInt2.start, tempInt.end);
                    sortedIntervals[i + 1] = new TimeInterval(tempInt.start, tempInt2.end);
                }
                else
                {
                    sortedIntervals[i] = new TimeInterval(tempInt.end, tempInt2.start);
                    sortedIntervals[i + 1] = new TimeInterval(tempInt2.end, tempInt.start);
                }
            }

            return sortedIntervals.OrderBy(x => x.start).ToList();
        }

        /// <summary>
        /// Проверка попадает ли время в интервал
        /// </summary>
        /// <param name="end">Конец интервала</param>
        /// <returns>true если время попадает в интервал, иначе false</returns>
        public bool GetIsTimeInInterval(TimeOfDay time)
        {
            if (end < start && (time > start || time < end))
            {
                return true;
            }

            return (time > start && time < end);
        }
    }
}
