using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApp.Service;
using TestApp.Model;

namespace UnitTests
{
    [TestClass]
    public class TimeCalcTests
    {
        private WorkDuration GetHours(TimeOfDay shiftStart, TimeOfDay shiftEnd)
        {
            var service = new TimeCalculationService();

            service.shiftInterval = new TimeInterval(shiftStart, shiftEnd);

            return service.Calculate();
        }

        [TestMethod]
        public void Test4To20Shift()
        {
            var result = GetHours(new TimeOfDay(4, 0, 0), new TimeOfDay(20, 0, 0));

            Assert.AreEqual(new TimeSpan(8, 0, 0), result.morningHours, "Morning hours.");
            Assert.AreEqual(new TimeSpan(8, 0, 0), result.dayHours, "Day hours.");
            Assert.AreEqual(new TimeSpan(0, 0, 0), result.eveningHours, "Night hours.");
        }

        [TestMethod]
        public void Test0To24Shift()
        {
            var result = GetHours(new TimeOfDay(0, 0, 0), new TimeOfDay(24, 0, 0));

            Assert.AreEqual(new TimeSpan(8, 0, 0), result.morningHours, "Morning hours.");
            Assert.AreEqual(new TimeSpan(8, 0, 0), result.dayHours, "Day hours.");
            Assert.AreEqual(new TimeSpan(8, 0, 0), result.eveningHours, "Night hours.");
        }

        [TestMethod]
        public void Test20To12Shift()
        {
            var result = GetHours(new TimeOfDay(20, 0, 0), new TimeOfDay(12, 0, 0));

            Assert.AreEqual(new TimeSpan(8, 0, 0), result.morningHours, "Morning hours.");
            Assert.AreEqual(new TimeSpan(0, 0, 0), result.dayHours, "Day hours.");
            Assert.AreEqual(new TimeSpan(8, 0, 0), result.eveningHours, "Night hours.");
        }

        [TestMethod]
        public void Test5to4Shift()
        {
            var result = GetHours(new TimeOfDay(5, 0, 0), new TimeOfDay(4, 0, 0));

            Assert.AreEqual(new TimeSpan(7, 0, 0), result.morningHours, "Morning hours.");
            Assert.AreEqual(new TimeSpan(8, 0, 0), result.dayHours, "Day hours.");
            Assert.AreEqual(new TimeSpan(8, 0, 0), result.eveningHours, "Night hours.");
        }
    }
}
