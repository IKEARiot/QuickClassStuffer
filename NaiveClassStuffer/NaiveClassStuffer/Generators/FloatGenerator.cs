using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveClassStuffer.Generators
{  
    public class FloatGenerator : GeneratorBase, IRandomGenerator
    {
       
        #region IRandomGenerator Members

        public object SelectRandom()
        {
            double mantissa = (ImSoRandom.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, ImSoRandom.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

        #endregion
    }
}
