using System;
using System.Linq.Expressions;

namespace Contracts.Crosscutting.ObjectMapper
{
    public interface IObjectMapperBindingConfig<TFrom, TTo>
    {
        IObjectMapperBindingConfig<TFrom, TTo> Bind(Expression<Func<TFrom, object>> source, Expression<Func<TTo, object>> target);
        IObjectMapperBindingConfig<TFrom, TTo> Ignore(Expression<Func<TFrom, object>> expression);
    }
}