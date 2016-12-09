using System;
using AutoMapper;
using AutoMapper.Configuration;
using DddCore.Contracts.Crosscutting.ObjectMapper;

namespace DddCore.Crosscutting.ObjectMapper.AutoMapper
{
    public class AutoMapperObjectMapperConfig : IObjectMapperConfig
    {
        #region Private Members

        readonly MapperConfigurationExpression configuration = new MapperConfigurationExpression();

        #endregion

        #region Public Methods

        public T Map<T>(object @from)
        {
            return Mapper.Map<T>(@from);
        }

        public void Bind<TFrom, TTo>(Action<IObjectMapperBindingConfig<TFrom, TTo>> config)
        {
            var mappingExpression = configuration.CreateMap<TFrom, TTo>();
            var bindingConfig = new AutoMapperObjectMapperBindingConfig<TFrom, TTo>(mappingExpression);

            config(bindingConfig);
            Mapper.Initialize(configuration);
        }

        #endregion
    }
}
