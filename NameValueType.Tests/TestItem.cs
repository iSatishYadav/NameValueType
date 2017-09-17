using System;
using System.Collections.Generic;

namespace NameValueType.Tests
{
    public class TestItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] StringArray { get; set; }
        public string[] AnotherStringArray { get; set; }

        public int Count { get; set; }

        public int[] AnotherIds { get; set; }

        public Guid[] Guids { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as TestItem;
            return item != null &&
                   Id.Equals(item.Id) &&
                   Name == item.Name &&
                   EqualityComparer<string[]>.Default.Equals(StringArray, item.StringArray) &&
                   EqualityComparer<string[]>.Default.Equals(AnotherStringArray, item.AnotherStringArray) &&
                   Count == item.Count &&
                   EqualityComparer<int[]>.Default.Equals(AnotherIds, item.AnotherIds) &&
                   EqualityComparer<Guid[]>.Default.Equals(Guids, item.Guids);
        }

        public override int GetHashCode()
        {
            var hashCode = -1575855290;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(StringArray);
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(AnotherStringArray);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(AnotherIds);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid[]>.Default.GetHashCode(Guids);
            return hashCode;
        }

        public static bool operator ==(TestItem application1, TestItem application2)
        {
            return EqualityComparer<TestItem>.Default.Equals(application1, application2);
        }

        public static bool operator !=(TestItem application1, TestItem application2)
        {
            return !(application1 == application2);
        }
    }
}
