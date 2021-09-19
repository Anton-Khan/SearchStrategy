using System;
using System.Collections.Generic;

namespace SearchStrategy.AlgorithmTypes
{
    class SimulatedAnnealing : BaseAlgoritm
    {
        private double _max;
        private double _min;
        private Func<double, double> Function;

        private double _Tmax;
        private double _Tmin;
        private List<AnnealingInfo> _annealingData;
        
        public SimulatedAnnealing(ISearchProperties properties)
        {
            _max = properties.Max;
            _min = properties.Min;
            Function = properties.Function;

            _Tmax = 10000;
            _Tmin = 0.1;
        }
        public override void SetMainFunction(Func<double, double> function)
        {
            Function = function;
        }

        private double GetRandomPointAt(double min, double max, Random rand)
        {
            return min + rand.NextDouble() * (max - min);
        }

        private double Probability(double delta, double T)
        {
            return Math.Exp(-delta/T);
        }

        private bool IsTransferedByP(double P, Random rand)
        {
            return rand.NextDouble() <= P;
        }

        private double ChangeTemperature(double T)
        {
            return T * 0.95;
        }

        public override void Start()
        {
            _annealingData = new List<AnnealingInfo>();
            Random rand = new Random();
            double T = _Tmax;

            double bestPoint = GetRandomPointAt(_min, _max, rand);
            double bestFunc = Function(bestPoint);
            _annealingData.Add(new AnnealingInfo(T, bestPoint, bestFunc, 1, true, bestPoint, bestFunc));

            while (T >= _Tmin)
            {
                double point = GetRandomPointAt(_min, _max, rand);
                double func = Function(point);

                double delta = func - bestFunc;

                double P = Probability(delta, T);
                bool IsTransfered = IsTransferedByP(P, rand);

                if (delta  <= 0 || IsTransfered)
                {
                    bestPoint = point;
                    bestFunc = func;
                    _annealingData.Add(new AnnealingInfo(T, point, func, P>1? 1 : P, IsTransfered, bestPoint, bestFunc));
                }else
                    _annealingData.Add(new AnnealingInfo(T, point, func, P, IsTransfered, bestPoint, bestFunc));

                T = ChangeTemperature(T);
            }
        }
        public override void ShowProcess()
        {
            
            Console.WriteLine("+-------+----------+-------+---------+-------+-------+-------+---------+");
            Console.WriteLine("|  N    |    T     |   x   |    F(x) |   P   |accept?|x best |F(x) best");
            Console.WriteLine("+-------+----------+-------+---------+-------+-------+-------+---------+");
            for (int i = 0; i < _annealingData.Count; i++)
            {
                Console.WriteLine($"| {i+1}\t| {_annealingData[i].T.ToString("F2").PadRight(9)}| {_annealingData[i].x.ToString("F3").PadRight(5)} " +
                    $"| {_annealingData[i].Fx.ToString("F3").PadRight(7)} | {_annealingData[i].P.ToString("F3")} " +
                    $"| {(_annealingData[i].IsAccepted ? "  T  " : "  -  ")} | {_annealingData[i].x_best.ToString("F3").PadRight(5)} " +
                    $"| {_annealingData[i].fx_best.ToString("F3").PadRight(7)} |");
            }
            Console.WriteLine("+-------+----------+-------+---------+-------+-------+-------+---------+");

        }
        
    }


    class AnnealingInfo
    {
        public double T;
        public double x;
        public double Fx;
        public double P;
        public bool IsAccepted;

        public double x_best;
        public double fx_best;

        public AnnealingInfo(double t, double x, double fx, double p, bool isAccepted, double x_best, double fx_best)
        {
            T = t;
            this.x = x;
            Fx = fx;
            P = p;
            IsAccepted = isAccepted;
            this.x_best = x_best;
            this.fx_best = fx_best;
        }
    }
}
