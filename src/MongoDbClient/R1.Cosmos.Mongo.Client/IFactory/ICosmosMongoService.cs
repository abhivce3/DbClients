// Ignore Spelling: Mongo

using MongoDB.Driver;
using R1.Cosmos.Mongo.Client.IServices;
using System.Threading.Tasks;

namespace R1.Cosmos.Mongo.Client.IFactory
{
    public interface ICosmosMongoService
    {
        void GetDatabase(string databaseId);
        Task<IClientSessionHandle> BeginTransactionAsync();

        Task CommitAsync(IClientSessionHandle session);

        Task RollbackAsync(IClientSessionHandle session);

        IEntityService<T> Create<T>(string collectionName = "", string partitionKey = "", string partitionValue = "") where T : class;
    }
}