using System;
using System.Threading;

namespace TRSPO_HW3
{
    internal class Calculator2Ex
    {
        private static int _steps = 0;

        private static int _rest = 0;
        private static ManualResetEvent _event = new ManualResetEvent(false);

        public static void ThreadHandle(object obj)
        {
            var data = (NewData) obj;

            if (data.CurNumber == 1)
            {
                if (Interlocked.Decrement(ref _rest) == 0)
                    _event.Set();
                return;
            }

            if (data.CurNumber % 2 == 0)
            {
                data.CurNumber = data.CurNumber / 2;
                Interlocked.Increment(ref _steps);
                ThreadPool.QueueUserWorkItem(ThreadHandle, data);
            }
            else
            {
                data.CurNumber = data.CurNumber * 3 + 1;
                Interlocked.Increment(ref _steps);
                ThreadPool.QueueUserWorkItem(ThreadHandle, data);
            }
        }

        public static void Execute(int numberCount)
        {
            _steps = 0;
            _rest = numberCount;


            var t0 = DateTime.Now;
            for (var i = 0; i < numberCount; i++) ThreadPool.QueueUserWorkItem(ThreadHandle, new NewData(i + 1));

            _event.WaitOne();
            var t1 = DateTime.Now;
            Console.WriteLine("Calculator2Ex: " + ": " + (t1 - t0).TotalMilliseconds +
                              " msec. steps: " + _steps);
        }
    }
}