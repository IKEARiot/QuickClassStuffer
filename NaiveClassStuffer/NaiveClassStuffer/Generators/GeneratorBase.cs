using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveClassStuffer.Generators
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public abstract class GeneratorBase : System.Attribute
    {
        protected static readonly Random ImSoRandom = new Random();
    }
}
