﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Skender.Stock.Indicators;

namespace StockIndicators.Tests
{
    [TestClass]
    public class UlcerIndexTests : TestBase
    {

        [TestMethod()]
        public void GetUlcerIndexTest()
        {
            int lookbackPeriod = 14;

            IEnumerable<UlcerIndexResult> results = Indicator.GetUlcerIndex(history, lookbackPeriod);

            // assertions

            // proper quantities
            // should always be the same number of results as there is history
            Assert.AreEqual(502, results.Count());
            Assert.AreEqual(502 - lookbackPeriod + 1, results.Where(x => x.UI != null).Count());

            // sample value
            UlcerIndexResult result = results.Where(x => x.Date == DateTime.Parse("12/31/2018")).FirstOrDefault();
            Assert.AreEqual((decimal)5.7255, Math.Round((decimal)result.UI, 4));
        }


        /* EXCEPTIONS */

        [TestMethod()]
        [ExpectedException(typeof(BadHistoryException), "Insufficient history.")]
        public void InsufficientHistory()
        {
            Indicator.GetUlcerIndex(history.Where(x => x.Index < 30), 30);
        }

    }
}