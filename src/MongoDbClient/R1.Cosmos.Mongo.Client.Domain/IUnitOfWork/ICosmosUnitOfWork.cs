// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Domain.IUnitOfWork
{
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Domain.Data;

    public interface ICosmosUnitOfWork
    {
        ICosmosMongoRepository<T> GetRepository<T>(string collectionName = "", string partitionKey = "", object partitionValue = null) where T : class;

        Task<IClientSessionHandle> BeginTransactionAsync();

        Task CommitAsync(IClientSessionHandle session);

        Task RollbackAsync(IClientSessionHandle session);
    }
}