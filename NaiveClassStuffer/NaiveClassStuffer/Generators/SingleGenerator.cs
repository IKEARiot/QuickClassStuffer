using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveClassStuffer.Generators
{  
    public class SingleGenerator : GeneratorBase, IRandomGenerator
    {       
        #region IRandomGenerator Members

        public object SelectRandom()
        {
            double result = ImSoRandom.NextDouble();
            return (Single) result;            
        }

        #endregion
    }
}
