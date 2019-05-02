using System.ComponentModel;
using DataGenerator;
using Microsoft.Extensions.Configuration;
using MongoDB.DataGeneration;
using MongoDB.Driver;
using MongoDB.Models;
using Unity;

namespace MongoDB
{
    internal sealed class ConsoleContainerFactory : IContainerFactory
    {
        public IUnityContainer ConfigureContainer()
        {
            IUnityContainer container = new UnityContainer();
            var configuration= new ConfigurationBuilder().AddJsonFile("appConfig.json").Build();
            IMongoClient mongoClient = new MongoClient(configuration["connectionString"]);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(configuration["databaseName"]);

            Generator.Default.Configure(builder => builder.Profile<AccountInfoProfile>());
            Generator.Default.Configure(builder => builder.Profile<AddressProfile>());
            Generator.Default.Configure(builder => builder.Profile<EmployeeProfile>());

            container.RegisterType<ILogger, ConsoleLogger>();
            container.RegisterType<IApplication, ConsoleApplication>();
            container.RegisterType<IDataProvider<Employee>, EmployeeDataProvider>();
            container.RegisterInstance<IConfiguration>(configuration);
            container.RegisterInstance(mongoDatabase);
            return container;
        }
    }
}