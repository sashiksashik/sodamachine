using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public enum Transfer { Sent, Recieve }
    /// <summary>
    /// Represents wallet
    /// </summary>
    public class Wallet
    {

        private Dictionary<uint, uint> coins;

        public Wallet()
        {
            coins = new Dictionary<uint, uint>();
            coins.Add(1, 0);
            coins.Add(5, 0);
            coins.Add(25, 0);
            coins.Add(50, 0);
        }
        /// <summary>
        /// Initialize wallet
        /// </summary>
        /// <param name="centCount">1 cent pcs</param>
        /// <param name="cent5Count">5 cent pcs</param>
        /// <param name="cent25Count">25 cent pcs</param>
        /// <param name="cent50Count">50 cent pcs</param>
        public Wallet(uint centCount, uint cent5Count, uint cent25Count, uint cent50Count)
        {
            coins = new Dictionary<uint, uint>();
            coins.Add(1, centCount);
            coins.Add(5, cent5Count);
            coins.Add(25, cent25Count);
            coins.Add(50, cent50Count);
        }
        /// <summary>
        /// Calculate coins amount in wallet
        /// </summary>
        /// <returns>Coins amount</returns>
        public int getAmount()
        {
            uint amount = 0;
            var enumerator = coins.GetEnumerator();
            while (enumerator.MoveNext())
            {
                amount += enumerator.Current.Value * enumerator.Current.Key;
            }
            return (int)amount;
        }
        /// <summary>
        /// Method checks required amount.
        /// </summary>
        /// <param name="amount">Required amount</param>
        /// <returns></returns>
        public bool hasAmount(uint amount)
        {
            if (amount <= getAmount()) return true; else return false;
        }

        /// <summary>
        /// Method transfers one coin
        /// </summary>
        /// <param name="transfer">Transfer type</param>
        /// <param name="nominal">Coin nominal</param>
        public void transfer(Transfer transfer, uint nominal)
        {
            if (coins.ContainsKey(nominal))
            {
                if (transfer.Equals(Transfer.Recieve)) coins[nominal]++;
                if (transfer.Equals(Transfer.Sent) && coins[nominal] > 0) coins[nominal]--;
            }
            else throw new ArgumentException("There are not coin "+nominal+" cent in wallet.");
        }
        /// <summary>
        /// Wallet string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Amount : " + getAmount() + ". Coins:");
            var e = coins.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value > 0) str.Append(" " + e.Current.Key + " cent " + e.Current.Value + " pcs;");
            }
            return str.ToString();
        }

        public Dictionary<uint, uint> getCoins()
        {
            return this.coins;
        }
        public void addWallet(Wallet anotherWallet)
        {
            var e = anotherWallet.coins.GetEnumerator();
            while (e.MoveNext())
            {
                uint numinal = e.Current.Key;
                this.coins[numinal] += anotherWallet.coins[numinal];
            }
        }
    }

}
