using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;

namespace UnitTests
{
    [TestClass]
    public class TimeIntervalTests
    {
        [TestMethod]
        public void IntervalTestSubstractionFromMiddle()
        {
            var int9to5 = new TimeInterval(9, 17);
            var int12to13 = new TimeInterval(12, 13);

            var res = int9to5 - int12to13;

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(new TimeInterval(9, 12), res[0]);
            Assert.AreEqual(new TimeInterval(13, 17), res[1]);
        }

        [TestMethod]
        public void IntervalTestSubstractionFromStart()
        {
            var int9to5 = new TimeInterval(9, 17);
            var int5to11 = new TimeInterval(5, 11);

            var res = int9to5 - int5to11;

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(new TimeInterval(11, 17), res[0]);
        }

        [TestMethod]
        public void IntervalTestSubstractionFromEnd()
        {
            var int9to5 = new TimeInterval(9, 17);
            var int16to20 = new TimeInterval(16, 20);

            var res = int9to5 - int16to20;

            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(new TimeInterval(9, 16), res[0]);
        }


        [TestMethod]
        public void IntervalTestProductFromMiddle()
        {
            var int9to5 = new TimeInterval(9, 17);
            var int10to13 = new TimeInterval(10, 13);

            var res = int9to5 * int10to13;

            Assert.AreEqual(new TimeInterval(10, 13), res);
        }

        [TestMethod]
        public void IntervalTestProductFromOutside()
        {
            var int10to14 = new TimeInterval(10, 14);
            var int9to15 = new TimeInterval(9, 15);

            var res = int10to14 * int9to15;

            Assert.AreEqual(new TimeInterval(10, 14), res);
        }

        [TestMethod]
        public void IntervalTestProductFromOutsideNotIntersecting()
        {
            var res = new TimeInterval(20, 4) * new TimeInterval(12, 13);

            Assert.AreEqual(new TimeInterval(), res);
        }

        [TestMethod]
        public void IntervalTestProductFromStart()
        {
            var res = new TimeInterval(9, 17) * new TimeInterval(5, 11);

            Assert.AreEqual(new TimeInterval(9, 11), res);
        }

        public void IntervalTestProductFromEnd()
        {
            var res = new TimeInterval(9, 17) * new TimeInterval(15, 20);

            Assert.AreEqual(new TimeInterval(15, 17), res);
        }
    }
}
