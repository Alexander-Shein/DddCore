using System;
using System.Collections.Generic;
using System.Linq;

namespace DddCore.Crosscutting
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(this IEnumerable<TValue> values, int chunkSize)
        {
            return values
                .Select((v, i) => new { v, groupIndex = i / chunkSize })
                .GroupBy(x => x.groupIndex)
                .Select(g => g.Select(x => x.v));
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            // perf optimization. try to not use enumerator if possible
            if (source is IList<T> list)
            {
                for (int i = 0, count = list.Count; i < count; i++)
                {
                    action(list[i]);
                }
            }
            else
            {
                foreach (var value in source)
                {
                    action(value);
                }
            }

            return source;
        }

        public static bool IsSingle<T>(this IEnumerable<T> list)
        {
            using (var enumerator = list.GetEnumerator())
            {
                return enumerator.MoveNext() && !enumerator.MoveNext();
            }
        }
    }
}