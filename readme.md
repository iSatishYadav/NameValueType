# Create strongly typed .NET objects from ````IEnumerable<NameValueType>```` using Reflection.

[![Build Status](https://travis-ci.org/iSatishYadav/NameValueType.svg?branch=master)](https://travis-ci.org/iSatishYadav/NameValueType)

To create a strongly typed .NET objecta you need 3 information about an object.
1. What are the property names of that object?
2. What are the types of those properties?
3. What are the values of those properties.


To capture these values, the class ````NameTypeValue```` is created.
### Quick Start
e.g. Let's say you have a class Customer:

```` C#
public class Customer
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public int Age { get; set; }
}
````

And you need to create a new ````Customer```` dynamically.

#### 1. Create an ````IEnumerable<NameValueType>````

```` C#
var customerNameValueInfo = new List<NameValueType>
{
	new NameValueType(namof(Customer.Id), Guid.NewGuid(), typeof(Guid)),
	new NameValueType(namof(Customer.Name), "Satish", typeof(string)),
	new NameValueType(namof(Customer.Age), 25, typeof(int)),
}
````

#### 2. Use ````ToTypedObject<T>```` extension method.

```` C#
var customer = customerNameValueInfo.ToTypedObject<Customer>();
````

### Advance Properties

Creating objects with Array type properties.

So, this:

#### Usage
```` C#
var testItem = testItemNameValueTypeInfo.ToTypedObject<TestItem>();
````

will transform:

#### ````NameValueType```` info
```` C#

var testItemNameValueTypeInfo = new List<NameValueType>
{
	new NameValueType(nameof(TestItem.Id), app.Id.ToString(), typeof(Guid).FullName),

	new NameValueType(nameof(TestItem.Name), app.Name, typeof(string).FullName),

	new NameValueType(nameof(TestItem.Count), app.Count.ToString(), typeof(int).FullName),

	new NameValueType(nameof(TestItem.StringArray), app.StringArray[0], typeof(string).FullName),
	new NameValueType(nameof(TestItem.StringArray), app.StringArray[1], typeof(string).FullName),
	new NameValueType(nameof(TestItem.Guids), app.Guids[0].ToString(), typeof(Guid).FullName),
	new NameValueType(nameof(TestItem.Guids), app.Guids[1].ToString(), typeof(Guid).FullName),

	new NameValueType(nameof(TestItem.AnotherStringArray), app.AnotherStringArray[0], typeof(string).FullName),
	new NameValueType(nameof(TestItem.AnotherStringArray), app.AnotherStringArray[1], typeof(string).FullName),

	new NameValueType(nameof(TestItem.AnotherIds), app.AnotherIds[0].ToString(), typeof(int).FullName),
	new NameValueType(nameof(TestItem.AnotherIds), app.AnotherIds[1].ToString(), typeof(int).FullName),
	new NameValueType(nameof(TestItem.AnotherIds), app.AnotherIds[2].ToString(), typeof(int).FullName),
	new NameValueType(nameof(TestItem.AnotherIds), app.AnotherIds[3].ToString(), typeof(int).FullName),

};
````

into:

#### Expected Object

```` C#

var testItem = new TestItem
{
	Id = Guid.NewGuid(),
	Name = "App2",
	Count = 1,
	StringArray = new string[] { "Hello", "World" },
	Guids = new Guid[] { Guid.NewGuid(), Guid.NewGuid() },                
	AnotherIds = new int[] { 1, 2, 3, 4 }
};
````
