using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Dal.DomainStack.Ef
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeRange<T, TProperty>(this IQueryable<T> source, params Expression<Func<T, TProperty>>[] paths)
        {
            if (paths == null)
                throw new NullReferenceException();

            return paths.Aggregate(source, (current, path) => current.Include(path));
        }
    }
}