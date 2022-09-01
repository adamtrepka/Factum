using Factum.Modules.Documents.Core.Documents.Exceptions;
using System;
using System.Collections.Generic;

namespace Factum.Modules.Documents.Core.Documents.ValueObjects
{
    public class AccessType : IEquatable<AccessType>
    {
        private static readonly HashSet<string> AllowedValues = new()
        {
            "owner", "contributor", "reader"
        };

        public string Value { get; }

        public AccessType(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidAccessTypeException();

            value = value.ToLowerInvariant();
            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedAccessTypeException(value);
            }

            Value = value;
        }

        public static implicit operator string(AccessType value) => value.Value;
        public static implicit operator AccessType(string value) => new(value);

        public static bool operator ==(AccessType a, AccessType b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is not null && b is not null)
            {
                return a.Value.Equals(b.Value);
            }

            return false;
        }
        public static bool operator !=(AccessType a, AccessType b) => !(a == b);
        public bool Equals(AccessType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AccessType)obj);
        }
        public override int GetHashCode() => Value is not null ? Value.GetHashCode() : 0;
    }
}
