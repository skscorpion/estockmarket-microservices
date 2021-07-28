using Company.API.Entities;
using MongoDB.Driver;

namespace Company.API.Data.Interfaces
{
    public interface ICompanyContext
    {
        IMongoCollection<CompanyDetails> Companies { get; }
    }
}
