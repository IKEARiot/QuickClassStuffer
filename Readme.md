Quick Class Stuffer - Object population with randomised data for .Net
=====================================================================

Features
--------
QCS is intended to be used by a developer/tester looking to create early-stage, meaningful test data for classes. It's primary goal is to be quick and painless.


Create a class and decorate the properties with directives
----------------------------------------------------------

```csharp
public class ExampleClass
{
	[StringFromFileGenerator(@"C:\Users\gcase\Documents\GitHub\QuickClassStuffer\QuickClassStuffer\ClassStufferUnitTest\TestFile1.txt")]
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
	
}
```
Generate objects using the QCS populated with randomised data
-------------------------------------------------------------
Example usage:

```csharp
var myGenerator = new QuickClassStuffer.ClassStuffer();
var results = myGenerator.StuffClass<ExampleClass>(1000);
```

Take an existing decorated class and populate specific fields within the objects
--------------------------------------------------------------------------------

```csharp
var toBeStuffed = new List<ExampleClass>();

toBeStuffed.Add(new ExampleClass());
toBeStuffed.Add(new ExampleClass());
toBeStuffed.Add(new ExampleClass());
toBeStuffed.Add(new ExampleClass());

var myGenerator = new QuickClassStuffer.ClassStuffer();

myGenerator.StuffProperty<ExampleClass>("AStringFromFile", toBeStuffed);
```


Method
------
The program uses reflection to discover object properties. Instances of classes are created and married up to the correct generator.


Limitations and caveats
-----------------------
Must use decorated classes for fairly obvious reasons; not true POCO.

Author
------
Gareth Case