using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingLabs
{
    class Program
    {
        static Thread t1 = new Thread(new ThreadStart(Incrementer));
        static Thread t2 = new Thread(new ThreadStart(Decrementer));

        static void Main(string[] args)
        {
            t1.Start();
            t2.Start();
            Console.ReadLine();
        }


        public static void Incrementer()
        {
            t2.Join();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Incrementer: {0}.", i);
            }

            Console.WriteLine("Incrementer is now finished.");
        }

        public static void Decrementer()
        {
            t1.Join();

            for (int i = 10; i >= 0; i--)
            {
                Console.WriteLine("Decrementer: ===== {0}.", i);
            }

            Console.WriteLine("Decrementer is now finished.");
        }

    }
}
