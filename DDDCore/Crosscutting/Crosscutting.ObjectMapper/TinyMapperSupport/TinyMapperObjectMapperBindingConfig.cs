using System;
using System.Linq.Expressions;
using Contracts.Crosscutting.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;

namespace Crosscutting.ObjectMapper.TinyMapperSupport
{
    public class TinyMapperObjectMapperBindingConfig<TFrom, TTo> : IObjectMapperBindingConfig<TFrom, TTo>
    {
        #region Private Members

        readonly IBindingConfig<TFrom, TTo> tinyMapperBindingConfig;

        #endregion

        public TinyMapperObjectMapperBindingConfig(IBindingConfig<TFrom, TTo> tinyMapperBindingConfig)
        {
            this.tinyMapperBindingConfig = tinyMapperBindingConfig;
        }

        #region Public Methods

        public IObjectMapperBindingConfig<TFrom, TTo> Bind(Expression<Func<TFrom, object>> source, Expression<Func<TTo, object>> target)
        {
            tinyMapperBindingConfig.Bind(source, target);
            return this;
        }

        public IObjectMapperBindingConfig<TFrom, TTo> Ignore(Expression<Func<TFrom, object>> expression)
        {
            tinyMapperBindingConfig.Ignore(expression);
            return this;
        }

        #endregion
    }
}