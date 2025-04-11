// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Persistence.Factory
{
    using System;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Domain.IFactory;
    using R1.Cosmos.Mongo.Client.Domain.IUnitOfWork;
    using R1.Cosmos.Mongo.Client.Persistence.Contexts;
    using R1.Cosmos.Mongo.Client.Persistence.UnitOfWork;

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IMongoClient mongoClient;

        public UnitOfWorkFactory(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public ICosmosUnitOfWork Create(string databaseId)
        {
            var context = new MongoDbContext(this.mongoClient, databaseId);
            return new CosmosUnitOfWork(context);
        }
    }
}