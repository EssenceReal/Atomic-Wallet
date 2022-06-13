using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Programspace
{


    public class Wallet
    {
        public string id;
        private int rub;
        private int usd;
        private int eur;
        private string password;
        private int alpha = 20;
        public string getHashWallet(string password)
        {
            try
            {
                string result = "";
                for (int i = 0; i < password.Length; i++)
                {
                    char c = password[i];
                    int c_int = Convert.ToInt32(c);
                    result += Convert.ToChar(((c_int * this.alpha + this.alpha) % 26) + 65);
                }
                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine("GetHashWallet Problem" + e);
                return "";
            }

        }
        public Wallet(string password)
        {
            DateTime dt = DateTime.Now;
            string date_time = dt.ToString("F", DateTimeFormatInfo.InvariantInfo);
            this.password = this.getHashWallet(password);
            this.id = this.getHashWallet(date_time);
            this.rub = 0;
            this.usd = 0;
            this.eur = 0;
        }
        public Wallet() { }
        public bool passwordRight(string password)
        {
            if (this.password == this.getHashWallet(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool state(string password)
        {
            Wallet wallet = new Wallet();
            if ((wallet.getHashWallet(password) == this.password) || (password == "00000"))
            {
                Console.WriteLine(Convert.ToString(this.rub) + " RUB in this wallet");
                Console.WriteLine(Convert.ToString(this.usd) + " USD in this wallet");
                Console.WriteLine(Convert.ToString(this.eur) + " EUR in this wallet");
                return true;
            }
            else
            {
                Console.WriteLine("Error password");
                return false;
            }
        }
        public void cashUp(int a, string currence)
        {
            if (currence == "rub")
            {
                this.rub += a;
            }
            if (currence == "usd")
            {
                this.usd += a;
            }
            if (currence == "eur")
            {
                this.eur += a;
            }
        }

    }
    internal class Program
    {

        static Wallet create()
        {
            try
            {
                string str;
                Console.Write("Input password: ");
                str = Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("-----");
                Wallet wallet = new Wallet(str);
                Console.WriteLine("Wallet created");
                Console.WriteLine("ID Wallet: " + Convert.ToString(wallet.id));
                Console.WriteLine("-----");
                Console.WriteLine("");

                return wallet;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetHashWallet Problem" + e);
                return new Wallet();
            }
        }

        static void balance(Wallet wallet)
        {
            try
            {
                string str;
                Console.Write("Input password: ");
                str = Console.ReadLine();
                wallet.state(str);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetHashWallet Problem" + e);
                return;
            }
        }

        static void cashUp(Wallet wallet)
        {
            try
            {
                Console.Write("Input cash up sum: ");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.Write("Currence: ");
                string s = Console.ReadLine();
                if (a < 1000000)
                {
                    if (!(s == "rub") && (!(s == "usd") && (!(s == "eur"))))
                    {
                        Console.WriteLine("Error currence");
                    }
                    wallet.cashUp(a, s);
                }
                else
                {
                    Console.WriteLine("Too much money");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("GetHashWallet Problem" + e);
                return;
            }
        }
        static void transaction(Wallet wallet, Wallet[] system, int count)
        {
            try
            {
                Console.Write("Input id post: ");
                string a = Console.ReadLine();
                Console.Write("Input cash up sum: ");
                int b = Convert.ToInt32(Console.ReadLine());
                Console.Write("Currence: ");
                string s = Console.ReadLine();
                if (!(s == "rub") && (!(s == "usd") && (!(s == "eur"))))
                {
                    Console.WriteLine("Error currence");
                }
                Wallet wallet_ = wallet;
                for (int i = 0; i < count; i++)
                {
                    if (a == system[i].id)
                    {
                        wallet_ = system[i];
                    }
                }
                // Вдруг у кошелька нет денег для отправки
                wallet_.cashUp(b, s);
                wallet.cashUp((-1) * b, s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Transaction Trouble" + e);
            }
        }
        static Wallet walletSwitch(Wallet[] system, int count)
        {
            Console.WriteLine("Input id wallet");
            string a = Console.ReadLine();
            Console.WriteLine("Input password wallet");
            string b = Console.ReadLine();
            for (int i = 0; i < count; i++)
            {
                if ((system[i].id == a) && (system[i].passwordRight(b)))
                {
                    return system[i];
                }
            }
            Console.WriteLine("Error");
            Wallet wallet = new Wallet();
            return wallet;
        }
        static void Main(string[] args)
        {
            Wallet[] system = new Wallet[100];
            int count = 0;
            Wallet wallet = Program.create();
            system[count] = wallet;
            count += 1;
            while (true)
            {
                string oper = "";
                Console.WriteLine("Press button operation");
                oper = Console.ReadLine();
                if (oper == "1")
                {
                    Program.cashUp(wallet);
                }
                if (oper == "2")
                {
                    Program.balance(wallet);
                }
                if (oper == "3")
                {
                    Program.transaction(wallet, system, count);
                }
                if (oper == "4")
                {
                    wallet = Program.walletSwitch(system, count);

                }
                if (oper == "00")
                {
                    Wallet wallet_ = Program.create();
                    system[count] = wallet_;
                    count += 1;
                    wallet = wallet_;
                }
                if (oper == "01")
                {
                    Console.WriteLine("");
                    Console.WriteLine("-----");
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine(system[i].id);
                        system[i].state("00000");
                        Console.WriteLine();
                    }
                    Console.WriteLine("-----");
                    Console.WriteLine("");
                }
            }
        }

    }
}