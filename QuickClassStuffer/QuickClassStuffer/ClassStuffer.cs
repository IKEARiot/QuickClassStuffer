using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using QuickClassStuffer.Generators;

namespace QuickClassStuffer
{
    public class ClassStuffer
    {
        static string[] _generators = { "IntegerGenerator", "SingleGenerator", "SingleRangeGenerator", 
                                          "DoubleGenerator", "DoubleRangeGenerator", "BoolGenerator", 
                                          "IntegerRangeGenerator", "StringRangeGenerator", "DateRangeGenerator", 
                                          "StringFromFileGenerator", "NormallyDistributedDoubleGenerator" };
        IDictionary<string, IRandomGenerator> cacheGenerators = new Dictionary<string, IRandomGenerator>();

        public IEnumerable<T> StuffClass<T>(int count)
        {
            Type thisType = typeof(T);
            var properties = thisType.GetProperties();
            var results = new List<T>();

            for (int i = 0; i < count; i++)
            {
                results.Add(GenerateRow<T>(thisType, properties));
            }
            return results;
        }

        public void StuffProperty<T>(string propertyName, List<T> items)
        {
            Type thisType = typeof(T);
            var properties = thisType.GetProperties();
            var field = properties.Where(p => p.Name == propertyName).First();

            if (field != null)
            {
                var attribType = field.CustomAttributes.FirstOrDefault();
                var thisGenerator = GetGenerator(attribType, field);

                foreach (var item in items)
                {
                    var result = (T) item;                    
                    field.SetValue(result, thisGenerator.SelectRandom());
                }                              
            }    
        }

        public void StuffProperty<T>(string propertyName, T item)
        {
            Type thisType = typeof(T);
            var properties = thisType.GetProperties();
            var field = properties.Where(p => p.Name == propertyName).First();

            if (field != null)
            {
                var attribType = field.CustomAttributes.FirstOrDefault();
                var thisGenerator = GetGenerator(attribType, field);                
                var result = (T)item;
                field.SetValue(result, thisGenerator.SelectRandom());                
            }
        }

        private IRandomGenerator GetGenerator(CustomAttributeData thisCustomAttribute, PropertyInfo thisPropInfo)
        {
            IRandomGenerator generator = null;

            switch (thisCustomAttribute.AttributeType.Name)
            {
                case "IntegerGenerator":
                    generator = new IntegerGenerator();
                    break;
                case "SingleGenerator":
                    generator = new SingleGenerator();
                    break;
                case "SingleRangeGenerator":
                    generator = GetPropertyInfoAttribute<SingleRangeGenerator>(thisPropInfo);
                    if (generator == null)
                    {
                        generator = new DoubleRangeGenerator(Single.MinValue, Single.MaxValue);
                    }
                    break;
                case "DoubleGenerator":
                    generator = new DoubleGenerator();
                    break;
                case "NormallyDistributedDoubleGenerator":
                    generator = GetPropertyInfoAttribute<NormallyDistributedDoubleGenerator>(thisPropInfo);
                    if (generator == null)
                    {
                        generator = new NormallyDistributedDoubleGenerator(0, 1);
                    }
                    break;
                case "DoubleRangeGenerator":
                    generator = GetPropertyInfoAttribute<DoubleRangeGenerator>(thisPropInfo);
                    if (generator == null)
                    {
                        generator = new DoubleRangeGenerator(double.MinValue, double.MaxValue);
                    }
                    break;
                case "BoolGenerator":
                    generator = new BoolGenerator();
                    break;
                case "IntegerRangeGenerator":
                    generator = GetPropertyInfoAttribute<IntegerRangeGenerator>(thisPropInfo);
                    if (generator == null)
                    {
                        generator = new IntegerRangeGenerator(int.MinValue, int.MaxValue);
                    }
                    break;
                case "StringRangeGenerator":
                    generator = GetPropertyInfoAttribute<StringRangeGenerator>(thisPropInfo);
                    if (generator == null)
                    {
                        generator = new StringRangeGenerator(new string[] { "" });
                    }
                    break;
                case "DateRangeGenerator":
                    generator = GetPropertyInfoAttribute<DateRangeGenerator>(thisPropInfo);
                    if (generator == null)
                    {
                        generator = new DateRangeGenerator(DateTime.MinValue, DateTime.MaxValue);
                    }
                    break;
                case "StringFromFileGenerator":
                    string pathAsKey = thisCustomAttribute.ConstructorArguments.First().ToString();
                    if (cacheGenerators.ContainsKey(pathAsKey))
                    {
                        generator = cacheGenerators[pathAsKey];
                    }
                    else
                    {
                        generator = GetPropertyInfoAttribute<StringFromFileGenerator>(thisPropInfo);
                        cacheGenerators.Add(pathAsKey, generator);
                    }
                    if (generator == null)
                    {
                        generator = new StringRangeGenerator(new string[] { "" });
                    }
                    break;
                default:
                    break;
            }

            return generator;
        }
             

        private T GenerateRow<T>(Type thisType, PropertyInfo[] thisPropInfo)
        {
            var result = (T)Activator.CreateInstance(thisType);

            foreach (var item in thisPropInfo)
            {
                string fullname = item.PropertyType.Name;
                var attribType = item.CustomAttributes.Where(ca=> _generators.Contains(ca.AttributeType.Name)).First();

                if (attribType != null)
                {
                    var thisGenerator = GetGenerator(attribType, item);
                    item.SetValue(result, thisGenerator.SelectRandom());
                }
            }
            return result;
        }

        private static T GetPropertyInfoAttribute<T>(PropertyInfo propInfo)
        {
            if (propInfo.CustomAttributes.Count() > 0)
            {
                var myAttrib = propInfo.GetCustomAttributes(typeof(T), false).Cast<T>().Single();
                return myAttrib;
            }
            else
            {
                return default(T);
            }
        }


       
    }
}
