
namespace DddCore.Tests.Unit.Crosscutting
{
    public class AssemblyUtilityTests
    {
        //[Fact]
        //public void GetInstances_NoImplementations()
        //{
        //    var actual = AssemblyUtility.GetInstancesOf<IHasNoImplementations>();
        //
        //    actual.Should().NotBeNull();
        //    actual.Should().BeEmpty();
        //}
        //
        //[Fact]
        //public void GetInstances_SingleInstance()
        //{
        //    var actual = AssemblyUtility.GetInstancesOf<IHasSingleImplementation>();
        //
        //    actual.Should().NotBeNull();
        //    actual.Should().ContainSingle();
        //}
        //
        //[Fact]
        //public void GetInstances_MultipleInstances()
        //{
        //    var actual = AssemblyUtility.GetInstancesOf<IHasMultipleImplementations>();
        //
        //    actual.Should().NotBeNull();
        //    actual.Count().Should().Be(2);
        //}
        //
        //[Fact]
        //public void GetTypes_Generic_ClosedGenericArgument()
        //{
        //    var actual = AssemblyUtility.GetTypes<IGenericInterface<Instance1>>();
        //
        //    actual.Should().NotBeNull();
        //    actual.Count().Should().Be(1);
        //}
        //
        //[Fact]
        //public void GetTypes_Generic()
        //{
        //    var actual = AssemblyUtility.GetTypes<IHasMultipleImplementations>();
        //
        //    actual.Should().NotBeNull();
        //    actual.Count().Should().Be(2);
        //}
        //
        //[Fact]
        //public void GetTypes_OpenedGenericArgument()
        //{
        //    var actual = AssemblyUtility.GetTypes(typeof(IGenericInterface<>));
        //
        //    actual.Should().NotBeNull();
        //    actual.Count().Should().Be(2);
        //}
        //
        //[Fact]
        //public void GetTypes_ClosedGenericArgument()
        //{
        //    var actual = AssemblyUtility.GetTypes(typeof(IGenericInterface<Instance1>));
        //
        //    actual.Should().NotBeNull();
        //    actual.Count().Should().Be(1);
        //}
        //
        //[Fact]
        //public void GetTypes()
        //{
        //    var actual = AssemblyUtility.GetTypes(typeof(IHasMultipleImplementations));
        //
        //    actual.Should().NotBeNull();
        //    actual.Count().Should().Be(2);
        //}
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