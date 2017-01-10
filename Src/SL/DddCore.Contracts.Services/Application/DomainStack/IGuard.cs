using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Services.Infrastructure;

namespace DddCore.Contracts.Services.Application.DomainStack
{
    public interface IGuard : IInfrastructureService
    {
        void NotNull(object obj, string message = "");

        Task<BusinessRulesValidationResult> ValidateAggregateRootAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
        BusinessRulesValidationResult ValidateAggregateRoot<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;

        Task ValidateAggregateRootAndThrowAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
        void ValidateAggregateRootAndThrow<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
    }
}
