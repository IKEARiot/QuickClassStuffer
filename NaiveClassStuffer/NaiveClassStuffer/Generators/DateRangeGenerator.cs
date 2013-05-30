using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveClassStuffer.Generators
{    
    public class DateRangeGenerator : GeneratorBase, IRandomGenerator
    {
        private DateTime Lower { get; set; }
        private DateTime Upper { get; set; }

        private enum TimeComponent
        {
            hours = 0,
            minutes = 1,
            seconds = 2,
            milliseconds = 3
        }

        public DateRangeGenerator(string lower, string upper)
        {
            this.Lower = DateTime.Parse(lower);
            this.Upper = DateTime.Parse(upper);
        }

        public DateRangeGenerator(DateTime lower, DateTime upper)
        {
            this.Lower = lower;
            this.Upper = upper;
        }
               
        #region IRandomGenerator Members

        public object SelectRandom()
        {
            TimeSpan timeSpan = Upper.Subtract(Lower);
            DateTime result = Lower.AddDays(ImSoRandom.Next(0, timeSpan.Days + 1));           
            result = result.AddHours(GetRandomTime(TimeComponent.hours));
            result = result.AddMinutes(GetRandomTime(TimeComponent.minutes));
            result = result.AddSeconds(GetRandomTime(TimeComponent.seconds));
            result = result.AddMilliseconds(GetRandomTime(TimeComponent.milliseconds));
            return result;
        }
        #endregion

        private double GetRandomTime(TimeComponent thisTimeComponent)
        {
            double result = 0;

            switch (thisTimeComponent)
            {
                case TimeComponent.hours:
                    result = ImSoRandom.Next(0, 24);
                    break;
                case TimeComponent.minutes:
                    result = ImSoRandom.Next(0, 60);
                    break;
                case TimeComponent.seconds:
                    result = ImSoRandom.Next(0, 60);
                    break;
                case TimeComponent.milliseconds:
                    result = ImSoRandom.Next(0, 1000);
                    break;
                default:
                    break;
            }

            return result;
        }
       
    }
}
