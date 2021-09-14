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
            a.SetAlgoritmType(Algoritms.Passive);
            a.Search();
            
        }

        static double Function(double x)
        {
            return (x * x) * Math.Sin(x) - 2;
        }
        
    }
}
