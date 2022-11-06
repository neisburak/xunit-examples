using Microsoft.AspNetCore.Mvc;
using Mvc.Application.Models;
using Mvc.Application.Repository;

namespace Mvc.Application.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsApiController : ControllerBase
{
    private readonly IGenericRepository<Product> _repository;

    public ProductsApiController(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

    [HttpGet("{a}/{b}")]
    public IActionResult Add(int a, int b)
    {
        return Ok(new Helpers.Helper().Add(a, b));
    }

    // GET: api/ProductsApi
    [HttpGet]
    public async Task<IActionResult> GetProduct()
    {
        return Ok(await _repository.GetAll());
    }

    // GET: api/ProductsApi/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _repository.GetById(id);
        if (product is null) return NotFound();

        return Ok(product);
    }

    // PUT: api/ProductsApi/5
    [HttpPut("{id}")]
    public IActionResult PutProduct(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        _repository.Update(product);

        return NoContent();
    }

    // POST: api/ProductsApi
    [HttpPost]
    public async Task<IActionResult> PostProduct(Product product)
    {
        await _repository.Create(product);

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/ProductsApi/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await _repository.GetById(id);
        if (product is null) return NotFound();

        _repository.Delete(product);

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        var product = _repository.GetById(id)?.Result;

        return product != null;
    }
}
