﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickClassStuffer;
using QuickClassStuffer.Generators;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace ClassStufferUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanInitialiseGenerator()
        {
            var myGenerator = new QuickClassStuffer.ClassStuffer();           
            Assert.IsTrue(myGenerator != null);            
        }
        
        [TestMethod]
        public void CanPopulateWithData()
        {
            var myGenerator = new QuickClassStuffer.ClassStuffer();
            var results = myGenerator.StuffClass<ExampleClass>(1000);
            
            Assert.IsTrue(results.Count() == 1000);

            string[] wordsInFile = { "keep", "on", "rocking", "in", "the", "free", "world" };
            Assert.IsTrue(wordsInFile.Contains(results.First().AStringFromFile));

            string[] wordSet = { "One", "Two", "Three", "Four" };
            Assert.IsTrue(wordSet.Contains(results.First().AStringFromADefinedSet));

            // take 50 of the integers, select any with a non default value
            var someIntegers = results.Select(r => r.AnInteger).Take(50).Where(i => i != 0);
            Assert.IsTrue(someIntegers.Count() > 0);

            var someSingles = results.Select(r => r.ASingle).Take(50).Where(s => s != 0);
            Assert.IsTrue(someSingles.Count() > 0);

            var someDoubles = results.Select(r => r.ADouble).Take(50).Where(d => d != 0);
            Assert.IsTrue(someDoubles.Count() > 0);

            var someIntsFromRange = results.Select(r => r.AnIntegerFromAGivenRange).Take(50).Where(i => i < 10 || i > 20);
            Assert.IsTrue(someIntsFromRange.Count() == 0);

            var someDates = results.Select(r => r.ADateBetweenTwoDates).Take(50).Where(d => d < DateTime.Parse("2013-01-01") || d >= DateTime.Parse("2013-01-11"));
            Assert.IsTrue(someDates.Count() == 0);
            
            var someDoubleInRange =  results.Select(r => r.ADoubleInaRange).Take(50).Where(d => d < -3.2 || d > 3.5);
            Assert.IsTrue(someDoubleInRange.Count() == 0);

            var someSingleInRange =  results.Select(r => r.ASingleInaRange).Take(50).Where(d => d < -10 || d > 10);
            Assert.IsTrue(someSingleInRange.Count() == 0);

            var someBools =  results.Select(r => r.ABoolean).Take(50).Where(d => d == true);
            Assert.IsTrue(someSingleInRange.Count() != 50);
                                
            var someNormals = results.Select(r => r.ANormallyDistributedDoobery).Take(50).Where(i => i != 0);
            Assert.IsTrue(someNormals.Count() > 0);

            var someSequence = results.Select(r => r.AnIntegerSequence1).Take(50).Where(i => i == 0);
            Assert.IsTrue(someSequence.Count() < 50);
        }

        [TestMethod]
        public void CanPopulateSpecificFieldInExistingArray()
        {
            var results = new List<ExampleClass>();

            results.Add(new ExampleClass());
            results.Add(new ExampleClass());
            results.Add(new ExampleClass());
            results.Add(new ExampleClass());

            var myGenerator = new QuickClassStuffer.ClassStuffer();

            var stringSnapshot = results.Select(r => r.AStringFromFile).ToList();
            myGenerator.StuffProperty<ExampleClass>(z=>z.AStringFromFile, results);
            
            stringSnapshot.AddRange(results.Select(r => r.AStringFromFile).ToList());

            Assert.IsTrue(stringSnapshot.Count() == 8);
            var resultSnapshot = stringSnapshot.Where(s => s != null);
            Assert.IsTrue(resultSnapshot.Count() == 4);    
        }

        [TestMethod]
        public void CanPopulateSpecificFieldInExistingObject()
        {
            var myObject = new ExampleClass();
            var observe1 = myObject.AStringFromFile;
            var myGenerator = new QuickClassStuffer.ClassStuffer();                        
            myGenerator.StuffProperty<ExampleClass>(z => z.AStringFromFile, myObject);
            var observe2 = myObject.AStringFromFile;
            Assert.IsTrue(observe1 != observe2);
        }
              
    }
    
    public class ExampleClass
    {
     
        [StringFromFileGenerator(@"D:\Users\gcase.ONEFUTURE\Documents\GitHub\QuickClassStuffer\QuickClassStuffer\ClassStufferUnitTest\TestFile1.txt")]
        public string AStringFromFile { get; set; }

        [IntegerGenerator]
        public int AnInteger { get; set; }

        [IntegerRangeGenerator(10, 20)]
        public int AnIntegerFromAGivenRange { get; set; }

        [DateRangeGenerator("2013-01-01", "2013-01-10")]
        public DateTime ADateBetweenTwoDates { get; set; }

        [StringRangeGenerator(new string[] { "One", "Two", "Three", "Four" })]
        public string AStringFromADefinedSet { get; set; }

        [SingleGenerator]
        public Single ASingle { get; set; }

        [DoubleGenerator]
        public Double ADouble { get; set; }

        [DoubleRangeGenerator(-3.2, 3.5)]
        public Double ADoubleInaRange { get; set; }

        [SingleRangeGenerator(-10, 10)]
        public Single ASingleInaRange { get; set; }

        [BoolGenerator]
        public Boolean ABoolean { get; set; }

        [NormallyDistributedDoubleGenerator(2.5, 0.03)]
        public double ANormallyDistributedDoobery { get; set; }

        [IntegerSequenceGenerator(0, 3)]
        public int AnIntegerSequence1 { get; set; }

        [IntegerSequenceGenerator(-3, 3)]
        public int AnIntegerSequence2 { get; set; }

        [IntegerSequenceGenerator()]
        public int AnIntegerSequence3 { get; set; }


        [DecimalGenerator]
        public decimal ADecimal { get; set; }

        [DecimalRangeGenerator(-3.2, 3.5)]
        public decimal ADecimalInARange { get; set; }

    }
}
