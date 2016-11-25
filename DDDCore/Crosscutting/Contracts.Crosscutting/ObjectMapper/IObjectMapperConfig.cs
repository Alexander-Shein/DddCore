using System;

namespace Contracts.Crosscutting.ObjectMapper
{
    public interface IObjectMapperConfig : IObjectMapper
    {
        void Bind<TFrom, TTo>(Action<IObjectMapperBindingConfig<TFrom, TTo>> config);
    }
}