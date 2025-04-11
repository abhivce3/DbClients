namespace R1.Cosmos.Mongo.Client.Extensions
{
    using R1.Cosmos.Mongo.Client.Domain.Configuration;
    using System;
    using R1.Cosmos.Mongo.Client.Factory;
    using R1.Cosmos.Mongo.Client.IFactory;
    using R1.Cosmos.Mongo.Client.Persistence.Extensions;

#if NETSTANDARD || NETCOREAPP || NET5_0_OR_GREATER

    using Microsoft.Extensions.DependencyInjection;

#elif NETFRAMEWORK
using Unity;
#endif

    public static class ServiceCollectionExtensions
    {
#if NETSTANDARD || NETCOREAPP || NET5_0_OR_GREATER

        public static IServiceCollection AddCosmosServices(this IServiceCollection services, Action<CosmosDbOptions> setupAction)
        {
            services.AddCosmosDbServices(setupAction);
            services.AddSingleton<ICosmosMongoService, CosmosMongoService>();
            return services;
        }

#elif NETFRAMEWORK
 public static UnityContainer AddCosmosServices(this UnityContainer container, Action<CosmosDbOptions> setupAction)
        {
            container.RegisterSingleton<ICosmosMongoService, CosmosMongoService>();
            container.AddCosmosDbServices(setupAction);
            return container;
        }
#endif
    }
}