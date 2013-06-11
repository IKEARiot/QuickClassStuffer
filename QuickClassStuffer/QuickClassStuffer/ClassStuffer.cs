﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using QuickClassStuffer.Generators;

namespace QuickClassStuffer
{
    /// <summary>
    /// Given a class with default get/set properties, populate an object with random values.
    /// </summary>
    public class ClassStuffer
    {

        static string[] _generators = { "IntegerGenerator", "SingleGenerator", "SingleRangeGenerator", 
                                          "DoubleGenerator", "DoubleRangeGenerator", "BoolGenerator", 
                                          "IntegerRangeGenerator", "StringRangeGenerator", "DateRangeGenerator", 
                                          "StringFromFileGenerator", "NormallyDistributedDoubleGenerator" };

        // Some generators should be stored in a cache to avoid repeated processing of the source sample space i.e. a file
        IDictionary<string, IRandomGenerator> cacheGenerators = new Dictionary<string, IRandomGenerator>();

        /// <summary>
        /// Returns an array of with randomised data of type T where T is a decorated POCO 
        /// </summary>
        /// <typeparam name="T">Any class with properties decorated with a Generator</typeparam>
        /// <param name="count">The number of objects to be created</param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> StuffClass<T>(int count)
        {
            Type thisType = typeof(T);
            var properties = thisType.GetProperties();
            var results = new List<T>();

            for (int i = 0; i < count; i++)
            {
                // for each element, generate random data
                results.Add(GenerateRow<T>(thisType, properties));
            }
            return results;
        }

        /// <summary>
        /// Populates the designated field of a pre-existing enumeration of decorated POCO objects
        /// </summary>
        /// <typeparam name="T">Any class with properties decorated with a Generator</typeparam>
        /// <param name="propertyName">The name of the property to be filled</param>
        /// <param name="items">An existing array of decorated POCOs</param>
        public void StuffProperty<T>(string propertyName, IEnumerable<T> items)
        {
            Type thisType = typeof(T);
            var properties = thisType.GetProperties();
            var field = properties.Where(p => p.Name == propertyName).First();

            if (field != null)
            {
                var attribType = field.CustomAttributes.FirstOrDefault();
                // get the assigned generator
                var thisGenerator = GetGenerator(attribType, field);

                foreach (var item in items)
                {
                    // create instance
                    var result = (T) item;                    
                    //assign value
                    field.SetValue(result, thisGenerator.SelectRandom());
                }                              
            }    
        }

        /// <summary>
        /// Populates the designated field of a pre-existing decorated POCO object
        /// </summary>
        /// <typeparam name="T">Any class with properties decorated with a Generator</typeparam>
        /// <param name="propertyName">The name of the property to be filled</param>
        /// <param name="item">A decorated POCO</param>
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

        /// <summary>
        /// A factory for returning the appropriate generator
        /// </summary>
        /// <param name="thisCustomAttribute">The decoration defining the generator</param>
        /// <param name="thisPropInfo">The reflected values of the property</param>
        /// <returns>IRandomGenerator</returns>
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
                    // this is a cached generator, stored for the sake of not having to re-process the file
                    if (cacheGenerators.ContainsKey(pathAsKey))
                    {
                        generator = cacheGenerators[pathAsKey];
                    }
                    else
                    {
                        // process the gerneator
                        generator = GetPropertyInfoAttribute<StringFromFileGenerator>(thisPropInfo);
                        // add to a cache
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
             
        /// <summary>
        /// Returns a row of randomised data for an object of type T
        /// </summary>
        /// <typeparam name="T">Any class with properties decorated with a Generator</typeparam>
        /// <param name="thisType">Type information relating to POCO</param>
        /// <param name="thisPropInfo">Array of POCO's properties</param>
        /// <returns>T</returns>
        private T GenerateRow<T>(Type thisType, PropertyInfo[] thisPropInfo)
        {
            // create an instance of the relevant type
            var result = (T)Activator.CreateInstance(thisType);
            
            foreach (var item in thisPropInfo)
            {
                string fullname = item.PropertyType.Name;
                // we need to get the annotation that relates to our generators. Prob a better way to do this 
                var attribType = item.CustomAttributes.Where(ca=> _generators.Contains(ca.AttributeType.Name)).First();
                // if we found the annotation
                if (attribType != null)
                {
                    // get generator
                    var thisGenerator = GetGenerator(attribType, item);
                    // set value
                    item.SetValue(result, thisGenerator.SelectRandom());
                }
            }
            return result;
        }

        /// <summary>
        /// Returns an instance of a generator.
        /// </summary>
        /// <typeparam name="T">Type of generator required</typeparam>
        /// <param name="propInfo">The reflected values of the property</param>
        /// <returns>T</returns>
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
