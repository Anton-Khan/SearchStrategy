using System;

namespace SearchStrategy
{
    class Program
    {
        public static double Min = 4;
        public static double Max = 7;
        public static double E = 0.1;

        static void Main(string[] args)
        {
            SearchAlgoritm a = new SearchAlgoritm(Min, Max, E, Function);
            Console.WriteLine("Optimal Passive Search");
            a.SetAlgoritmType(Algoritms.Passive);
            a.Search();
            a.ShowProcess();
            Console.WriteLine();
            Console.WriteLine("\t\t\t\tDichotomy");
            a.SetAlgoritmType(Algoritms.Dichotomy);
            a.Search();
            a.ShowProcess();

        }

        static double Function(double x)
        {
            return (x * x) * Math.Sin(x) - 2;
        }
        
    }
}
