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

        private Dictionary<uint,uint> coins;
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
                amount+=enumerator.Current.Value*enumerator.Current.Key;
            }
            return (int)amount;
        }

        public bool hasAmount(uint amount)
        {
            if (amount < +getAmount()) return true; else return false;
        }
    }
    
}
