using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class IntegerGenerator : RandomGenerator, IDataGenerator
    {        
        #region IRandomGenerator Members

        public object SelectData()
        {
            return ImSoRandom.Next(int.MinValue, int.MaxValue);
        }

        #endregion
    }
}
