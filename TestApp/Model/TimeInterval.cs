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

        public TimeInterval(TimeOfDay start, TimeOfDay end)
        {
            this.start = start;
            this.end = end;
        }

        public TimeInterval(uint startHours, uint endHours)
            : this(new TimeOfDay(startHours), new TimeOfDay(endHours))
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
                if(r.start < l.end && r.end > l.start)
                {
                    return new List<TimeInterval>() { new TimeInterval(r.end, r.start) };
                }

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
        public static List<TimeInterval> operator *(TimeInterval l, TimeInterval r)
        {
            return l - r.Invert();

            //bool isStartIn = l.GetIsTimeInInterval(r.start);
            //bool isEndIn = l.GetIsTimeInInterval(r.end);

            //if (isStartIn && !isEndIn)
            //{
            //    return new TimeInterval(r.start, l.end);
            //}
            //if (!isStartIn && isEndIn)
            //{
            //    return new TimeInterval(l.start, r.end);
            //}
            //if (!isStartIn && !isEndIn)
            //{
            //    if (l.start > r.start && l.end < r.end)
            //    {
            //        return r * l;
            //    }
            //    else
            //    {
            //        return new TimeInterval();
            //    }
            //}
            //if (isStartIn && isEndIn)
            //{
            //    if(r.start < l.start || r.end > l.end)
            //    {
            //        return new TimeInterval()
            //    }

            //    return new TimeInterval(r.start, r.end);
            //}

            //return new TimeInterval();
        }

        public TimeInterval Invert()
        {
            return new TimeInterval(this.end, this.start);
        }

        /// <summary>
        /// Проверка попадает ли время в интервал
        /// </summary>
        /// <param name="end">Конец интервала</param>
        /// <returns>true если время попадает в интервал, иначе false</returns>
        public bool GetIsTimeInInterval(TimeOfDay time)
        {
            if (end < start && (time >= start || time <= end))
            {
                return true;
            }

            return (time >= start && time <= end);
        }

        public TimeSpan GetTimeTillEnd(TimeOfDay time)
        {
            return end - time;
        }
    }
}
