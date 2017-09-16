using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NameValueType
{
    public static class NameValueTypeExtensions
    {
        /// <summary>
        /// Converts set of claims into specified typed object. ONLY value types and array are supported.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameValueTypes"></param>
        /// <returns></returns>
        /// <remarks>Do not use until you're 100 % sure what this method does.</remarks>

        public static T ToTypedObject<T>(this IEnumerable<NameValueType> nameValueTypes) where T : new()
        {

            //new System.Security.Claims.NameValueType()
            var typeOfString = typeof(string);
            var typeOfGuid = typeof(Guid);
            var outputType = typeof(T);
            var outTypeObject = new T();
            var properties = outputType.GetProperties()
                .Join(nameValueTypes,
                prop => prop.Name,
                nvt => nvt.Name,
                (prop, nvt) => new
                {
                    NameValueType = nvt,
                    Prop = prop
                })
                .Where(x => x.NameValueType.Value != null)
                .GroupBy(x => x.NameValueType.Name)
                .Select(x => new { Name = x.Key, Prop = x.Select(y => y.Prop).First(), NameValueTypes = x.Select(c => c.NameValueType).ToList() })
                ;
            foreach (var property in properties)
            {
                var propertyType = property.Prop.PropertyType;
                var firstPropertyNameValueType = property.NameValueTypes.First();
                if (propertyType.IsArray)
                {
                    //TODO: create a new array if not exists, or add values to array if it does
                    var claimValueType = Type.GetType(firstPropertyNameValueType.Type);
                    for (int i = 0; i < property.NameValueTypes.Count; i++)
                    {
                        var arrayPropertyValue = property.Prop.GetValue(outTypeObject);

                        Array claimArray =
                            //(arrayPropertyValue == defaultPropertyValue)
                            i == 0
                            ? Array.CreateInstance(claimValueType, property.NameValueTypes.Count)
                            : (arrayPropertyValue as Array);

                        if (claimValueType == typeOfGuid)
                            claimArray.SetValue(Guid.Parse(property.NameValueTypes[i].Value), i);
                        else
                            claimArray.SetValue(Convert.ChangeType(property.NameValueTypes[i].Value, claimValueType), i);

                        property.Prop.SetValue(outTypeObject, claimArray);
                    }
                }
                else if (propertyType == typeOfGuid)
                {
                    property.Prop.SetValue(outTypeObject, Guid.Parse(firstPropertyNameValueType.Value));
                }
                else if (propertyType.IsAssignableFrom(typeof(IEnumerable)) && !propertyType.IsArray)
                {
                    throw new InvalidOperationException("Non array Enumerables are not supported.");
                }
                else
                {
                    property.Prop.SetValue(outTypeObject, Convert.ChangeType(firstPropertyNameValueType.Value, propertyType));
                }
            }
            return outTypeObject;
        }
    }
}
