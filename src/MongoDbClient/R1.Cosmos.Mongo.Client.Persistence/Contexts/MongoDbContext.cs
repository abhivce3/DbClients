// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Persistence.Contexts
{
    using MongoDB.Driver;

    public class MongoDbContext
    {
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoClient mongoClient;

        public MongoDbContext(IMongoClient mongoClient, string databaseId)
        {
            this.mongoDatabase = mongoClient.GetDatabase(databaseId);
            this.mongoClient = mongoClient;
        }

        public IMongoDatabase MongoDatabase => mongoDatabase;
        public IMongoClient MongoClient => mongoClient;
    }
}