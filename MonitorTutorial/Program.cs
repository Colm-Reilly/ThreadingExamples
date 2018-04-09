using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorTutorial
{
    class Program
    {
        static object locker = new object();
        static void ThreadMain()
        {
            Thread.Sleep(800);    // Simulate Some work
            WriteToFile();          // Access a shared resource / critical section
        }
        static void WriteToFile()
        {
            String ThreadName = Thread.CurrentThread.Name;
            Console.WriteLine("{0} using C-sharpcorner.com", ThreadName);
            Monitor.Enter(locker);
            try

            {
                using (StreamWriter sw = new StreamWriter(@"D:\akshaydata\akshay.txt", true))
                {
                    sw.WriteLine(ThreadName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Monitor.Exit(locker);
                Console.WriteLine("{0} releasing C-sharpcorner.com", ThreadName);
            }
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)

            {
                Thread thread = new Thread(new ThreadStart(ThreadMain));
                thread.Name = String.Concat("Thread - ", i);
                thread.Start();

            }
            Console.Read();
        }
    }
}
