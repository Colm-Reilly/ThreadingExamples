using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClockTutorial
{
    class Program
    {
        class TickTock
        {
            public void tick(bool running)
            {
                lock (this)
                {
                    if (!running)
                    {  //  stop  the  clock    
                        Monitor.Pulse(this);  //  notify  any  waiting  threads    
                        return;
                    }
                    Console.Write("Tick  ");
                    Monitor.Pulse(this);  //  let  tock()  run    
                    Monitor.Wait(this);  //  wait  for  tock()  to  complete    
                }
            }
            public void tock(bool running)
            {
                lock (this)
                {
                    if (!running)
                    {  //  stop  the  clock    
                        Monitor.Pulse(this);  //  notify  any  waiting  threads    |
                        return;
                    }
                    Console.WriteLine("Tock");
                    Monitor.Pulse(this);  //  let  tick()  run    
                    Monitor.Wait(this);  //  wait  for  tick()  to  complete    
                }
            }
        }
        class MyThread
        {
            public Thread thrd;
            TickTock ttOb;
            //  Construct  a  new  thread.    
            public MyThread(string name, TickTock tt)
            {
                thrd = new Thread(this.run);
                ttOb = tt;
                thrd.Name = name;
                thrd.Start();
            }
            //  Begin  execution  of  new  thread.    
            void run()
            {
                if (thrd.Name == "Tick")
                {
                    for (int i = 0; i < 5; i++) ttOb.tick(true);
                    ttOb.tick(false);
                }
                else
                {
                    for (int i = 0; i < 5; i++) ttOb.tock(true);
                    ttOb.tock(false);
                }
            }
        }
        class TickingClock
        {
            public static void Main()
            {
                TickTock tt = new TickTock();
                MyThread mt1 = new MyThread("Tick", tt);
                MyThread mt2 = new MyThread("Tock", tt);

                mt1.thrd.Join();
                mt2.thrd.Join();
                Console.WriteLine("Clock  Stopped");
                Console.Read();
            }
        }
    }
}
