﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Service
{
    using KVPair = KeyValuePair<WorkTimeType, TimeInterval>;
    public class TimeCalculationService : ITimeCalculationService
    {
        public TimeInterval shiftInterval { get; set; }
        public List<TimeInterval> workBreaks { get; set; }

        private List<KVPair> intervalsOriginal = new List<KVPair>()
        {
            new KVPair(WorkTimeType.Morning, new TimeInterval(4, 12)),
            new KVPair(WorkTimeType.Day, new TimeInterval(12, 20)),
            new KVPair(WorkTimeType.Evening, new TimeInterval(20, 4))
        };

        public TimeCalculationService()
        {
            workBreaks = new List<TimeInterval>();
        }

        public WorkDuration Calculate()
        {
            var intervals = AccountForBreaks();

            var finalTimes = from a in intervals
                             group a by a.Key into groupedIntervals
                             from g in groupedIntervals
                             let hours = (g.Value * shiftInterval).Select(x => x.length)
                             select new { type = groupedIntervals.Key, hours = hours.Aggregate((x, y) => x + y) };

            var result = new WorkDuration();

            foreach (var time in finalTimes)
            {
                switch (time.type)
                {
                    case WorkTimeType.Morning:
                        result.morningHours = time.hours;
                        break;
                    case WorkTimeType.Day:
                        result.dayHours = time.hours;
                        break;
                    case WorkTimeType.Evening:
                        result.eveningHours = time.hours;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private List<KVPair> AccountForBreaks()
        {
            if (workBreaks.Count == 0)
                return intervalsOriginal;

            var intervalsWithBreaks = from a in intervalsOriginal
                                      from b in workBreaks
                                      let diff = a.Value - b
                                      from i in diff
                                      select new KVPair(a.Key, i);

            return intervalsWithBreaks.ToList();
        }
    }
}
