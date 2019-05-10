using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace api.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> customerCollection;

        public CustomerService(IConfiguration config)
        {
            var database = new MongoClient(config.GetConnectionString("ApiDb")).GetDatabase("ApiDb");
            customerCollection = database.GetCollection<Customer>("Customers");
        }

        public List<Customer> Get()
        {
            return customerCollection.Find<Customer>(customer => true).ToList();
        }

        public Customer Get(string id)
        {
            return customerCollection.Find<Customer>(customer => customer.Id == id).FirstOrDefault(); 
        }

        public Customer Create(Customer newCustomer) 
        {
            customerCollection.InsertOne(newCustomer);
            return newCustomer;
        }

        public void Update(string id, Customer customerToUpdate)
        {
            customerToUpdate.Id = null;
            customerCollection.ReplaceOne(customer => customer.Id == id, customerToUpdate);
        }

        public void Remove(Customer customerToDelete)
        {
            customerCollection.DeleteOne(customer => customer.Id == customerToDelete.Id);
        }

        public void Remove(string id)
        {
            customerCollection.DeleteOne(customer => customer.Id == id);
        }
    }
}