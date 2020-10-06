using System;

namespace lights.api.Models
{
    public class Id
    {
        public string Value { get; }

        public Id(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
