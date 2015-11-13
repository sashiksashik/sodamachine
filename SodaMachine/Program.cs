using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine(2, 2, 2, 2);
            Wallet userWallet = new Wallet(30, 10, 4, 4);
            while (userWallet.hasAmount((uint)SodaMachine.SODA_PRICE)||userWallet.hasAmount((uint)(SodaMachine.SODA_PRICE-sodaMachine.cuurentBalance.getAmount())))
            {
                Console.Write(sodaMachine.ToString());
                Console.Write("Your wallet:\n" + userWallet.ToString() + "\n");
                String comand = Console.ReadLine();
                if ("push".Equals(comand))
                {
                    try
                    {
                        sodaMachine.getSoda();
                    }
                    catch (CannotGetChangeException ee)
                    {
                        Console.Write("Machine cannot get change. Please buy more soda or insert mor coins.\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if ("back".Equals(comand))
                {
                    userWallet.addWallet(sodaMachine.getChange());
                }
                else
                {
                    try
                    {
                        uint nominal = uint.Parse(comand);
                        userWallet.transfer(Transfer.Sent, nominal);
                        sodaMachine.insertCoin(nominal);
                    }
                    catch (ArgumentException argex)
                    {
                        Console.WriteLine(argex.Message);
                    }
                    catch (Exception e)
                    {
                        Console.Write("Unknown comand.\npush - get soda and change;\nback - return you coins.\n Coin nominal - insert coin. Machine recieves 1,5,25 and 50 cents.");
                    }
                }

            }
            Console.Write("Sorry, you haven't enough money for soda. See you later.");
            Console.Read();
        }
    }
}
