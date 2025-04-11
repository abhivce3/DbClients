// Ignore Spelling: Mongo uow

namespace R1.Cosmos.Mongo.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Domain.Data;
    using R1.Cosmos.Mongo.Client.Domain.IUnitOfWork;
    using R1.Cosmos.Mongo.Client.IServices;

    public class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly ICosmosUnitOfWork uow;
        private readonly ICosmosMongoRepository<T> repository;

        public EntityService(ICosmosUnitOfWork uow, string collectionName, string partitionKey = "", string partitionValue = "")
        {
            this.uow = uow;
            this.repository = this.uow.GetRepository<T>(collectionName, partitionKey, partitionValue);
        }

        public async Task<IEnumerable<T>> GetAllAsync(IClientSessionHandle session = null)
        {
            return await this.repository.GetAllAsync(session);
        }

        public async Task<T> GetByIdAsync(string id, IClientSessionHandle session = null)
        {
            return await this.repository.GetByIdAsync(id, session);
        }
        public async Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filterDefinition, IClientSessionHandle session = null)
        {
            return await this.repository.GetByFilterAsync(filterDefinition, session);
        }

        public async Task AddAsync(T entity, IClientSessionHandle session = null)
        {
            await this.repository.AddAsync(entity, session);
        }

        public async Task UpdateAsync(string id, T entity, IClientSessionHandle session = null)
        {
            await this.repository.UpdateAsync(id, entity, session);
        }

        public async Task DeleteAsync(string id, IClientSessionHandle session = null)
        {
            await this.repository.DeleteAsync(id, session);
        }
    }
}