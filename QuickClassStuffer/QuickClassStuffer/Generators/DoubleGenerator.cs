using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{    
    public class DoubleGenerator : GeneratorBase, IRandomGenerator
    {
       
        #region IRandomGenerator Members

        public object SelectRandom()
        {
            return ImSoRandom.NextDouble();
        }

        #endregion
    }
}
