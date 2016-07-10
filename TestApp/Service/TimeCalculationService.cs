using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Service
{
    public class TimeCalculationService : ITimeCalculationService
    {
        public TimeInterval shiftInterval { get; set; }
        public List<TimeInterval> workBreaks { get; set; }

        private TimeInterval morningInterval = new TimeInterval(new TimeSpan(4, 0, 0), new TimeSpan(12, 0, 0));
        private TimeInterval dayInterval = new TimeInterval(new TimeSpan(12, 0, 0), new TimeSpan(20, 0, 0));
        private TimeInterval nightInterval = new TimeInterval(new TimeSpan(20, 0, 0), new TimeSpan(28, 0, 0));

        public WorkDuration Calculate()
        {
            var lastTime = shiftInterval.start;
            var fullTimeCounter = new TimeSpan();

            var morningTimeCounter = new TimeSpan();
            var dayTimeCounter = new TimeSpan();
            var nightTimeCounter = new TimeSpan();

            while(fullTimeCounter < shiftInterval.length)
            {
                lastTime -= new TimeSpan(lastTime.Days * 24, 0, 0);

                if (morningInterval.GetIsTimeInInterval(lastTime))
                {
                    var timeTillEnd = morningInterval.GetTimeTillEnd(lastTime);

                    fullTimeCounter += timeTillEnd;
                    morningTimeCounter = timeTillEnd;
                    lastTime += timeTillEnd;
                }
                else if (dayInterval.GetIsTimeInInterval(lastTime))
                {
                    var timeTillEnd = dayInterval.GetTimeTillEnd(lastTime);

                    fullTimeCounter += timeTillEnd;
                    dayTimeCounter = timeTillEnd;
                    lastTime += timeTillEnd;
                }
                else if (nightInterval.GetIsTimeInInterval(lastTime))
                {
                    var timeTillEnd = nightInterval.GetTimeTillEnd(lastTime);

                    fullTimeCounter += timeTillEnd;
                    nightTimeCounter = timeTillEnd;
                    lastTime += timeTillEnd;
                }
            }

            return new WorkDuration() { morningHours = morningTimeCounter, dayHours = dayTimeCounter, nightHours = nightTimeCounter };
        }
    }
}
