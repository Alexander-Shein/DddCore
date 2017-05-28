using System;
using System.Linq.Expressions;

namespace DddCore.Contracts.Crosscutting.ObjectMapper
{
    public interface IObjectMapperBindingConfig<TFrom, TTo>
    {
        IObjectMapperBindingConfig<TFrom, TTo> Bind(Expression<Func<TTo, object>> target, Expression<Func<TFrom, object>> source);
        IObjectMapperBindingConfig<TFrom, TTo> Ignore(Expression<Func<TTo, object>> target);
    }
}