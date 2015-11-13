using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    /// <summary>
    /// Represents soda machine
    /// </summary>
    public class SodaMachine
    {
        /// <summary>
        /// Soda machinу wallet.
        /// </summary>
        private Wallet wallet;
        /// <summary>
        /// Cup of soda price
        /// </summary>
        public static int SODA_PRICE = 30;
        /// <summary>
        /// Current balance
        /// </summary>
        public Wallet cuurentBalance;
        /// <summary>
        /// Initialises soda machine
        /// </summary>
        /// <param name="centCount">1 cent pcs</param>
        /// <param name="cent5Count"><5 cent pcs</param>
        /// <param name="cent25Count">25 cent pcs</param>
        /// <param name="cent50Count">50 cent pcs</param>
        public SodaMachine(uint centCount, uint cent5Count, uint cent25Count, uint cent50Count)
        {
            wallet = new Wallet(centCount, cent5Count, cent25Count, cent50Count);
            cuurentBalance = new Wallet();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Price " + SODA_PRICE + " cent\n");
            str.Append("You current balance:\n");
            str.Append(cuurentBalance.ToString() + "\n");
            return str.ToString();
        }
        /// <summary>
        /// Method inserts coin to machine
        /// </summary>
        /// <param name="nominal">coin nominal</param>
        public void insertCoin(uint nominal)
        {
            cuurentBalance.transfer(Transfer.Recieve, nominal);
        }
        /// <summary>
        /// Methods get soda for buyer.
        /// </summary>
        public void getSoda()
        {
            if (cuurentBalance.hasAmount((uint)SODA_PRICE))
            {
                wallet.addWallet(cuurentBalance);
                cuurentBalance = getChange((uint)(cuurentBalance.getAmount() - SODA_PRICE));
            }
            else
            {
                throw new Exception("Inserted coins are not enough for soda.");
            }
        }
        
        /// <summary>
        /// Method get back coins from current balance.
        /// </summary>
        /// <returns></returns>
        public Wallet getChange()
        {
            int amount = cuurentBalance.getAmount();
            cuurentBalance = new Wallet();
            return getChange((uint)(amount));
        }
        /// <summary>
        /// Nethod get change represented by wallet.
        /// </summary>
        /// <param name="amount">Change amount</param>
        /// <returns></returns>
        private Wallet getChange(uint amount)
        {
            if (amount > 0)
            {
                Wallet changeWallet = new Wallet();
                var list = searchChangeVariants(amount);
                list.Sort((a, b) => a.Count.CompareTo(b.Count));
                List<uint> change = null;
                var e = list.GetEnumerator();
                while (e.MoveNext() && change == null)
                {
                    if (haveChange(e.Current)) change = e.Current;
                }
                if (change != null) change.ForEach(nominal => changeWallet.transfer(Transfer.Recieve, nominal)); else throw new CannotGetChangeException("Cannot get change, please insert more coins. Machines wallet :\n" + wallet.ToString());
                return changeWallet;
            }
            else return new Wallet();
        }

        /// <summary>
        /// Method checks machine posibility to get change
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        private bool haveChange(List<uint> coins)
        {
            Wallet w = new Wallet();
            foreach (uint nominal in coins)
            {
                w.transfer(Transfer.Recieve, nominal);
            }
            var e = wallet.getCoins().GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value < w.getCoins()[e.Current.Key]) return false;
            }
            return true;
        }
        /// <summary>
        /// Method cheack possibility to pick up the change with a coin in the machine
        /// </summary>
        /// <param name="n">amount</param>
        /// <returns></returns>
        private bool canGetChange(int n)
        {
            if (n < 1)
                return false;
            if (wallet.getCoins().ContainsKey((uint)n))
            {
                return true;
            }
            else
            {
                bool isTrue = false;
                foreach (var i in wallet.getCoins().Keys)
                {
                    if (canGetChange((int)(n - i)))
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            return false;
        }
        /// <summary>
        /// Method serach all change variants 
        /// </summary>
        /// <param name="n">change amount</param>
        /// <returns>list of all change variants</returns>
        private List<List<uint>> searchChangeVariants(uint n)
        {
            List<List<uint>> list = new List<List<uint>>();
            if (this.wallet.getCoins().ContainsKey(n))
            {
                List<uint> l = new List<uint>();
                l.Add(n);
                list.Add(l);
            }
            else
                foreach (var i in wallet.getCoins().Keys)
                {
                    if (canGetChange((int)(n - i)))
                    {
                        foreach (List<uint> l in searchChangeVariants(n - i))
                        {
                            List<uint> ll = new List<uint>();
                            ll.Add(i);
                            ll.AddRange(l);
                            list.Add(ll);
                        }
                    }
                }
            return list;
        }


    }
    public class CannotGetChangeException : Exception
    {
        public CannotGetChangeException(String message) : base(message)
        {
        }
    }
}
