using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct TimeOfDay : IComparable
    {
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Количество секунд.
        /// 
        /// </summary>
        public uint seconds { get; }
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Количество минут.
        /// 
        /// </summary>
        public uint minutes { get; }
        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Количество часов.
        /// 
        /// </summary>
        public uint hours { get; }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Полное количество секунд с начала дня. 
        /// 
        /// </summary>
        public uint totalSeconds
        {
            get
            {
                return seconds + minutes * 60 + hours * 3600;
            }
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Создаёт новое время дня. Если Итоговое количество часов превысит 24, то отсчёт начнётся с нуля.
        /// 
        /// </summary>
        /// <param name="h">Часы</param>
        /// <param name="m">Минуты</param>
        /// <param name="s">Секунды</param>
        public TimeOfDay(uint h, uint m, uint s)
        {
            seconds = s % 60;
            uint additionalMins = s / 60;
            minutes = (additionalMins + m) % 60;
            uint additionalHours = (additionalMins + m) / 60;
            var totalHours = (h + additionalHours);
            // Если итоговое кол-во часов == 24, то оставляем его так,
            // чтобы можно было создавать интервалы от 0 до 24 часов
            hours = totalHours / 24 == 1 ? totalHours : totalHours % 24;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Создаёт новое время дня. Если Итоговое количество часов превысит 24, то отсчёт начнётся с нуля.
        /// 
        /// </summary>
        /// <param name="h">Часы</param>
        /// <param name="m">Минуты</param>
        public TimeOfDay(uint h, uint m)
            : this(h, m, 0)
        {

        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Создаёт новое время дня. Если Итоговое количество часов превысит 24, то отсчёт начнётся с нуля.
        /// 
        /// </summary>
        /// <param name="h">Часы.</param>
        public TimeOfDay(uint h)
            : this(h, 0, 0)
        {

        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Сложение времён.
        /// 
        /// </summary>
        /// <param name="l">Первое слагаемое</param>
        /// <param name="r">Второе слагаемое</param>
        /// <returns>Сумму первого и второго времени дня.</returns>
        public static TimeOfDay operator +(TimeOfDay l, TimeOfDay r)
        {
            return new TimeOfDay(l.hours + r.hours, l.minutes + r.minutes, l.seconds + r.seconds);
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Разница времён. Учитывает вычитание большего времени из меньшего
        /// и считает это как вычитание из времени следующего дня.
        /// 
        /// </summary>
        /// <param name="l">Уменьшаемое.</param>
        /// <param name="r">Вычитаемое.</param>
        /// <returns>Разницу во времени между <paramref name="l"/> и <paramref name="r"/>.</returns>
        public static TimeSpan operator -(TimeOfDay l, TimeOfDay r)
        {
            if (l < r)
                return new TimeSpan(1, (int)l.hours, (int)l.minutes, (int)l.seconds) - new TimeSpan((int)r.hours, (int)r.minutes, (int)r.seconds);

            return new TimeSpan((int)(l.hours - r.hours), (int)(l.minutes - r.minutes), (int)(l.seconds - r.seconds));
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Меньше ли <paramref name="l"/> чем <paramref name="r"/>.
        /// 
        /// </summary>
        public static bool operator <(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds < r.totalSeconds;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Больше ли <paramref name="l"/> чем <paramref name="r"/>.
        /// 
        /// </summary>
        public static bool operator >(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds > r.totalSeconds;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// <paramref name="l"/> меньше или равно <paramref name="r"/>
        /// 
        /// </summary>
        public static bool operator <=(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds <= r.totalSeconds;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// <paramref name="l"/> больше или равно <paramref name="r"/>.
        /// 
        /// </summary>
        public static bool operator >=(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds >= r.totalSeconds;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Равняется ли <paramref name="l"/> и <paramref name="r"/>
        /// 
        /// </summary>
        public static bool operator ==(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds == r.totalSeconds;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// true если <paramref name="l"/> не равняется <paramref name="r"/>.
        /// 
        /// </summary>
        public static bool operator !=(TimeOfDay l, TimeOfDay r)
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
        /// <param name="obj">Объект с которым нужно сравнивать this</param>
        /// <returns>Равняются ли эти объекты</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is TimeOfDay)
            {
                return this == (TimeOfDay)obj;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Переопределение стандартного метода <see cref="object.GetHashCode()"/>.
        /// 
        /// </summary>
        /// <returns>Возвращает <see cref="totalSeconds"/>, т.к. это своеобразный хэш и всегда уникален для разных объектов <see cref="TimeOfDay"/>.</returns>
        public override int GetHashCode()
        {
            return (int)totalSeconds;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// 
        /// Имплементация <see cref="IComparable.CompareTo(object)"/>. Нужно для сортировки.
        /// 
        /// </summary>
        /// <param name="obj">Другой объект <see cref="TimeOfDay"/> для сравнения.</param>
        /// <returns>
        /// Возвращает:
        /// -1 если this нужно поместить перед <paramref name="obj"/>;
        /// 0 если this и obj не нужно менять местами;
        /// 1 если this нужно поместить после <paramref name="obj"/>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj != null && obj is TimeOfDay)
            {
                var time = (TimeOfDay)obj;

                if (this < time)
                {
                    return -1;
                }

                if (this == time)
                {
                    return 0;
                }

                if (this > time)
                {
                    return 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// <para>
        /// Автор: Сергей Позняк
        /// </para>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", hours, minutes, seconds);
        }
    }
}
