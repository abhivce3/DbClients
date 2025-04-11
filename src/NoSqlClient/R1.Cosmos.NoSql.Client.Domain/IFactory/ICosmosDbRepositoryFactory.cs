namespace R1.CosmosClient.Domain.IFactory
{
    using R1.CosmosClient.Domain.Data;

    public interface ICosmosDbRepositoryFactory
    {
        ICosmosDbRepository<T> Create<T>(string databaseId, string containerId) where T : class;
    }
}