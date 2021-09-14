using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchStrategy
{
    class OptimalPassiveSearch : BaseAlgoritm 
    {
        private List<double> Ys;
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
            for (double x = min; x < max; x+=distance)
            {
                Ys.Add(Function(x));
                Console.WriteLine(Ys.Last());
            }

            Console.WriteLine($"Min = {Ys.Min()} +- {_delta}");
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
        
    }
}
