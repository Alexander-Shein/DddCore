using DddCore.Crosscutting;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace DddCore.Tests.Unit.Crosscutting
{
    public class AssemblyUtilityTests
    {
        [Fact]
        public void GetInstances_NoImplementations()
        {
            var actual = AssemblyUtility.GetInstances<IHasNoImplementations>();

            actual.Should().NotBeNull();
            actual.Should().BeEmpty();
        }

        [Fact]
        public void GetInstances_SingleInstance()
        {
            var actual = AssemblyUtility.GetInstances<IHasSingleImplementation>();

            actual.Should().NotBeNull();
            actual.Should().ContainSingle();
        }

        [Fact]
        public void GetInstances_MultipleInstances()
        {
            var actual = AssemblyUtility.GetInstances<IHasMultipleImplementations>();

            actual.Should().NotBeNull();
            actual.Count().Should().Be(2);
        }

        [Fact]
        public void GetInterfaceAndInstanceTypes_Generic_ClosedGenericArgument()
        {
            var actual = AssemblyUtility.GetInterfaceAndInstanceTypes<IGenericInterface<Instance1>>();

            actual.Should().NotBeNull();
            actual.Count().Should().Be(1);
        }

        [Fact]
        public void GetInterfaceAndInstanceTypes_Generic()
        {
            var actual = AssemblyUtility.GetInterfaceAndInstanceTypes<IHasMultipleImplementations>();

            actual.Should().NotBeNull();
            actual.Count().Should().Be(2);
        }

        [Fact]
        public void GetInterfaceAndInstanceTypes_OpenedGenericArgument()
        {
            var actual = AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IGenericInterface<>));

            actual.Should().NotBeNull();
            actual.Count().Should().Be(2);
        }

        [Fact]
        public void GetInterfaceAndInstanceTypes_ClosedGenericArgument()
        {
            var actual = AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IGenericInterface<Instance1>));

            actual.Should().NotBeNull();
            actual.Count().Should().Be(1);
        }

        [Fact]
        public void GetInterfaceAndInstanceTypes()
        {
            var actual = AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IHasMultipleImplementations));

            actual.Should().NotBeNull();
            actual.Count().Should().Be(2);
        }
    }

    public interface IHasNoImplementations { }

    public interface IHasSingleImplementation { }
    public class SingleImplementstion : IHasSingleImplementation { }

    public interface IHasMultipleImplementations { }
    public class Instance1 : IHasMultipleImplementations { }
    public class Instance2 : IHasMultipleImplementations { }

    public interface IGenericInterface<T> { }

    public class GenericInstance1 : IGenericInterface<Instance1> { }
    public class GenericInstance2 : IGenericInterface<Instance2> { }
}