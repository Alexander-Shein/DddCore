using System;
using System.Collections.Generic;
using System.Linq;

namespace DddCore.BLL.Domain.ValueObjects
{
    public abstract class ValueObjectBase<T> : IEquatable<T> where T : class
    {
        #region Private Members

        const int Undefined = -1;
        volatile int hashCode = Undefined;

        #endregion

        public static bool operator ==(ValueObjectBase<T> x, ValueObjectBase<T> y)
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

        public static bool operator !=(ValueObjectBase<T> x, ValueObjectBase<T> y)
        {
            return !(x == y);
        }

        public bool Equals(T other)
        {
            var otherEquatable = other as ValueObjectBase<T>;
            if (otherEquatable == null)
            {
                return false;
            }

            return GetAllAttributesToBeUsedForEquality().SequenceEqual(otherEquatable.GetAllAttributesToBeUsedForEquality());
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as T;

            return !ReferenceEquals(other, null) && Equals(other);
        }

        public override int GetHashCode()
        {
            return GetHashCodeImpl();
        }

        protected abstract IEnumerable<object> GetAllAttributesToBeUsedForEquality();

        protected virtual void ResetHashCode()
        {
            hashCode = Undefined;
        }

        int GetHashCodeImpl()
        {
            if (hashCode == Undefined)
            {
                var code = 0;

                foreach (var attribute in GetAllAttributesToBeUsedForEquality())
                {
                    code = (code * 397) ^ (attribute?.GetHashCode() ?? 0);
                }

                hashCode = code;
            }

            return hashCode;
        }
    }
}
