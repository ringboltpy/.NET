using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var created = await _productService.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        var success = await _productService.UpdateAsync(product);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _productService.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        return Ok(await _productService.GetByCategoryAsync(categoryId));
    }

    [HttpGet("category/{categoryId}/total")]
    public async Task<IActionResult> GetTotalPriceByCategory(int categoryId)
    {
        return Ok(await _productService.GetTotalPriceByCategoryAsync(categoryId));
    }

    [HttpGet("total-per-category")]
    public async Task<IActionResult> GetTotalPricePerCategory()
    {
        return Ok(await _productService.GetTotalPricePerCategoryAsync());
    }
}
