﻿using System;
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

        [TestMethod]
        public void TransferAndHasAmount()
        {
            Wallet wallet = new Wallet(1, 0, 1, 1);
            Assert.IsTrue(wallet.hasAmount(70));
            wallet.transfer(Transfer.Recieve, 5);
            Assert.AreEqual(81, wallet.getAmount());
            Assert.IsTrue(wallet.hasAmount(81));
            Assert.IsFalse(wallet.hasAmount(82));
            wallet.transfer(Transfer.Sent, 50);
            Assert.IsTrue(wallet.hasAmount(31));
            Assert.IsFalse(wallet.hasAmount(32));
        }

        [TestMethod]
        public void AddWallet()
        {
            Wallet wallet = new Wallet(1, 0, 1, 0);
            Wallet anotherWallet = new Wallet(0, 1, 0, 1);
            wallet.addWallet(anotherWallet);
            Assert.AreEqual(81, wallet.getAmount());
        }
    }
}
