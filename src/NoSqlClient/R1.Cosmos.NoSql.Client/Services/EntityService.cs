namespace R1.CosmosClient.Services
{
    using Microsoft.Extensions.Logging;
    using R1.CosmosClient.Domain.Constants;
    using R1.CosmosClient.Domain.Data;
    using R1.CosmosClient.Domain.IFactory;
    using R1.CosmosClient.IServices;

    public class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly ICosmosDbRepository<T> repository;
        private readonly ILogger logger;

        public EntityService(ICosmosDbRepositoryFactory repositoryFactory, string databaseId, string containerId, ILogger logger)
        {
            this.repository = repositoryFactory.Create<T>(databaseId, containerId);
            this.logger = logger;
        }

        public async Task AddAsync(T item, string partitionKey)
        {
            try
            {
                logger.LogDebug(MessageConstants.Start, nameof(EntityService<T>), nameof(AddAsync));
                await repository.AddAsync(item, partitionKey);
                logger.LogDebug(MessageConstants.End, nameof(EntityService<T>), nameof(AddAsync));
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(EntityService<T>), nameof(AddAsync), ex);
                throw;
            }
        }

        public async Task DeleteAsync(string id, string partitionKey)
        {
            try
            {
                logger.LogDebug(MessageConstants.Start, nameof(EntityService<T>), nameof(DeleteAsync));

                await repository.DeleteAsync(id, partitionKey);

                logger.LogDebug(MessageConstants.End, nameof(EntityService<T>), nameof(DeleteAsync));
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(EntityService<T>), nameof(DeleteAsync), ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                logger.LogDebug(MessageConstants.Start, nameof(EntityService<T>), nameof(GetAllAsync));

                var results = await repository.GetAllAsync();

                logger.LogDebug(MessageConstants.End, nameof(EntityService<T>), nameof(GetAllAsync));
                return results;
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(EntityService<T>), nameof(GetAllAsync), ex);
                throw;
            }
        }

        public async Task<T> GetByIdAsync(string id, string partitionKey)
        {
            try
            {
                logger.LogDebug(MessageConstants.Start, nameof(EntityService<T>), nameof(GetByIdAsync));

                var result = await repository.GetByIdAsync(id, partitionKey);
                logger.LogDebug(MessageConstants.End, nameof(EntityService<T>), nameof(GetByIdAsync));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(EntityService<T>), nameof(GetByIdAsync), ex);
                throw;
            }
        }

        public async Task UpdateAsync(string id, T item, string partitionKey)
        {
            try
            {
                logger.LogDebug(MessageConstants.Start, nameof(EntityService<T>), nameof(UpdateAsync));

                await repository.UpdateAsync(id, item, partitionKey);
                logger.LogDebug(MessageConstants.End, nameof(EntityService<T>), nameof(UpdateAsync));
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(EntityService<T>), nameof(UpdateAsync), ex);
                throw;
            }
        }
    }
}