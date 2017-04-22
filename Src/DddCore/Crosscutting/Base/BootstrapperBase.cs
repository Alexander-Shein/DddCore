using System;
using System.Linq;
using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.Crosscutting.Base
{
    public abstract class BootstrapperBase<T, TConfig, TModule> : IBootstrapper<T, TConfig, TModule> where TModule : IModuleInstaller<TConfig>
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
                    .GetInstancesOf<TModule>()
                    .ToArray();

            return Bootstrap(modules);
        }

        public T Bootstrap(params TModule[] iocModules)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

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
    }
}