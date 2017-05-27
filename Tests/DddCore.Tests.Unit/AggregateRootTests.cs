using DddCore.BLL.Domain.Entities.GuidEntities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Graph;
using DddCore.Crosscutting.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DddCore.Tests.Unit
{
    public class AggregateRootTests
    {
        [Fact]
        public void Validate()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddDddCore();

            var container = serviceCollection.BuildServiceProvider();
            var factory = container.GetService<IBusinessRulesValidatorFactory>();

            var domain = new TestAggregateRoot();
            var validationResult = domain.ValidateGraph(factory, GraphDepth.AggregateRoot);
        }
    }

    public class TestAggregateRoot : GuidAggregateRootBase
    {
        public string Name { get; set; }
    }
}