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

    public interface IMyAggregateRootRepository : IRepository<MyAggregateRoot, Guid>
    {
    }

    public class MyAggregateRootRepository : RepositoryBase<MyAggregateRoot, Guid>, IMyAggregateRootRepository
    {
        public MyAggregateRootRepository(IDataContext dataContext, IUserContext<Guid> userContext) : base(dataContext, userContext)
        {
        }

        public async override Task<MyAggregateRoot> ReadAggregateRootAsync(Guid key)
        {
            return await GetDbSet().Include(x => x.MyEntities).FirstOrDefaultAsync(x => x.Id == key);
        }
    }

    public class MyAggregateRootEntityService : EntityService<MyAggregateRoot, Guid>, IMyAggregateRootEntityService
    {
        public MyAggregateRootEntityService(IRepository<MyAggregateRoot, Guid> repository, IGuard guard, IDomainEventDispatcher domainEventDispatcher) : base(repository, guard, domainEventDispatcher) { }
    }

    public interface IMyAggregateRootEntityService : IEntityService<MyAggregateRoot, Guid>
    {
    }

    public interface IMyAggregateRootWorkflowService : IWorkflowService
    {
    }

    public class MyAggregateRootWorkflowService : IMyAggregateRootWorkflowService
    {
        public MyAggregateRootWorkflowService(IMyAggregateRootEntityService myAggregateRootEntityService)
        {
        }
    }
}