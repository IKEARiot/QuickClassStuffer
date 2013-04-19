Naive Class Stuffer - Object population with randomised data for .Net
========================================

Features
--------
NCS is intended to be used by a developer/tester looking to create early-stage, meaningful test data for classes. It's primary goal is to be quick and painless.


Create a class and decorate the properties with directives
----------------------------------------------------------

```csharp
public class TestClass1
{
	[StringFromFileGenerator(@"c:\TestFile1.txt")]
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
}
```
Generate objects using the NCS populated with randomised data
-------------------------------------------------------------
Example usage:

```csharp
var myGenerator = new NaiveClassStuffer.ClassStuffer();
var results = myGenerator.StuffClass<TestClass1>(10);
```

Take an existing decorated class and populate specific fields within the objects
--------------------------------------------------------------------------------

```csharp
var myGenerator = new NaiveClassStuffer.ClassStuffer();
var results = new List<TestClass1>();

results.Add(new TestClass1());
results.Add(new TestClass1());
results.Add(new TestClass1());
results.Add(new TestClass1());
// D being a property name
myGenerator.StuffProperty<TestClass1>("D", results);
myGenerator.StuffProperty<TestClass1>("A", results);
myGenerator.StuffProperty<TestClass1>("D", results.First());
```


Method
------
The program uses reflection to discover object properties. Instances of classes are created and married up to the correct generator.


Limitations and caveats
-----------------------
Must use decorated classes for fairly obvious reasons; not true POCO

Author
------
Gareth Case