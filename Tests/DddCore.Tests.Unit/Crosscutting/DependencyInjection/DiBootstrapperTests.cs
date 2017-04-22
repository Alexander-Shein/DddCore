using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Tests.Unit.Crosscutting.DependencyInjection
{
    public class DiBootstrapperTests
    {
        //[Fact]
        public void Bootstrap()
        {
            //var containerConfigMock = new Mock<IContainerConfig>();
            //var componentMock = new Mock<IComponent>();
            //var lifeStyleMock = new Mock<ILifeStyle>();
            //
            //componentMock.Setup(x => x.LifeStyle).Returns(lifeStyleMock.Object);
            //containerConfigMock.Setup(x => x.Register<IDiModule, DiModule>()).Returns(componentMock.Object);
            //
            //new DiBootstrapper()
            //    .AddConfig(containerConfigMock.Object)
            //    .Bootstrap();
            //
            //containerConfigMock.Verify(x => x.Register<IDiModule, DiModule>());
        }
    }

    public class DiModule : IDiModuleInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDiModuleInstaller, DiModule>();
        }
    }

    public class MicrosoftDiBootstrapper
    {
        //[Fact]
        public void Bootstrap()
        {
            //var containerConfigMock = new Mock<IContainerConfig>();
            //
            //var container =
            //    new DiBootstrapper()
            //        .AddMicrosoftDependencyInjection(new ServiceCollection())
            //        .Bootstrap();
            //
            //var module = container.GetService(typeof(IDiModule));
            //
            //container.Should().NotBeNull();
            //module.Should().NotBeNull();
        }
    }
}