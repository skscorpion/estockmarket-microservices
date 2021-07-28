using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Stock.API.Models;

namespace Stock.API.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(StockDetails item)
        {
            await this._container.CreateItemAsync<StockDetails>(item, new PartitionKey(item.ID));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<StockDetails>(id, new PartitionKey(id));
        }

        public async Task<StockDetails> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<StockDetails> response = await this._container.ReadItemAsync<StockDetails>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<StockDetails>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<StockDetails>(new QueryDefinition(queryString));
            List<StockDetails> results = new List<StockDetails>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, StockDetails item)
        {
            await this._container.UpsertItemAsync<StockDetails>(item, new PartitionKey(id));
        }
    }
}
