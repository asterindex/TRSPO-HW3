using System;
using System.Collections.Generic;
using System.Threading;

namespace TRSPO_HW3
{
    private class NewDatab : Data
    {
        public NewDatab(int number) : base(number)
        {
            CurNumber = number;
        }

        public long CurNumber { get; set; }
    }

    internal class Calculator2b
    {
        private static readonly Queue<NewData> _queue = new Queue<NewData>();
       
        public static void ThreadHandle()
        {
            while (true)
            {
                NewData data = null;

                lock (_queue)
                {

                    if (_queue.Count > 0)
                    {
                        data = _queue.Dequeue();
                    }
                    else
                        return;
                }

                if (data.CurNumber == 1) 
                    continue;

                if (data.CurNumber % 2 == 0)
                {
                    data.CurNumber = data.CurNumber / 2;
                    lock (_queue)
                    {
                        _queue.Enqueue(data);
                       
                    }
                }
                else
                {
                    data.CurNumber = data.CurNumber * 3 + 1;
                    lock (_queue)
                    {
                        _queue.Enqueue(data);
                      
                    }
                }
            }
        }

        public static void Execute(int numberCount, int threadCount)
        {
            _queue.Clear();
          

            for (var i = 0; i < numberCount; i++) 
               _queue.Enqueue(new NewData(i + 1));
              


            var ths = new List<Thread>();
            for (var i = 0; i < threadCount; i++)
            {
                var th = new Thread(ThreadHandle);
                ths.Add(th);
              
            }
            var t0 = DateTime.Now;
            for (var i = 0; i < threadCount; i++)
            {
               ths[i].Start();
            }
            foreach (var th in ths)
                th.Join();
            
            var t1 = DateTime.Now;
            Console.WriteLine("Calculator2: " + threadCount + " thread(s)" + ": " + (t1 - t0).TotalMilliseconds +
                              " msec");
        }
    }
}