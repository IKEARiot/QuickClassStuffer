using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickClassStuffer.Generators
{   
    public class StringFromFileGenerator : GeneratorBase, IRandomGenerator
    {
        public string Path { get; set; }

        public IList<String> Strings { get; set; }

        private void ReadFile()
        {
            string currentWord = "";
            using (var thisReader = new System.IO.StreamReader(Path))
            {
                while ((currentWord = thisReader.ReadLine()) != null)
                {
                    Strings.Add(currentWord);
                }
            }
        }

        public StringFromFileGenerator(string path)
        {
            this.Path = path;
            this.Strings = new List<String>();
            ReadFile();
        }  

        #region IRandomGenerator Members

        public object SelectRandom()
        {
            return Strings.ElementAt(ImSoRandom.Next(0, Strings.Count()));
        }

        #endregion
    }
}
