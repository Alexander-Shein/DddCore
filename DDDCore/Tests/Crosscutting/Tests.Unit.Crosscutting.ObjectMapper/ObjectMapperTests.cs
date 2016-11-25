using Contracts.Crosscutting.ObjectMapper;
using Contracts.Crosscutting.ObjectMapper.Base;
using Crosscutting.ObjectMapper;
using NUnit.Framework;

namespace Tests.Unit.Crosscutting.ObjectMapper
{
    [TestFixture]
    public class ObjectMapperTests
    {
        private IObjectMapper objectMapper;

        [SetUp]
        public void SetUp()
        {
            var objectMapperBootstrapper = new ObjectMapperBootstrapper();
            objectMapper = objectMapperBootstrapper.Bootstrap(ObjectMapperType.TinyMapper);
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
        }
    }

    public class ObjectMapperModule : IObjectMapperModule
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