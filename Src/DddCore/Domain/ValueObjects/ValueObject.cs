using System;
using System.Collections.Generic;
using System.Linq;

namespace DddCore.Domain.ValueObjects
{
    public abstract class ValueObject<T> : IEquatable<T> where T : class
    {
        #region Private Members

        const int Undefined = -1;
        volatile int hashCode = Undefined;

        #endregion

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }

        public bool Equals(T other)
        {
            var otherEquatable = other as ValueObject<T>;
            if (otherEquatable == null)
            {
                return false;
            }

            return this.GetAllAttributesToBeUsedForEquality().SequenceEqual(otherEquatable.GetAllAttributesToBeUsedForEquality());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as T;

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.GetHashCodeImpl();
        }

        protected abstract IEnumerable<object> GetAllAttributesToBeUsedForEquality();

        protected virtual void ResetHashCode()
        {
            this.hashCode = Undefined;
        }

        int GetHashCodeImpl()
        {
            if (this.hashCode == Undefined)
            {
                var code = 0;

                foreach (var attribute in this.GetAllAttributesToBeUsedForEquality())
                {
                    code = (code * 397) ^ (attribute?.GetHashCode() ?? 0);
                }

                this.hashCode = code;
            }

            return this.hashCode;
        }
    }
}
