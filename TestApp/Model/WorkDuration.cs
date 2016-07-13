using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public struct WorkDuration
    {
        public TimeSpan morningHours { get; set; }
        public TimeSpan dayHours { get; set; }
        public TimeSpan eveningHours { get; set; }
    }
}
