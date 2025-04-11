namespace R1.Cosmos.Mongo.Client.Domain.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class BaseEntity
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}