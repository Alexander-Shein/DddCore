using System;
using System.Linq.Expressions;
using AutoMapper;
using Contracts.Crosscutting.ObjectMapper;

namespace Crosscutting.ObjectMapper.AutoMapperSupport
{
    public class AutoMapperBindingConfig<TFrom, TTo> : IBindingConfig<TFrom, TTo>
    {
        #region Private Members

        readonly IMappingExpression<TFrom, TTo> mappingExpression;

        #endregion

        #region ctor

        public AutoMapperBindingConfig(IMappingExpression<TFrom, TTo> mappingExpression)
        {
            this.mappingExpression = mappingExpression;
        }

        #endregion

        #region Public Methods

        public IBindingConfig<TFrom, TTo> Bind(Expression<Func<TFrom, object>> source, Expression<Func<TTo, object>> target)
        {
            mappingExpression.ForMember(target, opt => opt.MapFrom(source));
            return this;
        }

        public IBindingConfig<TFrom, TTo> Ignore(Expression<Func<TTo, object>> expression)
        {
            mappingExpression.ForMember(expression, opt => opt.Ignore());
            return this;
        }

        #endregion
    }
}