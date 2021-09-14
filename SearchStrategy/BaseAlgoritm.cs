using System;
using System.Collections.Generic;
using System.Text;

namespace SearchStrategy
{
    abstract class BaseAlgoritm
    {
        public abstract void Start(double min, double max, double e);
        public abstract void SetMainFunction(Func<double, double> function);

    }
}
