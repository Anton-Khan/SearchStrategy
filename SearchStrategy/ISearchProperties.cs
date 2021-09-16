using System;

namespace SearchStrategy
{
    interface ISearchProperties
    {
        public double Min
        {
            get;
        }
        public double Max
        {
            get;
        }
        public double E
        {
            get;
        }
        public Func<double, double> Function
        {
            get;
        }
    }
}
