using System;
using System.Collections.Generic;

namespace SearchStrategy
{
    public enum Algoritms
    {
        Passive,
        Dichotomy
    }
    class SearchAlgoritm
    {
        private double _min = 0;
        private double _max = 0;
        private double _E = 0;
        private Func<double, double> _func;
        private BaseAlgoritm _algoritm;

        public SearchAlgoritm(double min, double max, double e, Func<double, double> function)
        {
            _min = min;
            _max = max;
            _E = e;
            _func = function;
        }

        public void SetAlgoritmType(Algoritms al)
        {
            switch (al)
            {
                case Algoritms.Passive: 
                    _algoritm = new OptimalPassiveSearch();
                    _algoritm.SetMainFunction(_func);
                    break;
                case Algoritms.Dichotomy:
                    _algoritm = new Dichotomy();
                    _algoritm.SetMainFunction(_func);
                    break;
            }
        }

        public void Search()
        {
            _algoritm.Start(_min, _max, _E);
        }
        public void ShowProcess()
        {
            _algoritm.ShowProcess();
        }
    }
}
