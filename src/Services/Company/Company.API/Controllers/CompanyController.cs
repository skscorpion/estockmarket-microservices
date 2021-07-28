using Company.API.Entities;
using Company.API.Repositories.Interfaces;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Company.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyDetailsRepository _repository;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyDetailsRepository repository, ILogger<CompanyController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyDetails>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductByCategory(category);
            return Ok(products);
        }

        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<CompanyDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetProductByName(string name)
        {
            var items = await _repository.GetProductByName(name);
            if (items == null)
            {
                _logger.LogError($"Products with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyDetails>> CreateProduct([FromBody] CompanyDetails product)
        {
            await _repository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] CompanyDetails product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]        
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }
    }
}
