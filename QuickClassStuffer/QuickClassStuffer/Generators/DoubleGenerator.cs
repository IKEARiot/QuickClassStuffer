using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class DoubleGenerator : RandomGenerator, IDataGenerator
    {
       
        #region IRandomGenerator Members

        public object SelectData()
        {
            return ImSoRandom.NextDouble();
        }

        #endregion
    }
}
