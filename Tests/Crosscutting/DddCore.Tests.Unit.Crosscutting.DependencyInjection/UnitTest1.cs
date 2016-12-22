using Xunit;
using DddCore.Crosscutting;
using FluentAssertions;

namespace DddCore.Tests.Unit.Crosscutting.DependencyInjection
{
    public class UnitTest1
    {
        //[Fact]
        public void Test1()
        {
            var ololos = AssemblyUtility.GetInstances<ISingleImplementation>();

            ololos.Should().NotBeNull();
            ololos.Should().ContainSingle();
        }
    }

    public interface ISingleImplementation { }
    public class SingleImplementstion : ISingleImplementation { }
}