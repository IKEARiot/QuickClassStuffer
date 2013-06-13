using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class IntegerRangeGenerator : RandomGenerator, IDataGenerator
    {
        public int Lower { get; set; }
        public int Upper { get; set; }

        public IntegerRangeGenerator(int lower, int upper)
        {
            this.Lower = lower;
            this.Upper = upper;
        }
        public int SelectRandom(int lower, int upper)
        {
            return ImSoRandom.Next(lower, upper);
        }       

        #region IRandomGenerator Members

        public object SelectData()
        {
            return ImSoRandom.Next(this.Lower, this.Upper);
        }

        #endregion
    }
}
