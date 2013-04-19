using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaiveClassStuffer;
using NaiveClassStuffer.Generators;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace ClassStufferUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var myGenerator = new NaiveClassStuffer.ClassStuffer();
            var results = myGenerator.StuffClass<TestClass1>(10);
        }


        [TestMethod]
        public void TestMethod2()
        {
            var myGenerator = new NaiveClassStuffer.ClassStuffer();
            var results = new List<TestClass1>();

            results.Add(new TestClass1());
            results.Add(new TestClass1());
            results.Add(new TestClass1());
            results.Add(new TestClass1());

            myGenerator.StuffProperty<TestClass1>("D", results);
            myGenerator.StuffProperty<TestClass1>("A", results);
            myGenerator.StuffProperty<TestClass1>("D", results.First());
            Console.Write("kdjhfskfj");            
        }

        [TestMethod]
        public void TestMethod3()
        {
            var myGenerator = new NaiveClassStuffer.ClassStuffer();
            var results = myGenerator.StuffClass<TestClass1>(10);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var myGenerator = new NaiveClassStuffer.ClassStuffer();
            var results = myGenerator.StuffClass<TestClass1>(100);
        }
    }

    public class TestClass1
    {
        [StringFromFileGenerator(@"c:\users\gcase\documents\visual studio 2012\Projects\NaiveClassStuffer\ClassStufferUnitTest\TestFile1.txt")]
        public string D { get; set; }
        [IntegerGenerator]
        public int X { get; set; }
        [IntegerRangeGenerator(10, 20)]
        public int Y { get; set; }
        [DateRangeGenerator("2013-01-01", "2013-01-10")]
        public DateTime Z { get; set; }
        [StringRangeGenerator(new string[] { "One", "Two", "Three", "Four" })]
        public string A { get; set; }
        [FloatGenerator]
        public float B { get; set; }
        [DoubleGenerator]
        public Double C { get; set; }
        [BoolGenerator]
        public Boolean E { get; set; }
        

        public override string ToString()
        {
            var thisStringBuilder = new System.Text.StringBuilder();
            Type thisType = typeof(TestClass1);
            var properties = thisType.GetProperties();
            foreach (var item in properties)
            {
                thisStringBuilder.Append(item.Name + ":" + item.GetValue(this).ToString() + "   |   ");
            }
            return thisStringBuilder.ToString();
        }
    }


}
