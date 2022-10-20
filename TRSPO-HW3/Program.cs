using System;

namespace TRSPO_HW3
{
    internal class Program
    {
        private const int N = 100000;


        public static void Main(string[] args)
        {
            Calculator1.Execute(N); // в лоб   
            //Calculator1.Execute(N); // в лоб   t1
            //Calculator1.Execute(N);

            //Calculator2.Execute(N, 1); 
            //Calculator2.Execute(N, 2); 
            //Calculator2.Execute(N, 4); // 
           
            Calculator3.Execute(N, 1);  
            Calculator3.Execute(N, 2);
            Calculator3.Execute(N, 4);
            Calculator3.Execute(N, 16);
            //Calculator3.Execute(N, 16);


            
            Console.ReadLine();
        }
    }
}