namespace R1.CosmosClient.Factory
{
    using Microsoft.Extensions.Logging;
    using R1.CosmosClient.Domain.Constants;
    using R1.CosmosClient.Domain.IFactory;
    using R1.CosmosClient.IFactory;
    using R1.CosmosClient.IServices;
    using R1.CosmosClient.Services;

    public class EntityServiceFactory : IEntityServiceFactory
    {
        private readonly ILogger logger;
        private readonly ICosmosDbRepositoryFactory cosmosDbRepositoryFactory;

        public EntityServiceFactory(ICosmosDbRepositoryFactory cosmosDbRepositoryFactory, ILogger<EntityServiceFactory> logger)
        {
            this.cosmosDbRepositoryFactory = cosmosDbRepositoryFactory;
            this.logger = logger;
        }

        public IEntityService<T> Create<T>(string databaseId, string containerId) where T : class
        {
            try
            {
                return new EntityService<T>(cosmosDbRepositoryFactory, databaseId, containerId, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(EntityServiceFactory), nameof(Create), ex);
                throw;
            }
        }
    }
}