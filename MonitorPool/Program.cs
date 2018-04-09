using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorPool
{
    class Program
    {
        private const int threads = 3;
        private const int workitems = 20;
        private static Object locker = new Object();
        static void Worker()
        {
            while (true)
            {
                lock (locker)
                {
                    Monitor.Wait(locker);
                }
                System.Console.WriteLine("{0} access the website c-sharpcorner", Thread.CurrentThread.Name);
                Thread.Sleep(100);
            }
        }
        [STAThread]
        static void Main(string[] args)
        {
            Thread[] t = new Thread[threads];
            for (int k = 0; k < threads; k++)
            {
                t[k] = new Thread(new ThreadStart(Worker));
                t[k].Name = "user " + k;
                t[k].IsBackground = true;
                t[k].Start();
            }
            for (int i = 0; i < workitems; i++)
            {
                Thread.Sleep(1000);
                lock (locker)
                {
                    Monitor.Pulse(locker);
                }
            }
            Console.Read();
        }
    }
}
