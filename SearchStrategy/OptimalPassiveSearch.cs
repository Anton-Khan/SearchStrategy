using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchStrategy
{
    class OptimalPassiveSearch : BaseAlgoritm 
    {
        private List<double> Ys;
        private List<double> Xs;
        private Func<double, double> Function;
        private int _N;
        private double _delta;
        private double _max;
        private double _min;
        private double _e;

        public OptimalPassiveSearch(ISearchProperties properties)
        {
            _max = properties.Max;
            _min = properties.Min;
            _e = properties.E;
            Function = properties.Function;
        }

        public override void SetMainFunction(Func<double, double> function)
        {
            Function = function;
        }

        public override void Start()
        {
            _N = CalculateN(_min, _max, _e);
            _delta = CalculateDelta(_min, _max);
            double distance = DistanceBetweenX(_min, _max);
            
            Ys = new List<double>();
            Xs = new List<double>();
            for (double x = _min; x < _max; x+=distance)
            {
                Xs.Add(x);
                Ys.Add(Function(x));
            }  
        }
        private int CalculateN(double min, double max, double e)
        {
            return (int)(2 * (max - min) / e - 1);
        }

        private double DistanceBetweenX(double min , double max)
        {
            return (max - min) / _N;
        }

        private double CalculateDelta(double min, double max)
        {
            return (max - min) / (_N + 1);
        }
        
        public override void ShowProcess()
        {
            Console.WriteLine("Optimal  Passive  Search");
            Console.WriteLine($"|  N\t|   X   |  F(X)");
            Console.WriteLine($"|_______|_______|_______");
            for (int i = 0; i < Ys.Count; i++)
            {
                Console.WriteLine($"| {(i+1)}\t| {Xs[i].ToString("F3")}\t|{Ys[i].ToString("F3")}");
            }
            Console.WriteLine($"|_______|_______|_______");
            Console.WriteLine($"Min:\nX = {Xs[Ys.IndexOf(Ys.Min())].ToString("F5")} +- {_delta}\nF(x) = {Ys.Min().ToString("F5")}");
        }

    }
}
