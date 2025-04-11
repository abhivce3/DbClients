namespace R1.CosmosClient.Persistence.Contexts
{
    using Microsoft.Azure.Cosmos;

    public class CosmosDbContext
    {
        private readonly Container container;

        public CosmosDbContext(CosmosClient cosmosClient, string databaseId, string containerId)
        {
            container = cosmosClient.GetContainer(databaseId, containerId);
        }

        public Container Container => container;
    }
}