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

            Companies = database.GetCollection<CompanyDetails>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CompanyContextSeed.SeedData(Companies);
        } 

        public IMongoCollection<CompanyDetails> Companies { get; }
    }
}
