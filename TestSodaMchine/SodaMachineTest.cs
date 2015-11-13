using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine;
namespace TestSodaMchine
{
    /// <summary>
    /// Сводное описание для SodaMachineTest
    /// </summary>
    [TestClass]
    public class SodaMachineTest
    {

        [TestMethod]
        public void CurrentBalnceGetSodaGetChange()
        {
            SodaMachine.SodaMachine soda = new SodaMachine.SodaMachine(2, 2, 2, 2);
            soda.insertCoin(5);
            Assert.AreEqual(5, soda.cuurentBalance.getAmount());
            soda.insertCoin(25);
            Assert.AreEqual(30, soda.cuurentBalance.getAmount());
            soda.getSoda();
            Assert.AreEqual(0, soda.cuurentBalance.getAmount());
            soda.insertCoin(25);
            Assert.AreEqual(25, soda.cuurentBalance.getAmount());
            Assert.AreEqual(soda.cuurentBalance.getAmount(), soda.getChange().getAmount());
        }
    }
}
