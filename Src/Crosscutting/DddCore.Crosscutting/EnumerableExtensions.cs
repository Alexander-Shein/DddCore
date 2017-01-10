﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DddCore.Crosscutting
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Devide IEnumerable to chunks
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="values"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(this IEnumerable<TValue> values, int chunkSize)
        {
            return values
                .Select((v, i) => new { v, groupIndex = i / chunkSize })
                .GroupBy(x => x.groupIndex)
                .Select(g => g.Select(x => x.v));
        }

        /// <summary>
        /// Check if collection is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Perform action on every element in collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check is collection comtains only one element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsSingle<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            using (var enumerator = list.GetEnumerator())
            {
                return enumerator.MoveNext() && !enumerator.MoveNext();
            }
        }
    }
}