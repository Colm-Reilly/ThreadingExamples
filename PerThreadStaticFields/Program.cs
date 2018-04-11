using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PerThreadStaticFields
{
    class Program
    {
        [ThreadStaticAttribute()]
        public static int bar = 10;

        public static void DisplayStaticFieldValue()
        {
            Console.WriteLine("The thread with hash code {0} and name {1} knows that bar = {2}", Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, bar);
        }

        static void Main(string[] args)
        {

            DisplayStaticFieldValue();

            Thread newStaticFieldThread =
                 new Thread(new ThreadStart(DisplayStaticFieldValue));

            newStaticFieldThread.Name = "ChildThread";

            newStaticFieldThread.Start();

            DisplayStaticFieldValue();

            Console.ReadLine();
        }
    }
}
