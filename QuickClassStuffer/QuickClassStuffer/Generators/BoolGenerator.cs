using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class BoolGenerator : RandomGenerator, IDataGenerator
    {
        
        #region IRandomGenerator Members

        public object SelectData()
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
