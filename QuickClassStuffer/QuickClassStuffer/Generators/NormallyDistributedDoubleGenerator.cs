using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class NormallyDistributedDoubleGenerator : RandomGenerator, IDataGenerator
    {
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }

        public NormallyDistributedDoubleGenerator(double mean, double standardDeviation)
        {
            this.Mean = mean;
            this.StandardDeviation = standardDeviation;
        }

        #region IRandomGenerator Members

        public object SelectData()
        {
            return GetGaussianDouble();
        }

        #endregion
        
        public double GetGaussianDouble()
        {
            // http://stackoverflow.com/questions/218060/random-gaussian-variables
            // Box-Muller transform
            double u1 = ImSoRandom.NextDouble(); 
            double u2 = ImSoRandom.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); 
            double randNormal = this.Mean + this.StandardDeviation * randStdNormal;
            return randNormal;
        }
    }
}
