using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{

    public class StringRangeGenerator : RandomGenerator, IDataGenerator
    {
        public string[] Set { get; set; }

        public StringRangeGenerator(string[] set)
        {
            this.Set = set;
        }       

        #region IRandomGenerator Members

        public object SelectData()
        {
            var thisNumber = ImSoRandom.Next(0, Set.Count());
            return Set.ElementAt(thisNumber);
        }

        #endregion
    }
}
