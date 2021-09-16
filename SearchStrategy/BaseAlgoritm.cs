using System;
using System.Collections.Generic;
using System.Text;

namespace SearchStrategy
{
    abstract class BaseAlgoritm
    {
        public abstract void Start();
        public abstract void SetMainFunction(Func<double, double> function);
        public abstract void ShowProcess();

    }
}
