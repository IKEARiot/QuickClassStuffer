using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class SingleGenerator : RandomGenerator, IDataGenerator
    {       
        #region IRandomGenerator Members

        public object SelectData()
        {
            double result = ImSoRandom.NextDouble();
            return (Single) result;            
        }

        #endregion
    }
}
