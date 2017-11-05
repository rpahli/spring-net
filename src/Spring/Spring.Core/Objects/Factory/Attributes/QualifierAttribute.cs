using System;

namespace Spring.Objects.Factory.Attributes
{
    /// <summary>
    ///     This annotation may be used on a field or parameter as a qualifier for
    ///     candidate beans when autowiring. It may also be used to annotate other
    ///     custom annotations that can then in turn be used as qualifiers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field |
                    AttributeTargets.Parameter)]
    public class QualifierAttribute : Attribute
    {
        /// <summary>
        ///     Instantiate a new qualifier with an empty name
        /// </summary>
        public QualifierAttribute()
        {
            Value = "";
        }

        /// <summary>
        ///     Instantiate a new qualifier with a givin name
        /// </summary>
        /// <param name="value">name to use as qualifier</param>
        public QualifierAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Gets the name associated with this qualifier
        /// </summary>
        public string Value { get; }


        /// <summary>
        ///     Checks weather the attribute is the same
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            QualifierAttribute o1 = obj as QualifierAttribute;
            if (Value != o1.Value) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
    }
}