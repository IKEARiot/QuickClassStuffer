using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{
    public class IntegerSequenceGenerator : GeneratorBase, IDataGenerator
    {
        private int _sequence;

        private int SequenceStart { get; set; }
        private int SequenceEnd { get; set; }

        public IntegerSequenceGenerator()
        {
            SequenceStart = 0;
            SequenceEnd = int.MaxValue;
            _sequence = SequenceStart;
        }

        public IntegerSequenceGenerator(int sequenceStart, int sequenceEnd)
        {
            this.SequenceStart = sequenceStart;
            this.SequenceEnd = sequenceEnd;
            _sequence = SequenceStart;
        }
        

        #region IDataGenerator Members

        public object SelectData()
        {
            int result;

            result = _sequence;

            if (_sequence == SequenceEnd) 
            {                
                _sequence = SequenceStart;
            }
            else
            {
                _sequence++;  
            }
            return result;
        }

        #endregion
    }
}
