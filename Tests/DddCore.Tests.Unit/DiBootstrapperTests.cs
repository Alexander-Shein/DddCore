using System;
using DddCore.Contracts.SL.Services.Application;
using DddCore.Crosscutting.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DddCore.Tests.Unit
{
    public class DiBootstrapperTests
    {
        [Theory]
        [InlineData(typeof(ITestWorkflowService), typeof(TestWorkflowService))]
        public void AddDddCore(Type contractType, Type implementationType)
        {
            // Arrange
            IServiceCollection serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddDddCore();

            var container = serviceCollection.BuildServiceProvider();

            // Assert
            var workflowService = container.GetService(contractType);

            workflowService.Should().NotBeNull();
            workflowService.Should().BeOfType(implementationType);
        }
    }

    public interface ITestWorkflowService : IWorkflowService
    {
    }

    public class TestWorkflowService : ITestWorkflowService
    {
    }
}