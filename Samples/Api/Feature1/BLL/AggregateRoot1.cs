using DddCore.Contracts.Dal.DomainStack;
using DddCore.Dal.DomainStack.EntityFramework;
using DddCore.Domain.Entities.GuidEntities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using DddCore.Contracts.Domain.Events;
using DddCore.Contracts.Services.Application;
using DddCore.Contracts.Services.Application.DomainStack;

namespace Api.Feature1.BLL
{
    public class MyAggregateRoot : GuidAggregateRootEntityBase
    {
        public Collection<MyEntity> MyEntities { get; set; }
        public string Title { get; set; }
    }

    public class MyEntity : GuidEntityBase
    {
        public DateTime SomeDate { get; set; }
    }
}