using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct TimeInterval
    {
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Время начала интервала.
        /// 
        /// </summary>
        public TimeOfDay start { get; }
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Время конца интервала.
        /// 
        /// </summary>
        public TimeOfDay end { get; }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// Длина этого интервала.
        /// </summary>
        public TimeSpan length
        {
            get
            {
                return end - start;
            }
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// "Универсальный" интервал - интервал от 0 до 24 часов, охватывает весь день.
        ///
        /// </summary>
        public static TimeInterval U = new TimeInterval(0, 24);
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Пустой интервал.
        /// 
        /// </summary>
        public static TimeInterval O = new TimeInterval(0, 0);

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Создаёт интервал.
        /// 
        /// </summary>
        /// <param name="start">Начало интервала</param>
        /// <param name="end">Конец интервала</param>
        public TimeInterval(TimeOfDay start, TimeOfDay end)
        {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Создаёт интервал.
        /// 
        /// </summary>
        /// <param name="startHours">Начало интервала в часах</param>
        /// <param name="endHours">Конец интервала в часах</param>
        public TimeInterval(uint startHours, uint endHours)
            : this(new TimeOfDay(startHours), new TimeOfDay(endHours))
        {

        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Объеденение двух интервалов.
        ///
        /// </summary>
        /// <param name="l">Первое слагаемое</param>
        /// <param name="r">Второе слагаемое</param>
        /// <returns>Возвращает один или несколько интервалов в которые попадает и первый и второй интервалы.</returns>
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
                if ((bothWrapAround 
                    && ((r.start < l.end && r.end > l.start) 
                    || (r.start > l.start && r.end < l.end)) 
                    || (bothDontWrapAround && l.start < r.start && l.end > r.end)))
                {
                    return new List<TimeInterval>() { l };
                }

                if (lWrapsAround 
                    && !rWrapsAround 
                    && ((r.start < l.start && r.end < l.start) 
                    || (r.start > l.end && r.end > l.end)))
                {
                    return new List<TimeInterval>() { l };
                }

                return new List<TimeInterval>() { U };
            }
            if (!isStartIn && !isEndIn)
            {
                if(r.GetIsTimeInInterval(l.start) && r.GetIsTimeInInterval(l.end))
                {
                    return r + l;
                }

                if (bothWrapAround || (bothDontWrapAround
                    && r.start < l.start
                    && r.end > l.end))
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
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// Разность двух интервалов.
        /// </summary>
        /// <param name="l">Уменьшаемое</param>
        /// <param name="r">Вычитаемое</param>
        /// <returns>Возвращает один или несколько интервалов которые включают в себя часть интервала l, не включающую интервал r</returns>
        /// <example>
        /// Пример вычитания.
        /// 
        /// <code>
        /// var result = new TimeInterval(10, 15) - new TimeInterval(9, 12);
        /// result[0] == new TimeInterval(12, 15) // true
        /// </code>
        /// 
        /// </example>
        public static List<TimeInterval> operator -(TimeInterval l, TimeInterval r)
        {
            return Invert(Invert(l) + r);
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Пересечение двух интервалов.
        /// 
        /// </summary>
        /// <param name="l">Первый интервал</param>
        /// <param name="r">Второй интервал</param>
        /// <returns>Возвращает один или несколько интервалов которые попадают в первый и второй интервалы одновременно.</returns>
        /// <example>
        /// Пример пересечения.
        /// 
        /// <code>
        /// var result = new TimeInterval(10, 15) * new TimeInterval(12, 13);
        /// result[0] == new TimeInterval(12, 13) // true
        /// </code>
        /// 
        /// </example>
        public static List<TimeInterval> operator *(TimeInterval l, TimeInterval r)
        {
            return l - Invert(r);
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Равняются ли интервалы друг другу.
        /// Интервалы с <see cref="length"/> = 0 считаются одинаковыми.
        /// 
        /// </summary>
        /// <returns>true если <paramref name="l"/> равняется <paramref name="r"/>.</returns>
        public static bool operator ==(TimeInterval l, TimeInterval r)
        {
            return (l.start == r.start && l.end == r.end) || (l.length == new TimeSpan(0, 0, 0) && r.length == new TimeSpan(0, 0, 0));
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Различаются ли интервалы.
        /// <returns>true если <paramref name="l"/> не равняется <paramref name="r"/></returns>
        public static bool operator !=(TimeInterval l, TimeInterval r)
        {
            return !(l == r);
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Переопределение стандартного метода <see cref="object.Equals(object)"/>. 
        /// 
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true если this и <paramref name="obj"/> равны.</returns>
        public override bool Equals(object obj)
        {
            if(obj != null && obj is TimeInterval)
            {
                return this == (TimeInterval)obj;
            }

            return false;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Переопределение стандартного метода <see cref="object.GetHashCode()"/> 
        /// 
        /// </summary>
        /// <returns>Возвращает хэш значение для этой структуры.</returns>
        public override int GetHashCode()
        {
            int hash = 13;

            hash = (hash * 7) + start.GetHashCode();
            hash = (hash * 7) + end.GetHashCode();

            return hash;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// <para>
        /// Инвертирует один интервал.
        /// </para>
        /// 
        /// </summary>
        /// <param name="that"></param>
        /// <returns>
        /// <para>
        /// Если <paramref name="that"/> == <see cref="U"/> (универсальному интервалу), то возвращается <see cref="O"/> (пустой интервал).
        /// </para>
        /// <para>
        /// Если <paramref name="that"/> == <see cref="O"/>, то возвращается <see cref="U"/>.
        /// </para>
        /// <para>
        /// Иначе возвращает интервал с поменяными местами <see cref="start"/> и <see cref="end"/>.
        /// </para>
        /// </returns>
        /// <example>
        /// 
        /// <code>
        /// TimeInterval.Invert(new TimeInterval(10, 12)) == new TimeInterval(12, 10) // true
        /// </code>
        /// 
        /// </example>
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

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Инвертирует лист интервалов, так чтобы <see cref="U"/> - <paramref name="intervals"/> == <see cref="Invert(List{TimeInterval})"/>
        /// 
        /// </summary>
        /// <param name="intervals">Интервалы которые нужно инвертировать.</param>
        /// <returns>Возвращает такой список интервалов который не пересекается с <paramref name="intervals"/></returns>
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
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Проверка попадает ли время в интервал.
        /// 
        /// </summary>
        /// <param name="end">Конец интервала</param>
        /// <returns>true если время попадает в интервал</returns>
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
