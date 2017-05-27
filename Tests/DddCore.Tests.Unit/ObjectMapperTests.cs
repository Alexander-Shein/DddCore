using System;
using DddCore.BLL.Domain.Entities.GuidEntities;
using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Crosscutting.DependencyInjection;
using DddCore.Crosscutting.ObjectMapper;
using DddCore.Crosscutting.ObjectMapper.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DddCore.Tests.Unit
{
    public class ObjectMapperTests
    {
        [Fact]
        public void IgnorePropertyTest()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDddCore();
            serviceCollection.AddAutoMapper();
            var container = serviceCollection.BuildServiceProvider();

            var objectMapper = container.GetService<IObjectMapper>();

            var im = new IdentityClaimIm
            {
                Id = Guid.NewGuid()
            };

            var domain = objectMapper.Map<IdentityResourceClaim>(im);
        }
    }

    public class IdentityResourceClaim : GuidEntityBase
    {
        public Guid IdentityClaimId { get; set; }
    }

    public class IdentityClaimIm
    {
        public Guid Id { get; set; }
    }

    public class ApiResourcesObjectMapperModuleInstaller : ObjectMapperModuleInstallerBase
    {
        protected override void FromDtoToView()
        {
        }

        protected override void FromDomainToView()
        {
        }

        protected override void FromInputToDomain()
        {
            Config.Bind<IdentityClaimIm, IdentityResourceClaim>(c =>
            {
                c.Bind(x => x.Id, x => x.IdentityClaimId);
                c.Ignore(x => x.Id);
            });
        }
    }
}