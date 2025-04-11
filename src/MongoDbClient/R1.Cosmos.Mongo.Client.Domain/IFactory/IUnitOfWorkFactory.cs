namespace R1.Cosmos.Mongo.Client.Domain.IFactory
{
    using R1.Cosmos.Mongo.Client.Domain.IUnitOfWork;

    public interface IUnitOfWorkFactory
    {
        ICosmosUnitOfWork Create(string databaseId);
    }
}