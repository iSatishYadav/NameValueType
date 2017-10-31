namespace NameValueType.Core
{
    public class NameValueType
    {

        public NameValueType(string name, string value, string type)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
        public string Type { get; private set; }
    }
}
