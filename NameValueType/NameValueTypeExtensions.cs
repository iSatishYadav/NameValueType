using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NameValueType.Core;

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

        public static T ToTypedObject<T>(this IEnumerable<Core.NameValueType> nameValueTypes) where T : new()
        {
            return Core.NameValueTypeExtensions.ToTypedObject<T>(nameValueTypes);
        }
    }
}
