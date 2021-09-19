using System;
using SearchStrategy.AlgorithmTypes;

namespace SearchStrategy
{
    public enum Algoritms
    {
        Passive,
        Dichotomy,
        RandomSearch,
        SimulatedAnnealing
    }
    class SearchAlgorithm : ISearchProperties
    {
        private BaseAlgoritm _algorithm;

        private double _min = 0;
        private double _max = 0;
        private double _E = 0;
        private Func<double, double> _func;
        private ISearchProperties _searchProperties;

        public double Min => _min;
        public double Max => _max;
        public double E => _E;
        public Func<double, double> Function => _func;

        public SearchAlgorithm(double min, double max, double e, Func<double, double> function)
        {
            _min = min;
            _max = max;
            _E = e;
            _func = function;
            _searchProperties = this;
        }

        public void SetAlgoritmType(Algoritms al)
        {
            switch (al)
            {
                case Algoritms.Passive: 
                    _algorithm = new OptimalPassiveSearch(_searchProperties);
                    break;
                case Algoritms.Dichotomy:
                    _algorithm = new Dichotomy(_searchProperties);
                    break;
                case Algoritms.RandomSearch:
                    _algorithm = new RandomSearch(_searchProperties);
                    break;
                case Algoritms.SimulatedAnnealing:
                    _algorithm = new SimulatedAnnealing(_searchProperties);
                    break;
            }
        }

        public void SetMainFunction(Func<double, double> function)
        {
            _algorithm.SetMainFunction(function);
        }
        
        public void Search()
        {
            _algorithm.Start();
        }
        public void ShowProcess()
        {
            _algorithm.ShowProcess();
        }
    }
}
