namespace R1.CosmosClient.Persistence.Extensions
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.DependencyInjection;
    using R1.CosmosClient.Domain.Configuration;
    using R1.CosmosClient.Domain.Data;
    using R1.CosmosClient.Domain.IFactory;
    using R1.CosmosClient.Persistence.Factory;
    using R1.CosmosClient.Persistence.Repositories;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosDbServices(this IServiceCollection services, Action<CosmosDbOptions> setupAction)
        {
            // bind CosmosDbOptions from the provided delegate setupAction to access Cosmos Db settings.
            var options = new CosmosDbOptions();
            setupAction(options);

            // Create a singleton CosmosClient instance based on the provided endpoint and key.
            services.AddSingleton(s => new CosmosClient(options.Endpoint, options.Credential));

            services.AddScoped(typeof(ICosmosDbRepository<>), typeof(CosmosDbRepository<>));
            services.AddSingleton<ICosmosDbRepositoryFactory, CosmosDbRepositoryFactory>();

            return services;
        }
    }
}