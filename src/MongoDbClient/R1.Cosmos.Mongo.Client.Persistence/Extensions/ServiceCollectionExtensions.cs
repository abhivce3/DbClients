// Ignore Spelling: Mongo

namespace R1.Cosmos.Mongo.Client.Persistence.Extensions
{
    using R1.Cosmos.Mongo.Client.Domain.Configuration;
    using System;
    using MongoDB.Driver;
    using R1.Cosmos.Mongo.Client.Persistence.Factory;
    using R1.Cosmos.Mongo.Client.Domain.IFactory;

#if NETSTANDARD || NETCOREAPP || NET5_0_OR_GREATER

    using Microsoft.Extensions.DependencyInjection;

#elif NETFRAMEWORK
    using Unity;
    using Unity.Injection;

#endif

    public static class ServiceCollectionExtensions
    {
#if NETSTANDARD || NETCOREAPP || NET5_0_OR_GREATER

        public static IServiceCollection AddCosmosDbServices(this IServiceCollection services, Action<CosmosDbOptions> setupAction)
        {
            var options = new CosmosDbOptions();
            setupAction(options);
            services.AddSingleton<IMongoClient>(new MongoClient(options.Endpoint));
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            return services;
        }

#elif NETFRAMEWORK
        public static UnityContainer AddCosmosDbServices(this UnityContainer container, Action<CosmosDbOptions> setupAction)
        {
            var options = new CosmosDbOptions();
            setupAction(options);
            container.RegisterSingleton<IMongoClient, MongoClient>(new InjectionConstructor(options.Endpoint));
            container.RegisterSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            return container;
        }
#endif
    }
}