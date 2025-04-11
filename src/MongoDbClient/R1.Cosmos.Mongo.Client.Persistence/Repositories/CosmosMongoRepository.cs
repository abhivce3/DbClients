// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Domain.Data;
    using R1.Cosmos.Mongo.Client.Persistence.Contexts;

    public class CosmosMongoRepository<T> : ICosmosMongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> collection;
        private readonly string partitionKey;
        private readonly object partitionValue;

        public CosmosMongoRepository(MongoDbContext context, string collectionName, string partitionKey = "", object partitionValue = null)
        {
            this.collection = context.MongoDatabase.GetCollection<T>(collectionName);
            this.partitionKey = partitionKey;
            this.partitionValue = partitionValue;
        }

        public async Task<IEnumerable<T>> GetAllAsync(IClientSessionHandle session = null)
        {
            var filter = !string.IsNullOrWhiteSpace(this.partitionKey) && this.partitionValue != null ?
                        GetPartitionFilter() : Builders<T>.Filter.Empty;
            return session == null ? await this.collection.Find(filter).ToListAsync().ConfigureAwait(false)
                                    : await this.collection.Find(session, filter).ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(string id, IClientSessionHandle session = null)
        {
            var filter = GetIdPartitionFilter(id);
            return session == null ? await this.collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false)
                                    : await this.collection.Find(session, filter).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetByFilterAsync(FilterDefinition<T> filterDefinition, IClientSessionHandle session = null)
        {
            var filter = !string.IsNullOrWhiteSpace(this.partitionKey) && this.partitionValue != null ?
                        GetPartitionFilter() & filterDefinition : filterDefinition;
            return session == null ? await this.collection.Find(filter).ToListAsync().ConfigureAwait(false)
                                    : await this.collection.Find(session, filter).ToListAsync().ConfigureAwait(false);
        }

        public async Task AddAsync(T entity, IClientSessionHandle session = null)
        {
            if (!string.IsNullOrWhiteSpace(this.partitionKey) && this.partitionValue != null)
            {
                var prop = entity.GetType().GetProperty(partitionKey);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(entity, this.partitionValue, null);
                }
                else
                {
                    throw new InvalidOperationException($"The entity type {typeof(T).Name} does not contain a settable property '{this.partitionKey}'");
                }
            }
            if (session == null)
            {
                await this.collection.InsertOneAsync(entity).ConfigureAwait(false);
            }
            else
            {
                await this.collection.InsertOneAsync(session, entity).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(string id, T entity, IClientSessionHandle session = null)
        {
            var filter = GetIdPartitionFilter(id);
            var entityPropInfo = entity.GetType().GetProperty("Id");
            if(entityPropInfo != null)
            {
                entityPropInfo.SetValue(entity, ObjectId.Parse(id));
            }
            if (session == null)
            {
                await this.collection.ReplaceOneAsync(filter, entity).ConfigureAwait(false);
            }
            else
            {
                await this.collection.ReplaceOneAsync(session, filter, entity).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(string id, IClientSessionHandle session = null)
        {
            var filter = GetIdPartitionFilter(id);
            if (session == null)
            {
                await this.collection.DeleteOneAsync(filter).ConfigureAwait(false);
            }
            else
            {
                await this.collection.DeleteOneAsync(session, filter).ConfigureAwait(false);
            }
        }

        private FilterDefinition<T> GetIdPartitionFilter(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            if (!string.IsNullOrWhiteSpace(this.partitionKey) && partitionValue != null)
            {
                filter &= GetPartitionFilter();
            }
            return filter;
        }

        private FilterDefinition<T> GetPartitionFilter()
        {
            return Builders<T>.Filter.Eq(this.partitionKey, this.partitionValue);
        }
    }
}