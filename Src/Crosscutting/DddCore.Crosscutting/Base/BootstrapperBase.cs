using System;
using System.Linq;
using DddCore.Contracts.Crosscutting.Base;
using DddCore.Crosscutting.Configuration;

namespace DddCore.Crosscutting.Base
{
    public abstract class BootstrapperBase<T, TConfig, TModule> : IBootstrapper<T, TConfig, TModule> where TModule : IModule<TConfig>
    {
        #region Private Members

        TConfig config;

        #endregion

        #region Public Methods

        public IBootstrapper<T, TConfig, TModule> AddConfig(TConfig config)
        {
            this.config = config;
            return this;
        }

        public T Bootstrap()
        {
            var modules =
                AssemblyUtility
                    .GetInstances<TModule>()
                    .ToArray();

            return Bootstrap(modules);
        }

        public T Bootstrap(params TModule[] iocModules)
        {
            ValidateConfig();

            if (iocModules != null)
            {
                foreach (var iocModule in iocModules)
                {
                    iocModule.Install(config);
                }
            }

            return GetInstance(config);
        }

        #endregion

        protected abstract T GetInstance(TConfig config);

        #region Private Methods

        void ValidateConfig()
        {
            if (config == null)
            {
                throw new ArgumentException($"{nameof(config)} is Null.");
            }
        }

        #endregion
    }
}