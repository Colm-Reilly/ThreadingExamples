using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Monitors
{
    class Program
    {
        private static int numericField = 0;

        private static object syncObj = new object();

        static Thread t1 = new Thread(new ThreadStart(IncrementNumericField));
        static Thread t2 = new Thread(new ThreadStart(DecrementNumericField));

        static void Main(string[] args)
        {
            Console.WriteLine("Numeric field initial value = " + numericField);

            t1.Start();   //increments
            t2.Start();   //decrements
            t1.Join();

            //lets create a method which modifies the numericField from the main() meth 
            ModifyNumericField(20);  //is this a good idea?  

            Console.ReadLine();  //we need this so the console does not disappear
        }

        public static void IncrementNumericField()
        {
            Monitor.Enter(syncObj);
            try
            {
                for (int i = 0; i < 10; i++)  //increment 10 times
                {
                    ++numericField;
                    Console.WriteLine("Increment = " + numericField);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Monitor.Exit(syncObj);
            }

            //********************| USING  MONITOR.TRYENTER | ********************
            if (Monitor.TryEnter(syncObj, 1000))
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ++numericField;
                        Console.WriteLine("Increment = " + numericField);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Monitor.Exit(syncObj);
                }
            }

            //********************| USING  LOCK | ********************
            lock (syncObj)
            {
                for (int i = 0; i < 10; i++)
                {
                    ++numericField;
                    Console.WriteLine("Increment = " + numericField);
                }
            }
            ////
        }

        public static void DecrementNumericField()
        {
            Monitor.Enter(syncObj);
            try
            {
                if (numericField >= 5)
                {
                    //decrement 10 times
                    for (int i = 0; i < 10; i++)
                    {
                        --numericField;
                        Console.WriteLine("Decrement = " + numericField);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Monitor.Exit(syncObj);
            }

            // ********************| USING  MONITOR.TRYENTER  |********************
            if (Monitor.TryEnter(syncObj, 1000))
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        --numericField;
                        Console.WriteLine("Decrement = " + numericField);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Monitor.Exit(syncObj);
                }
            }

            //********************| USING  LOCK | ********************
            lock (syncObj)
            {
                for (int i = 0; i < 10; i++)
                {
                    --numericField;
                    Console.WriteLine("Decrement = " + numericField);
                }
            }
            ///
        }


        public static void ModifyNumericField(int newValue)
        {
            // is this a good idea?  
            numericField = newValue;
            Console.WriteLine("Numeric field modified in the main() = " + numericField);
        }
    }
}
