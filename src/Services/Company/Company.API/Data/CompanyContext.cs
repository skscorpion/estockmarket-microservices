using Company.API.Data.Interfaces;
using Company.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Company.API.Data
{
    public class CompanyContext : ICompanyContext
    {
        public CompanyContext(IConfiguration configuration)
        {            
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<CompanyDetails>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CompanyContextSeed.SeedData(Products);
        }

        public IMongoCollection<CompanyDetails> Products { get; }
    }
}
