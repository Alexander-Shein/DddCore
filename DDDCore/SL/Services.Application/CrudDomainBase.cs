using System;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;
using Contracts.Services.Application;
using Contracts.Services.Application.DomainStack;

namespace Services.Application
{
    public abstract class CrudDomainBase<TVm, TCreateIm, TUpdateIm, TDomain> :
        ICreate<TVm, TCreateIm>,
        IDelete<Guid>,
        IRead<TVm, Guid>,
        IUpdate<Guid, TVm, TUpdateIm>
        where TDomain : class, IAggregateRootEntityBase<Guid>
    {
        readonly IRepository<TDomain, Guid> repository;
        readonly IGuard guard;
        readonly IMapper mapper;
        readonly IUnitOfWork unitOfWork;

        protected CrudDomainBase(IRepository<TDomain, Guid> repository, IGuard guard, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.guard = guard;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public virtual TVm Create(TCreateIm im)
        {
            guard.NotNull(im);

            var domain = mapper.Map<TDomain>(im);

            guard.DomainIsValid(domain);

            repository.PersistEntityGraph(domain);

            unitOfWork.SaveAsync();

            var result = mapper.Map<TVm>(domain);
            return result;
        }

        public virtual TVm Read(Guid key, string[] includes = null)
        {
            var domain = repository.ReadAsync(key).Result;
            var result = mapper.Map<TVm>(domain);
            return result;
        }

        public virtual TVm Update(Guid key, TUpdateIm im)
        {
            var domain = repository.ReadAsync(key).Result;

            mapper.Map(im, domain);

            guard.DomainIsValid(domain);

            repository.PersistEntityGraph(domain);

            unitOfWork.SaveAsync();

            var result = mapper.Map<TVm>(domain);
            return result;
        }

        public virtual void Delete(Guid key)
        {
            var entity = repository.ReadAsync(key).Result;
            repository.PersistEntityGraph(entity);
            unitOfWork.SaveAsync();
        }

        public Task<TVm> CreateAsync(TCreateIm im)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<TVm> ReadAsync(Guid key, string[] includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Update(TVm key, TUpdateIm model)
        {
            throw new NotImplementedException();
        }
    }
}