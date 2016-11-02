using System;
using Contracts.Crosscutting.ObjectMapper;
using Contracts.Crosscutting.ObjectMapper.Base;
using Crosscutting.ObjectMapper.AutoMapperSupport;
using Crosscutting.ObjectMapper.TinyMapperSupport;

namespace Crosscutting.ObjectMapper
{
    public class ObjectMapperConfigFactory : IObjectMapperConfigFactory
    {
        #region Public Methods

        public IObjectMapperConfig Create(ObjectMapperType type)
        {
            switch (type)
            {
                case ObjectMapperType.AutoMapper:
                    return new AutoMapperObjectMapperConfig();

                case ObjectMapperType.TinyMapper:
                    return new TinyMapperObjectMapperConfig();

                default: throw new NotImplementedException();
            }
        }

        #endregion
    }
}
