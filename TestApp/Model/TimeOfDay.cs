using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct TimeOfDay
    {
        public uint seconds { get; }
        public uint minutes { get; }
        public uint hours { get; }

        public uint totalSeconds
        {
            get
            {
                return seconds + minutes * 60 + hours * 3600;
            }
        }

        public uint totalMinutes
        {
            get
            {
                return minutes + hours * 60;
            }
        }

        public TimeOfDay(uint h, uint m, uint s)
        {
            seconds = s % 60;
            uint additionalMins = s / 60;
            minutes = (additionalMins + m) % 60;
            uint additionalHours = (additionalMins + m) / 60;
            hours = (h + additionalHours) % 24;
        }

        public TimeOfDay(uint h, uint m)
            : this(h, m, 0)
        {

        }

        public TimeOfDay(uint h)
            : this(h, 0, 0)
        {

        }

        public static TimeOfDay operator +(TimeOfDay l, TimeOfDay r)
        {
            return new TimeOfDay(l.hours + r.hours, l.minutes + r.minutes, l.seconds + r.seconds);
        }

        public static TimeSpan operator -(TimeOfDay l, TimeOfDay r)
        {
            if (l < r)
                return new TimeSpan(1, (int)l.hours, (int)l.minutes, (int)l.seconds) - new TimeSpan((int)r.hours, (int)r.minutes, (int)r.seconds);

            return new TimeSpan((int)(l.hours - r.hours), (int)(l.minutes - r.minutes), (int)(l.seconds - r.seconds));
        }

        public static bool operator <(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds < r.totalSeconds;
        }

        public static bool operator >(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds > r.totalSeconds;
        }

        public static bool operator <=(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds <= r.totalSeconds;
        }

        public static bool operator >=(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds >= r.totalSeconds;
        }

        public static bool operator ==(TimeOfDay l, TimeOfDay r)
        {
            return l.totalSeconds == r.totalSeconds;
        }

        public static bool operator !=(TimeOfDay l, TimeOfDay r)
        {
            return !(l == r);
        }
    }
}
