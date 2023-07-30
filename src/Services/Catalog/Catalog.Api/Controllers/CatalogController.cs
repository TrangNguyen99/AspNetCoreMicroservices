using Catalog.Api.Data;
using Catalog.Api.Dto;
using Catalog.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogDbContext _catalogDbContext;

        public CatalogController(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _catalogDbContext.Products.Find(x => true).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _catalogDbContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateOrUpdateProductInput input)
        {
            var product = new Product
            {
                Name = input.Name,
                Price = input.Price
            };

            await _catalogDbContext.Products.InsertOneAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] CreateOrUpdateProductInput input)
        {
            var product = new Product
            {
                Id = id,
                Name = input.Name,
                Price = input.Price
            };

            await _catalogDbContext.Products.ReplaceOneAsync(x => x.Id == id, product);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            await _catalogDbContext.Products.DeleteOneAsync(x => x.Id == id);
            return Ok();
        }
    }
}
