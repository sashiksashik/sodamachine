using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class SodaMachine
    {
        private Wallet wallet;
        public static int SODA_PRICE = 30;
        public Wallet cuurentBalance;
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
        public void insertCoin(uint nominal)
        {
            cuurentBalance.transfer(Transfer.Recieve, nominal);
        }

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
        public Wallet getChange()
        {
            int amount = cuurentBalance.getAmount();
            cuurentBalance = new Wallet();
            return getChange((uint)(amount));
        }
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
        public List<List<uint>> searchChangeVariants(uint n)
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
