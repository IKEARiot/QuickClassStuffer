using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveClassStuffer.Generators
{

    public class StringRangeGenerator : GeneratorBase, IRandomGenerator
    {
        public string[] Set { get; set; }

        public StringRangeGenerator(string[] set)
        {
            this.Set = set;
        }       

        #region IRandomGenerator Members

        public object SelectRandom()
        {
            var thisNumber = ImSoRandom.Next(0, Set.Count());
            return Set.ElementAt(thisNumber);
        }

        #endregion
    }
}
