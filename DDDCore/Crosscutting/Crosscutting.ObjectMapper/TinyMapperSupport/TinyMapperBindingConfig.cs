using System;
using System.Linq.Expressions;
using Contracts.Crosscutting.ObjectMapper;

namespace Crosscutting.ObjectMapper.TinyMapperSupport
{
    public class TinyMapperBindingConfig<TFrom, TTo> : IBindingConfig<TFrom, TTo>
    {
        #region Public Methods

        public IBindingConfig<TFrom, TTo> Bind(Expression<Func<TFrom, object>> source, Expression<Func<TTo, object>> target)
        {
            throw new NotImplementedException();
        }

        public IBindingConfig<TFrom, TTo> Ignore(Expression<Func<TTo, object>> expression)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}