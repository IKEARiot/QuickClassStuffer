using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public abstract class GeneratorBase : System.Attribute
    {        
    }

    public abstract class RandomGenerator : GeneratorBase
    {
        protected static readonly Random ImSoRandom = new Random();
    }
}
