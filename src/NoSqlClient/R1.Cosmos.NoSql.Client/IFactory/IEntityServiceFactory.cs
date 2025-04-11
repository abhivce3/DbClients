namespace R1.CosmosClient.IFactory
{
    using R1.CosmosClient.IServices;

    public interface IEntityServiceFactory
    {
        IEntityService<T> Create<T>(string databaseId, string containerId) where T : class;
    }
}