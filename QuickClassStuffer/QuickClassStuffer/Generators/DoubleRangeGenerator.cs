using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
  
    public class DoubleRangeGenerator : GeneratorBase, IRandomGenerator
    {

        public double Lower { get; set; }
        public double Upper { get; set; }

        public DoubleRangeGenerator(double lower, double upper)
        {
            this.Upper = upper;
            this.Lower = lower;
        }

        #region IRandomGenerator Members

        public object SelectRandom()
        {
            return ImSoRandom.NextDouble() * (Upper-Lower) +  Lower;
        }

        #endregion
    }
  
}
