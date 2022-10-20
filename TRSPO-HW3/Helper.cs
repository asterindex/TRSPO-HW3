namespace TRSPO_HW3
{
    internal class Helper
    {
        public static int Handle(long num)
        {
            var steps = 0;
            var init = num;
            while (num != 1)
            {
                steps++;
                if (num % 2 == 0)
                    num = num / 2;
                else
                    num = num * 3 + 1;
            }

            return steps;
        }
    }
}