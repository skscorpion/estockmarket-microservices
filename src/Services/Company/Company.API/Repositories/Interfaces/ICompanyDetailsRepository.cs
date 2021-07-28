using Company.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.API.Repositories.Interfaces
{
    public interface ICompanyDetailsRepository
    {
        Task<IEnumerable<CompanyDetails>> GetCompanies();
        Task<CompanyDetails> GetCompany(string id);
        Task<IEnumerable<CompanyDetails>> GetCompanyByName(string name);
        Task<IEnumerable<CompanyDetails>> GetCompanyByCode(string categoryName);

        Task CreateCompany(CompanyDetails product);
        Task<bool> UpdateCompany(CompanyDetails product);
        Task<bool> DeleteCompany(string id);
    }
}
