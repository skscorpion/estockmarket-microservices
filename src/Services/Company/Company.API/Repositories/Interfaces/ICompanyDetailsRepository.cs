using Company.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.API.Repositories.Interfaces
{
    public interface ICompanyDetailsRepository
    {
        Task<IEnumerable<CompanyDetails>> GetProducts();
        Task<CompanyDetails> GetProduct(string id);
        Task<IEnumerable<CompanyDetails>> GetProductByName(string name);
        Task<IEnumerable<CompanyDetails>> GetProductByCategory(string categoryName);

        Task CreateProduct(CompanyDetails product);
        Task<bool> UpdateProduct(CompanyDetails product);
        Task<bool> DeleteProduct(string id);
    }
}
