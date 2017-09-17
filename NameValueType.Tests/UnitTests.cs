using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NameValueType.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void ToTypedObject_Should_ReturnTypedObject_When_ObjectDoesNotHaveArrayProperties()
        {
            var app = new TestItem
            {
                Id = Guid.NewGuid(),
                Name = "Hello",
                Count = 1
            };
            var claims = new List<NameValueType>
            {
                new NameValueType(nameof(TestItem.Id), app.Id.ToString(), typeof(Guid).FullName),
                new NameValueType(nameof(TestItem.Name), app.Name, typeof(string).FullName),
                new NameValueType(nameof(TestItem.Count), app.Count.ToString(), typeof(int).FullName)
            };
            //Act
            var appNow = claims.ToTypedObject<TestItem>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    claims.ToTypedObject<Item>();
            //}

            //Assert
            AssertPerpertyWiseEqual(app, appNow);
        }

        [TestMethod]

        public void ToTypedObject_Should_ReturnTypedObject_When_ObjectHasStringAndGuidArrayProperties()
        {
            //Arrange
            var app = new TestItem
            {
                Id = Guid.NewGuid(),
                Name = "App2",
                Count = 1,
                StringArray = new string[] { "Hello", "World" },
                Guids = new Guid[] { Guid.NewGuid(), Guid.NewGuid() },
                AnotherStringArray = new string[] { "9.8.7.6", "1.2.3.4" },
                AnotherIds = new int[] { 1, 2, 3, 4 }
            };
            var claims = new List<NameValueType>
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

            //Act
            var appNow = claims.ToTypedObject<TestItem>();


            //Assert
            AssertPerpertyWiseEqual(app, appNow);
        }

        private static void AssertPerpertyWiseEqual(object expected, object actual)
        {
            foreach (var prop in actual.GetType().GetProperties())
            {
                if (!prop.PropertyType.IsArray)
                    Assert.AreEqual(prop.GetValue(expected), prop.GetValue(actual));
                else
                {
                    CollectionAssert.AreEqual(prop.GetValue(expected) as Array, prop.GetValue(actual) as Array);
                }
            }
        }
    }
}
