using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper.Base;
using DddCore.Crosscutting.ObjectMapper;
using DddCore.Crosscutting.ObjectMapper.AutoMapper;
using FluentAssertions;
using NUnit.Framework;

namespace DddCore.Tests.Unit.Crosscutting.ObjectMapper
{
    [TestFixture]
    public class ObjectMapperTests
    {
        private IObjectMapper objectMapper;

        [SetUp]
        public void SetUp()
        {
            objectMapper =
                new ObjectMapperBootstrapper()
                    .AddAutoMapperConfig()
                    .Bootstrap(new ObjectMapperModule());
        }

        [Test]
        public void Test()
        {
            var from = new ObjectFrom
            {
                Property1 = "Property1",
                Property2 = "Property2"
            };

            var actual = objectMapper.Map<ObjectTo>(from);
            actual.Should().NotBeNull();
            actual.Property1.Should().Be(from.Property2);
            actual.Property2.Should().Be(from.Property1);
            actual.Property3.Should().BeNull();
        }
    }

    public class ObjectMapperModule : IObjectMapperModuleInstaller
    {
        #region Public Methods

        public void Install(IObjectMapperConfig config)
        {
            config.Bind<ObjectFrom, ObjectTo>(bindingConfig =>
            {
                bindingConfig.Bind(x => x.Property1, x => x.Property2);
                bindingConfig.Bind(x => x.Property2, x => x.Property1);
                bindingConfig.Ignore(x => x.Property3);
            });
        }

        #endregion
    }

    public class ObjectFrom
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
    }

    public class ObjectTo
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
    }
}