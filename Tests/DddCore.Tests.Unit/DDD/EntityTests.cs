using System;
using System.Collections.Generic;
using System.Linq;
using DddCore.Domain.Entities;
using DddCore.Domain.Entities.GuidEntities;
using FluentAssertions;
using NUnit.Framework;

namespace DddCore.Tests.Unit.DDD
{

    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void GetPropertiesWithEntities_ShouldReturnCollectionAndSimpleProperties_Test()
        {
            // Act
            var actual = EntityGraphHelper.GetPropertiesWithEntities<TestAggregateRootEntity, Guid>();

            // Asert
            actual.Should().NotBeNull();
            actual.Count().Should().Be(2);
        }

        [Test]
        public void GetPropertiesWithEntities_WhenPropertiesAreNull_Test()
        {
            // Arrange
            var rootEntity = new TestAggregateRootEntity
            {
                TestEntity = null,
                TestEntities = null
            };

            // Act
            var actual =
                EntityGraphHelper
                    .GetPropertiesWithEntities<TestAggregateRootEntity, Guid>();

            // Assert
            var properties = actual.Select(x => x.Compile()(rootEntity));

            properties.Should().NotBeNull();

            foreach (var property in properties)
            {
                property.Should().BeNull();
            }
        }

        [Test]
        public void GetPropertiesWithEntities_WhenPropertiesAreNotNull_Test()
        {
            // Arrange
            var rootEntity = new TestAggregateRootEntity
            {
                TestEntity = new TestEntity(),
                TestEntities = new List<TestEntity>
                {
                    new TestEntity()
                },
                Property1 = String.Empty,
                Property2 = new List<string>()
            };

            // Act
            var actual =
                EntityGraphHelper
                    .GetPropertiesWithEntities<TestAggregateRootEntity, Guid>();

            // Assert
            var properties = actual.Select(x => x.Compile()(rootEntity));

            properties.Should().NotBeNull();
        }
        
        [TestCase(true, 0)]
        [TestCase(false, 2)]
        public void GetPropertiesWithEntities_ExcludeAggregateRoots_Test(bool excludeAggregateRoots, int propertiesCount)
        {
            // Act
            var actual =
                EntityGraphHelper
                    .GetPropertiesWithEntities<TestEntity, Guid>(excludeAggregateRoots);

            // Assert
            actual.Should().NotBeNull();
            actual.Count().Should().Be(propertiesCount);
        }


        public class TestAggregateRootEntity : GuidAggregateRootEntityBase
        {
            public string Property1 { get; set; }
            public ICollection<string> Property2 { get; set; }
            public ICollection<TestEntity> TestEntities { get; set; }
            public TestEntity TestEntity { get; set; }
        }

        public class TestEntity : GuidEntityBase
        {
            public TestAggregateRootEntity TestAggregateRootEntity { get; set; }
            public ICollection<TestAggregateRootEntity> TestAggregateRootEntities { get; set; }
        }
    }
}