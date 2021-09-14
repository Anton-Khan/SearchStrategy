using System;
using System.Collections.Generic;

namespace SearchStrategy
{
    class SearchInfo
    {
        public double min;
        public double max;
        public double length;
        public double left;
        public double right;
        public double f_left;
        public double f_right;

        public SearchInfo(double min, double max, double length, double left, double right, double f_left, double f_right)
        {
            this.min = min;
            this.max = max;
            this.length = length;
            this.left = left;
            this.right = right;
            this.f_left = f_left;
            this.f_right = f_right;
        }
    }
    class Dichotomy : BaseAlgoritm
    {
        private double _delta;
        private double _max;
        private double _min;
        private List<SearchInfo> info;
        private Func<double, double> Function;
        public override void SetMainFunction(Func<double, double> function)
        {
            Function = function;
        }

        public override void Start(double min, double max, double e)
        {
            info = new List<SearchInfo>();
            _max = max;
            _min = min;
            _delta = CalculateDelta(e);

            while (_max - _min > e)
            {
                var Xs = CalculateX();
                info.Add(new SearchInfo(_min, _max, _max - _min, Xs.Item1, Xs.Item2, Function(Xs.Item1), Function(Xs.Item2)));
                LineExcluding(Xs);   
            }
        }

        private void LineExcluding( (double, double) Xs)
        {
            if(Function(Xs.Item1) < Function(Xs.Item2))
                _max = Xs.Item2;
            else            
                _min = Xs.Item1;
        }
        
        private (double, double) CalculateX()
        {
            double x = (_max + _min) / 2;
            return (x - _delta, x + _delta);
        }
        private double CalculateDelta(double E)
        {
            return E / 3;
        }
        public override void ShowProcess()
        {
            Console.WriteLine("|   N   ||  start |  end  |length ||  left | right | f(left) | f(right)");
            Console.WriteLine("|_______||________|_______|_______||_______|_______|_________|_________");
            for (int i = 0; i < info.Count; i++)
            {
                Console.WriteLine($"| {(i + 1)}\t|| {info[i].min.ToString("F3")}  |" +
                    $"{info[i].max.ToString("F3")}  |{info[i].length.ToString("F3")}  ||" +
                    $"{info[i].left.ToString("F3")}  |{info[i].right.ToString("F3")}  |" +
                    $"{info[i].f_left.ToString("F3")}  |{info[i].f_right.ToString("F3")}");
            }
            Console.WriteLine("|_______||________|_______|_______||_______|_______|_________|_________");
            Console.WriteLine($"Min = {Function((_max + _min) / 2)} +- {_delta}");
        }
    }
}
