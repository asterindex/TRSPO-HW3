using System;
using System.Collections.Generic;
using System.Threading;

namespace TRSPO_HW3
{
    internal class Calculator1
    {
        private static List<Data> _list = new List<Data>();
        private static int _steps = 0;

        public static void ThreadHandle()
        {
            for (var i = 0; i < _list.Count; i++)
            {
                //lock (_list)
                {
                    _list[i].Steps = Helper.Handle(_list[i].InitNumber);
                    _steps += _list[i].Steps;
                }
            }
        }

        public static void Execute(int numberCount)
        {
            _list.Clear();
            _steps = 0;
            for (var i = 0; i < numberCount; i++)
                _list.Add(new Data(i + 1));

            var th = new Thread(ThreadHandle);
            var t0 = DateTime.Now;
            th.Start();
            th.Join();

            var t1 = DateTime.Now;
            Console.WriteLine("Calculator1: 1 thread" + ": " + (t1 - t0).TotalMilliseconds + " msec. steps: " + _steps);
        }
    }
}