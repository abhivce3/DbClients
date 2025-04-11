namespace R1.CosmosClient.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Logging;
    using R1.CosmosClient.Domain.Constants;
    using R1.CosmosClient.Domain.Data;
    using R1.CosmosClient.Persistence.Contexts;

    public class CosmosDbRepository<T> : ICosmosDbRepository<T> where T : class
    {
        private readonly CosmosDbContext context;
        private readonly ILogger logger;

        public CosmosDbRepository(CosmosDbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task AddAsync(T entity, string partitionKey)
        {
            try
            {
                await context.Container.CreateItemAsync(entity, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex)
            {
                logger.LogError(MessageConstants.Error, nameof(CosmosDbRepository<T>), nameof(AddAsync), ex);
                throw;
            }
        }

        public async Task DeleteAsync(string id, string partitionKey)
        {
            try
            {
                await context.Container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex)
            {
                logger.LogError(MessageConstants.Error, nameof(CosmosDbRepository<T>), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var query = context.Container.GetItemQueryIterator<T>(new QueryDefinition("SELECT * FROM c"));
                var results = new List<T>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.Resource);
                }
                return results;
            }
            catch (CosmosException ex)
            {
                logger.LogError(MessageConstants.Error, nameof(CosmosDbRepository<T>), nameof(GetAllAsync), ex);
                throw;
            }
        }

        public async Task<T> GetByIdAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await context.Container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                logger.LogWarning("Item not found with id: {Id}", id);
                return null;
            }
            catch (CosmosException ex)
            {
                logger.LogError(MessageConstants.Error, nameof(CosmosDbRepository<T>), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(string id, T entity, string partitionKey)
        {
            try
            {
                await context.Container.UpsertItemAsync(entity, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex)
            {
                logger.LogError(MessageConstants.Error, nameof(CosmosDbRepository<T>), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}