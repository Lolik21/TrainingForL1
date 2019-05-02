using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Models;

namespace MongoDB
{
    internal sealed class EmployeeDataProvider : IDataProvider<Employee>
    {
        private readonly IMongoCollection<Employee> _mongoCollection;
        private readonly ILogger _logger;

        public EmployeeDataProvider(IMongoDatabase mongoDatabase, IConfiguration configuration, ILogger logger)
        {
            var collectionName = configuration["collectionName"];
            _mongoCollection = mongoDatabase.GetCollection<Employee>(collectionName);
            _logger = logger;
        }

        public IEnumerable<Employee> GetAll()
        {
            _logger.Log($"Getting all items from employee provider");
            var item = _mongoCollection.FindSync(employee => true).ToEnumerable();
            _logger.Log($"Got all items from employee provider");
            return item;
        }

        public Employee GetItem(string id)
        {
            _logger.Log($"Getting item from employee provider");
            var item = _mongoCollection.FindSync(employee => employee.Id == id).First();
            _logger.Log($"Got item from employee provider");
            return item;
        }

        public void Create(Employee item)
        {
            _logger.Log($"Creating from employee provider");
            _mongoCollection.InsertOne(item);
            _logger.Log($"Created from employee provider");
        }

        public void Update(Employee item)
        {
            _logger.Log($"Updating from employee provider");
            _mongoCollection.ReplaceOne(employee => employee.Id == item.Id, item);
            _logger.Log($"Updated from employee provider");
        }

        public void Delete(string id)
        {
            _logger.Log($"Deleting from employee provider");
            _mongoCollection.DeleteOne(employee => employee.Id == id);
            _logger.Log($"Deleted from employee provider");
        }
    }
}