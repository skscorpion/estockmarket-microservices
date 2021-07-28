using Stock.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.API.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<StockDetails>> GetItemsAsync(string query);
        Task<StockDetails> GetItemAsync(string id);
        Task AddItemAsync(StockDetails item);
        Task UpdateItemAsync(string id, StockDetails item);
        Task DeleteItemAsync(string id);
    }
}
