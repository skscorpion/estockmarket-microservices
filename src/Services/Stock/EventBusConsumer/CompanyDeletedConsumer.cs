using EventBus.Messages.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Stock.API.Services;

namespace Stock.API.EventBusConsumer
{
    public class CompanyDeletedConsumer : IConsumer<CompanyDeleteEvent>
    {
        private readonly ILogger<CompanyDeletedConsumer> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public CompanyDeletedConsumer(ILogger<CompanyDeletedConsumer> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        public async Task Consume(ConsumeContext<CompanyDeleteEvent> context)
        {
            var command = context.Message;
            var result = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");

            _logger.LogInformation("Stock deleted");
        }
    }
}
