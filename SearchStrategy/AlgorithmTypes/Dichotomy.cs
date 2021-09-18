using System;
using System.Collections.Generic;

namespace SearchStrategy.AlgorithmTypes
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

        public string ToString(int i)
        {
            return $"| {(i + 1)}\t|| {min.ToString("F3")}  |" +
                    $"{max.ToString("F3")}  |{length.ToString("F3")}  ||" +
                    $"{left.ToString("F3")}  |{right.ToString("F3")}  |" +
                    $"{f_left.ToString("F3")}  |{f_right.ToString("F3")}";
        }
        public string ToStringWithoutGeneratedPart(int i)
        {
            return $"| {(i + 1)}\t|| {min.ToString("F3")}  |" +
                    $"{max.ToString("F3")}  |{length.ToString("F3")}  ||" +
                    $"-------|-------|---------|--------";
        }
    }
    class Dichotomy : BaseAlgoritm
    {
        private double _delta;
        private double _max;
        private double _min;
        private double _e;
        private List<SearchInfo> info;
        private Func<double, double> Function;
        public Dichotomy(ISearchProperties properties)
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
            info = new List<SearchInfo>();
            _delta = CalculateDelta(_e);
            (double, double) Xs = (0,0);
            while (_max - _min >= _e)
            {
                Xs = CalculateX();
                info.Add(new SearchInfo(_min, _max, _max - _min, Xs.Item1, Xs.Item2, Function(Xs.Item1), Function(Xs.Item2)));
                LineExcluding(Xs);
            }
            info.Add(new SearchInfo(_min, _max, _max - _min, 0,0,0,0));
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
            Console.WriteLine("\t\t\t\tDichotomy");
            Console.WriteLine("|   N   ||  start |  end  |length ||  left | right | f(left) | f(right)");
            Console.WriteLine("|_______||________|_______|_______||_______|_______|_________|_________");
            for (int i = 0; i < info.Count-1; i++)
            {
                Console.WriteLine(info[i].ToString(i));
            }
            Console.WriteLine(info[info.Count-1].ToStringWithoutGeneratedPart(info.Count-1));
            Console.WriteLine("|_______||________|_______|_______||_______|_______|_________|_________");
            Console.WriteLine($"Min:\nX = {((_max + _min) / 2).ToString("F5")} +- {_delta.ToString("F3")}\nF(x) = {Function((_max + _min) / 2).ToString("F5")}");
        }
    }
}
