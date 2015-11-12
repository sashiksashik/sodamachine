using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine;

namespace TestSodaMchine
{
    [TestClass]
    public class WalletTest
    {
        [TestMethod]
        public void CorrectAmountCalculating()
        {
            Wallet wallet = new Wallet(1, 2, 3, 4);
            Assert.AreEqual(286, wallet.getAmount());
            Wallet secondWallet = new Wallet(1, 0, 1, 1);
            Assert.AreEqual(76, secondWallet.getAmount());
            Wallet thirdWallet = new Wallet(0, 0, 0, 0);
            Assert.AreEqual(0, thirdWallet.getAmount());
        }
    }
}
