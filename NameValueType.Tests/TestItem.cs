using System;
using System.Collections.Generic;

namespace NameValueType.Tests
{
    public class TestItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] Keywords { get; set; }
        public string[] IpAddresses { get; set; }

        public int Count { get; set; }

        public int[] AnotherIds { get; set; }

        public Guid[] Credentials { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as TestItem;
            return item != null &&
                   Id.Equals(item.Id) &&
                   Name == item.Name &&
                   EqualityComparer<string[]>.Default.Equals(Keywords, item.Keywords) &&
                   EqualityComparer<string[]>.Default.Equals(IpAddresses, item.IpAddresses) &&
                   Count == item.Count &&
                   EqualityComparer<int[]>.Default.Equals(AnotherIds, item.AnotherIds) &&
                   EqualityComparer<Guid[]>.Default.Equals(Credentials, item.Credentials);
        }

        public override int GetHashCode()
        {
            var hashCode = -1575855290;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(Keywords);
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(IpAddresses);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(AnotherIds);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid[]>.Default.GetHashCode(Credentials);
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
