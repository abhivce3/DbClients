namespace R1.CosmosClient.Domain.Configuration
{
    using Azure.Core;

    public class CosmosDbOptions
    {
        public string Endpoint { get; set; }
        public TokenCredential Credential { get; set; }
        public string DatabaseId { get; set; }
        public string ContainerId { get; set; }
    }
}