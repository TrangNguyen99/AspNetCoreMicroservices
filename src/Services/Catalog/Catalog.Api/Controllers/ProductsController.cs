using Catalog.Api.Dto;
using Catalog.Core.Data;
using Catalog.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly CatalogDbContext _catalogDbContext;

    public ProductsController(CatalogDbContext catalogDbContext)
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
    public async Task<ActionResult<Product?>> GetProductById(string id)
    {
        var product = await _catalogDbContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

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

        return CreatedAtAction(nameof(GetProductById), new { product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] CreateOrUpdateProductInput input)
    {
        var newProduct = new Product
        {
            Id = id,
            Name = input.Name,
            Price = input.Price
        };

        var oldProduct = await _catalogDbContext.Products.FindOneAndReplaceAsync(x => x.Id == id, newProduct);

        if (oldProduct == null)
        {
            return NotFound();
        }

        return Ok(newProduct);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(string id)
    {
        var product = await _catalogDbContext.Products.FindOneAndDeleteAsync(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok();
    }
}
