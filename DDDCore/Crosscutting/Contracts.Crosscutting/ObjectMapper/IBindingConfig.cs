using System;
using System.Linq.Expressions;

namespace Contracts.Crosscutting.ObjectMapper
{
    public interface IBindingConfig<TFrom, TTo>
    {
        IBindingConfig<TFrom, TTo> Bind(Expression<Func<TFrom, object>> source, Expression<Func<TTo, object>> target);
        IBindingConfig<TFrom, TTo> Ignore(Expression<Func<TTo, object>> expression);
    }
}