using System;
using System.Collections.Generic;
using System.Threading;

namespace TRSPO_HW3
{
    public class NewData : Data
    {
        public NewData(int number) : base(number)
        {
            CurNumber = number;
        }

        public long CurNumber { get; set; }
    }

    internal class Calculator2
    {
        private static Queue<NewData> _queue;

        private static int _steps = 0;
        private static int _locks = 0;

        public static void ThreadHandle()
        {
            while (true)
            {
                NewData data = null;

                lock (_queue)
                {
                    Interlocked.Increment(ref _locks);
                    if (_queue.Count > 0)
                        data = _queue.Dequeue();
                    else
                        return;
                }

                if (data.CurNumber == 1)
                    continue;

                if (data.CurNumber % 2 == 0)
                {
                    data.CurNumber = data.CurNumber / 2;
                    Thread.Sleep(1);
                   
                    Interlocked.Increment(ref _steps);
                    if (data.CurNumber == 1)
                        continue;
                    else
                        lock (_queue)
                        {
                            Interlocked.Increment(ref _locks);
                            _queue.Enqueue(data);
                        }
                }
                else
                {
                    data.CurNumber = data.CurNumber * 3 + 1;
                    Thread.Sleep(1);
                    
                    Interlocked.Increment(ref _steps);
                    lock (_queue)
                    {
                        Interlocked.Increment(ref _locks);
                        _queue.Enqueue(data);
                    }
                }
            }
        }

        public static void Execute(int numberCount, int threadCount)
        {
            _queue = new Queue<NewData>();
            _steps = 0;
            _locks = 0;


            for (var i = 0; i < numberCount; i++)
                _queue.Enqueue(new NewData(i + 1));


            var ths = new List<Thread>();
            for (var i = 0; i < threadCount; i++)
            {
                var th = new Thread(ThreadHandle);
                ths.Add(th);
            }

            var t0 = DateTime.Now;
            for (var i = 0; i < threadCount; i++) ths[i].Start();
            foreach (var th in ths)
                th.Join();

            var t1 = DateTime.Now;
            Console.WriteLine("Calculator2: " + threadCount + " thread(s)" + ": " + (t1 - t0).TotalMilliseconds +
                              " msec. steps: " + _steps + ". locks: " + _locks);
        }
    }
}