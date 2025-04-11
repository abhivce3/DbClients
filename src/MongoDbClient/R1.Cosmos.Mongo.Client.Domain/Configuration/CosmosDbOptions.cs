// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Domain.Configuration
{
    using Azure.Core;

    public class CosmosDbOptions
    {
        public string Endpoint { get; set; }
        public TokenCredential Credential { get; set; }
    }
}