using System;
using Contracts.Crosscutting.ObjectMapper;
using Nelibur.ObjectMapper;

namespace Crosscutting.ObjectMapper.TinyMapperSupport
{
    public class TinyMapperObjectMapperConfig : IObjectMapperConfig
    {
        #region Public Methods

        public T Map<T>(object @from)
        {
            return TinyMapper.Map<T>(@from);
        }

        public void Bind<TFrom, TTo>(Action<IBindingConfig<TFrom, TTo>> config)
        {
        }

        #endregion
    }
}
