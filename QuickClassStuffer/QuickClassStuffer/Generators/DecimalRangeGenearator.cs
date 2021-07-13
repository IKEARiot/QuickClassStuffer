using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
   
    public class DecimalRangeGenerator : RandomGenerator, IDataGenerator
    {

        public double Lower { get; set; }
        public double Upper { get; set; }

        // decimal cannot be used as it is not a primitive data type
        // https://stackoverflow.com/questions/3192833/why-decimal-is-not-a-valid-attribute-parameter-type
        public DecimalRangeGenerator(double lower, double upper) 
        {
            this.Upper = upper;
            this.Lower = lower;
        }

        #region IRandomGenerator Members

        public object SelectData()
        {
            return (decimal) (ImSoRandom.NextDouble() * (Upper - Lower) + Lower);
        }

        #endregion
    }
}
