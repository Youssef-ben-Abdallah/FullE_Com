using BI.Sales.Api.DTOs;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] int? categoryId, [FromQuery] int? subCategoryId, [FromQuery] string? q, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] bool? active)
    {
        var products = await _productRepository.GetAllAsync(categoryId, subCategoryId, q, minPrice, maxPrice, active);
        return Ok(products.Select(MapProduct));
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(MapProduct(product));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> Create(ProductDto request)
    {
        var product = new Product
        {
            SubCategoryId = request.SubCategoryId,
            Name = request.Name,
            Sku = request.Sku,
            UnitPrice = request.UnitPrice,
            StockQty = request.StockQty,
            IsActive = request.IsActive,
            ImageUrl = request.ImageUrl
        };

        product = await _productRepository.AddAsync(product);
        return Ok(MapProduct(product));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, ProductDto request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        product.SubCategoryId = request.SubCategoryId;
        product.Name = request.Name;
        product.Sku = request.Sku;
        product.UnitPrice = request.UnitPrice;
        product.StockQty = request.StockQty;
        product.IsActive = request.IsActive;
        product.ImageUrl = request.ImageUrl;

        await _productRepository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productRepository.DeleteAsync(product);
        return NoContent();
    }

    private static ProductDto MapProduct(Product product) =>
        new(product.ProductId, product.SubCategoryId, product.Name, product.Sku, product.UnitPrice, product.StockQty, product.IsActive, product.ImageUrl);
}
