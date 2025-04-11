// Ignore Spelling: Mongo uow

namespace R1.Cosmos.Mongo.Client.Factory
{
    using System;
    using MongoDB.Driver;
    using System.Threading.Tasks;
    using R1.Cosmos.Mongo.Client.Domain.IFactory;
    using R1.Cosmos.Mongo.Client.Domain.IUnitOfWork;
    using R1.Cosmos.Mongo.Client.IFactory;
    using R1.Cosmos.Mongo.Client.IServices;
    using R1.Cosmos.Mongo.Client.Services;

    public class CosmosMongoService : ICosmosMongoService
    {
        private readonly IUnitOfWorkFactory uowFactory;
        private ICosmosUnitOfWork uow;

        public CosmosMongoService(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public void GetDatabase(string databaseId)
        {
            this.uow = uowFactory.Create(databaseId);
        }

        public async Task<IClientSessionHandle> BeginTransactionAsync()
        {
            return await this.uow.BeginTransactionAsync();
        }

        public async Task CommitAsync(IClientSessionHandle session)
        {
            await this.uow.CommitAsync(session);
        }

        public async Task RollbackAsync(IClientSessionHandle session)
        {
            await this.uow.RollbackAsync(session);
        }

        public IEntityService<T> Create<T>(string collectionName = "", string partitionKey = "", string partitionValue = "") where T : class
        {
            return new EntityService<T>(this.uow, collectionName, partitionKey, partitionValue);
        }
    }
}