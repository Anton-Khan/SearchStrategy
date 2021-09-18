using System;
using System.Collections.Generic;
using System.Linq;


namespace SearchStrategy.AlgorithmTypes
{
    class RandomSearch : BaseAlgoritm
    {
        private List<double> Ps;
        private List<double> Qs;
        private Func<double, double> Function;
        private List<List<int>> _N;
        private List<List<double>> _Extremums;

        private double _max;
        private double _min;
        public RandomSearch(ISearchProperties properties)
        {
            _max = properties.Max;
            _min = properties.Min;
            Function = properties.Function;

        InitializePS(0.9, 0.99, 0.01, 0.005, 0.1, 0.005);
        }
        private void InitializePS(double Pmin, double Pmax, double Pstep, double Qmin, double Qmax, double Qstep)
        {
            Ps = new List<double>();
            for (double i = Pmin; i <= Pmax+0.000001; i+=Pstep)
                Ps.Add(i);

            Qs = new List<double>();
            for (double i = Qmin; i <= Qmax; i += Qstep)
                Qs.Add(i);
        }
        private int CalculateN(double P, double q)
        {
            return (int)(Math.Log(1 - P) / Math.Log(1 - q));
        }
        private void CalculateNs()
        {
            _N = new List<List<int>>();
            for (int i = 0; i < Qs.Count; i++)
            {
                _N.Add(new List<int>());
                for (int j = 0; j < Ps.Count; j++)
                {
                    _N[i].Add(CalculateN(Ps[j], Qs[i]));
                }
            }
        }

        private List<List<double>> FindExtremums(Func<double, double> function)
        {
            List<List<double>> extremums = new List<List<double>>();
            for (int i = 0; i < _N.Count; i++)
            {
                extremums.Add(new List<double>());
                for (int j = 0; j < _N[i].Count; j++)
                {
                    extremums[i].Add(FindMinimumAtN(_N[i][j],function));
                }
            }
            return extremums;
        }

        private double FindMinimumAtN(int N, Func<double, double> function)
        {
            double[] points = new double[N];
            Random rand = new Random();
            for (int i = 0; i < N; i++)
            {
                points[i] = function(GetRandomPointAt(_min, _max, rand));
            }
            return points.Min();
        }

        private double GetRandomPointAt(double min, double max, Random rand)
        {
            return min + rand.NextDouble() * (max - min);
        }

        public override void SetMainFunction(Func<double, double> function)
        {
            Function = function;
        }

        public override void Start()
        {
            CalculateNs();
            _Extremums = FindExtremums(Function);
        }
        public override void ShowProcess()
        {
            Console.WriteLine("\t\t\t\t\t\t\t\t\tN from P and q");
            DrawLine(7,7);
            DrawPsLine(1);
            DrawLine(7, 7);

            for (int i = 0; i < _N.Count; i++)
            {
                Console.Write($"| {Qs[i].ToString("F2")}\t");
                for (int j = 0; j < _N[i].Count; j++)
                {
                    Console.Write($"|  {_N[i][j]} \t");
                }
                Console.WriteLine("|");
            }
            DrawLine(7, 7);
            Console.WriteLine();

            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\tF(x) from P and q");
            DrawLine(7, 9);
            DrawPsLine(2);
            DrawLine(7, 9);
            for (int i = 0; i < _Extremums.Count; i++)
            {
                Console.Write($"| {Qs[i].ToString("F2")}\t");
                for (int j = 0; j < _Extremums[i].Count; j++)
                {
                    Console.Write($"| {_Extremums[i][j].ToString("F3")} ");
                }
                Console.WriteLine("|");
            }
            DrawLine(7, 9);
            Console.WriteLine();
            
        }

        private void DrawLine(int first, int second)
        {
            Console.Write("+" + new string('-', first));
            for (int i = 0; i < Ps.Count; i++)
            {
                Console.Write("+" + new string('-', second));
            }
            Console.WriteLine("+");
        }
        private void DrawPsLine(int second)
        {
            Console.Write($"| q \\ P\t");
            for (int i = 0; i < Ps.Count; i++)
            {
                Console.Write("|" + new string(' ', second) + Ps[i].ToString("F3") + new string(' ', second));
            }
            Console.WriteLine("|");
        }
    }
}
