namespace R1.CosmosClient.Persistence.Factory
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Logging;
    using R1.CosmosClient.Domain.Constants;
    using R1.CosmosClient.Domain.Data;
    using R1.CosmosClient.Domain.IFactory;
    using R1.CosmosClient.Persistence.Contexts;
    using R1.CosmosClient.Persistence.Repositories;

    public class CosmosDbRepositoryFactory : ICosmosDbRepositoryFactory
    {
        private readonly ILogger logger;
        private readonly CosmosClient cosmosClient;

        public CosmosDbRepositoryFactory(CosmosClient cosmosClient, ILogger<CosmosDbRepositoryFactory> logger)
        {
            this.logger = logger;
            this.cosmosClient = cosmosClient;
        }

        public ICosmosDbRepository<T> Create<T>(string databaseId, string containerId) where T : class
        {
            try
            {
                var context = new CosmosDbContext(cosmosClient, databaseId, containerId);
                return new CosmosDbRepository<T>(context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(MessageConstants.Error, nameof(CosmosDbRepositoryFactory), nameof(Create), ex);
                throw;
            }
        }
    }
}