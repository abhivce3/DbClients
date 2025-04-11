// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Persistence.UnitOfWork
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Domain.Data;
    using R1.Cosmos.Mongo.Client.Domain.IUnitOfWork;
    using R1.Cosmos.Mongo.Client.Persistence.Contexts;
    using R1.Cosmos.Mongo.Client.Persistence.Repositories;

    public class CosmosUnitOfWork : ICosmosUnitOfWork
    {
        private readonly MongoDbContext context;
        private readonly ConcurrentDictionary<Type, object> repositories;

        public CosmosUnitOfWork(MongoDbContext context)
        {
            this.context = context;
            this.repositories = new ConcurrentDictionary<Type, object>();
        }

        public ICosmosMongoRepository<T> GetRepository<T>(string collectionName = "", string partitionKey = "", object partitionValue = null) where T : class
        {
            return repositories.GetOrAdd(typeof(T), (type) =>
            {
                return new CosmosMongoRepository<T>(context, !string.IsNullOrWhiteSpace(collectionName) ? collectionName : typeof(T).Name
                                                             , partitionKey, partitionValue);
            }) as ICosmosMongoRepository<T>;
        }

        public async Task<IClientSessionHandle> BeginTransactionAsync()
        {
            var session = await context.MongoClient.StartSessionAsync();
            session.StartTransaction();
            return session;
        }

        public async Task CommitAsync(IClientSessionHandle session)
        {
            await session.CommitTransactionAsync();
        }

        public async Task RollbackAsync(IClientSessionHandle session)
        {
            await session.AbortTransactionAsync();
        }
    }
}