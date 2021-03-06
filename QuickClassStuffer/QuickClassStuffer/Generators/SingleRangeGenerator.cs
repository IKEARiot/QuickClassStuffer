﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class SingleRangeGenerator : RandomGenerator, IDataGenerator
    {

        public Single Lower { get; set; }
        public Single Upper { get; set; }

        public SingleRangeGenerator(Single lower, Single upper)
        {
            this.Upper = upper;
            this.Lower = lower;
        }

        #region IRandomGenerator Members

        public object SelectData()
        {
            return (Single) ImSoRandom.NextDouble() * (Upper - Lower) + Lower;
        }

        #endregion
    }
}
