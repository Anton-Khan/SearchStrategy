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
            //-------------------------Passive + Dichotomy 
            SearchAlgorithm algorithm = new SearchAlgorithm(Min, Max, E, Function);
            algorithm.SetAlgoritmType(Algoritms.Passive);
            algorithm.Search();
            algorithm.ShowProcess();
            Console.WriteLine();
            algorithm.SetAlgoritmType(Algoritms.Dichotomy);
            algorithm.Search();
            algorithm.ShowProcess();
            //-------------------------Random Search
            Console.WriteLine();
            algorithm.SetAlgoritmType(Algoritms.RandomSearch);
            algorithm.Search();
            Console.WriteLine("\n\t\t\t\tRandom Search on Unimodal Function");
            algorithm.ShowProcess();
            algorithm.SetMainFunction(MultimodalFunction);
            algorithm.Search();
            Console.WriteLine("\n\t\t\t\tRandom Search on Multimodal Function");
            algorithm.ShowProcess();

            
        }

        static double Function(double x)
        {
            return (x * x) * Math.Sin(x) - 2;
        }
        static double MultimodalFunction(double x)
        {
            return Function(x) * Math.Sin(5 * x);
        }
    }
}
