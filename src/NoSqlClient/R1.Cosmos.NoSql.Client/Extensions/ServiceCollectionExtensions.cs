namespace R1.CosmosClient.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using R1.CosmosClient.Domain.Configuration;
    using R1.CosmosClient.Factory;
    using R1.CosmosClient.IFactory;
    using R1.CosmosClient.IServices;
    using R1.CosmosClient.Persistence.Extensions;
    using R1.CosmosClient.Services;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosServices(this IServiceCollection services, Action<CosmosDbOptions> setupAction)
        {
            services.AddLogging();
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddSingleton<IEntityServiceFactory, EntityServiceFactory>();
            services.AddCosmosDbServices(setupAction);
            return services;
        }
    }
}