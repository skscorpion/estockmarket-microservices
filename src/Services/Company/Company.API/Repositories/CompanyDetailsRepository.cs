using Company.API.Data.Interfaces;
using Company.API.Entities;
using Company.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.API.Repositories
{
    public class CompanyDetailsRepository : ICompanyDetailsRepository
    {
        private readonly ICompanyContext _context;

        public CompanyDetailsRepository(ICompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CompanyDetails>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<CompanyDetails> GetProduct(string id)
        {
            return await _context
                           .Products
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyDetails>> GetProductByName(string name)
        {
            FilterDefinition<CompanyDetails> filter = Builders<CompanyDetails>.Filter.ElemMatch(p => p.Name, name);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<CompanyDetails>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<CompanyDetails> filter = Builders<CompanyDetails>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }


        public async Task CreateProduct(CompanyDetails product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(CompanyDetails product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<CompanyDetails> filter = Builders<CompanyDetails>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
