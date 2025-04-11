// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.IServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Domain.IUnitOfWork;

    public interface IEntityService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(IClientSessionHandle session = null);
        Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filterDefinition, IClientSessionHandle session = null);

        Task<T> GetByIdAsync(string id, IClientSessionHandle session = null);

        Task AddAsync(T entity, IClientSessionHandle session = null);

        Task UpdateAsync(string id, T entity, IClientSessionHandle session = null);

        Task DeleteAsync(string id, IClientSessionHandle session = null);
    }
}