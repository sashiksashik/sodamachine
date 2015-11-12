using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    /// <summary>
    /// Represents wallet
    /// </summary>
    public class Wallet
    {

        private List<Coin> coins;
        /// <summary>
        /// Initialize wallet
        /// </summary>
        /// <param name="centCount">1 cent pcs</param>
        /// <param name="cent5Count">5 cent pcs</param>
        /// <param name="cent25Count">25 cent pcs</param>
        /// <param name="cent50Count">50 cent pcs</param>
        public Wallet(uint centCount, uint cent5Count, uint cent25Count, uint cent50Count)
        {
            coins = new List<Coin>();
            coins.Add(new Coin(1, centCount));
            coins.Add(new Coin(5, cent5Count));
            coins.Add(new Coin(25, cent25Count));
            coins.Add(new Coin(50, cent50Count));
        }
        /// <summary>
        /// Calculate coins amount in wallet
        /// </summary>
        /// <returns>Coins amount</returns>
        public uint getAmount()
        {
            uint amount = 0;
            coins.ForEach(coin => amount += coin.nominal * coin.count);
            return amount;
        }
    }
    /// <summary>
    /// Represents coin in wallet
    /// </summary>
    public class Coin
    {
        public uint nominal { get; }
        public uint count { get; set; }
        public Coin() { }
        public Coin(uint nominal, uint count)
        {
            this.nominal = nominal;
            this.count = count;
        }
    }
}
