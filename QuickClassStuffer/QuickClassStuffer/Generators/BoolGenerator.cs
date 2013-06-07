using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class BoolGenerator : GeneratorBase, IRandomGenerator
    {
        
        #region IRandomGenerator Members

        public object SelectRandom()
        {
            if (ImSoRandom.Next(0, 2) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
