using System;
using System.Linq.Expressions;
using AutoMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper;

namespace DddCore.Crosscutting.ObjectMapper.AutoMapper
{
    public class AutoMapperObjectMapperBindingConfig<TFrom, TTo> : IObjectMapperBindingConfig<TFrom, TTo>
    {
        #region Private Members

        readonly IMappingExpression<TFrom, TTo> mappingExpression;

        #endregion

        #region ctor

        public AutoMapperObjectMapperBindingConfig(IMappingExpression<TFrom, TTo> mappingExpression)
        {
            this.mappingExpression = mappingExpression;
        }

        #endregion

        #region Public Methods

        public IObjectMapperBindingConfig<TFrom, TTo> Bind(Expression<Func<TTo, object>> target, Expression<Func<TFrom, object>> source)
        {
            mappingExpression.ForMember(target, opt => opt.MapFrom(source));
            return this;
        }

        public IObjectMapperBindingConfig<TFrom, TTo> Ignore(Expression<Func<TTo, object>> target)
        {
            mappingExpression.ForMember(target, opt => opt.Ignore());
            return this;
        }

        #endregion
    }
}