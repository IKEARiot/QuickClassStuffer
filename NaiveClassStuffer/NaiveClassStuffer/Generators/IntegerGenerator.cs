using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveClassStuffer.Generators
{
    public class IntegerGenerator :GeneratorBase, IRandomGenerator
    {        

        #region IRandomGenerator Members

        public object SelectRandom()
        {
            return ImSoRandom.Next(int.MinValue, int.MaxValue);
        }

        #endregion
    }
}
