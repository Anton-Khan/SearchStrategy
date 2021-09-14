using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchStrategy
{
    class OptimalPassiveSearch : BaseAlgoritm 
    {
        private List<double> Ys;
        private List<double> Xs;
        private Func<double, double> Function;
        private int _N;
        private double _delta;

        public override void SetMainFunction(Func<double, double> function)
        {
            Function = function;
        }

        public override void Start(double min, double max, double e)
        {
            _N = CalculateN(min, max, e);
            _delta = CalculateDelta(min, max);
            double distance = DistanceBetweenX(min, max);
            
            Ys = new List<double>();
            Xs = new List<double>();
            for (double x = min; x < max; x+=distance)
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
            Console.WriteLine($"|  N\t|   X   |  F(X)");
            Console.WriteLine($"|_______|_______|_______");
            for (int i = 0; i < Ys.Count; i++)
            {
                Console.WriteLine($"| {(i+1)}\t| {Xs[i].ToString("F3")}\t|{Ys[i].ToString("F3")}");
            }
            Console.WriteLine($"|_______|_______|_______");
            Console.WriteLine($"Min = {Ys.Min()} +- {_delta}");
        }

    }
}
