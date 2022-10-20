using System;
using System.Collections.Generic;
using System.Threading;

namespace TRSPO_HW3
{
    internal class Calculator3
    {
        public static Queue<Data> _queue = new Queue<Data>();
        private static int _steps = 0;
        private static int _locks = 0;

        public static void ThreadHandle()
        {
            while (true)
            {
                Data data;
                lock (_queue)
                {
                    Interlocked.Increment(ref _locks);
                    if (_queue.Count > 0)
                        data = _queue.Dequeue();
                    else
                        return;
                }

                data.Steps = Helper.Handle(data.InitNumber);
                Interlocked.Add(ref _steps, data.Steps);
            }
        }

        public static void Execute(int numberCount, int threadCount)
        {
            _queue.Clear();
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
            Console.WriteLine("Calculator3: " + threadCount + " thread(s)" + ": " + (t1 - t0).TotalMilliseconds +
                              " msec. steps: " + _steps + ". locks: " + _locks);
        }
    }
}